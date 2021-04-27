using Database;
using Database.TableClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Books.TabControllers
{
    public class MaterialTabController
    {
        private MainForm f;

        private delegate void VoidEventHandler();
        private event VoidEventHandler SectionsUpdated;
        private event VoidEventHandler AuthorsUpdated;
        private event VoidEventHandler PublishersUpdated;


        public MaterialTabController(MainForm form)
        {
            f = form;

            SelectedSection = null;

            newBookCopies = new List<BookCopy>();
            newAuthors = new List<Author>();

            f.BookCopyAcquisitionCB.Items.Add((AcquisitionType)0);
            f.BookCopyAcquisitionCB.Items.Add((AcquisitionType)1);
            f.BookCopyAcquisitionCB.Items.Add((AcquisitionType)2);
            f.BookCopyAcquisitionCB.Items.Add((AcquisitionType)3);
            f.BookCopyAcquisitionCB.Items.Add((AcquisitionType)4);


            SectionsUpdated += OnSectionsUpdated;
            OnSectionsUpdated();
            AuthorsUpdated += OnAuthorsUpdated;
            OnAuthorsUpdated();
            PublishersUpdated += OnPublishersUpdated;
            OnPublishersUpdated();
        }

        #region Signal bindings

        private void OnSectionsUpdated()
        {
            var sections = new List<Section>();

            Task.Run(async () =>
            {
                sections = await DatabaseManager.GetSections();
            }).Wait();

            f.BookSectionCB.Items.Clear();
            foreach (var s in sections) f.BookSectionCB.Items.Add(s);
        }

        private void OnAuthorsUpdated()
        {
            var authors = new List<Author>();
            var t = f.AuthorSelectionCB.Text;
            Task.Run(async () =>
            {
                authors = await DatabaseManager.GetAuthors(t);
            }).Wait();
            f.AuthorSelectionCB.Items.Clear();
            foreach (var a in authors) f.AuthorSelectionCB.Items.Add(a);
        }

        private void OnPublishersUpdated()
        {
            var publishers = new List<Publisher>();
            var t = f.PublishersCB.Text;
            Task.Run(async () =>
            {
                publishers = await DatabaseManager.GetPublishers(t);
            }).Wait();
            f.PublishersCB.Items.Clear();
            foreach (var a in publishers) f.PublishersCB.Items.Add(a);
        }

        #endregion

        #region Books page

        private Book currentBook;
        public Book CurrentBook
        {
            get => currentBook;
            set
            {

                if (value != null)
                {
                    f.BookTitleTB.Text = value.Title;
                    f.BookDescriptionTB.Text = value.Description;

                    if (value.Authors.Count == 0 || value.Section == null || value.BookCopies.Count < 1)
                        Task.Run(async () => value = await DatabaseManager.GetBook(value)).Wait();


                    f.BookSectionCB.SelectedItem = value.Section;

                    f.AuthorsBookLB.Items.Clear();
                    foreach (var a in value.Authors) f.AuthorsBookLB.Items.Add(a.Name);

                    f.BookCopyLW.Items.Clear();
                    foreach (var bc in value.BookCopies) DisplayBookCopy(bc);

                    newBookCopies = value.BookCopies;
                    newAuthors = value.Authors;
                }
                else
                {
                    f.BookTitleTB.Text = "";
                    f.BookDescriptionTB.Text = "";

                    f.BookSectionCB.SelectedItem = null;
                    f.AuthorsBookLB.Items.Clear();
                    f.AuthorSelectionCB.SelectedIndex = -1;
                    f.BookCopyLW.Items.Clear();

                    SelectedBookCopy = null;

                    newBookCopies = new List<BookCopy>();
                    newAuthors = new List<Author>();
                }

                currentBook = value;
            }
        }

        private BookCopy selectedBookCopy;
        private BookCopy SelectedBookCopy
        {
            get => selectedBookCopy;
            set
            {
                selectedBookCopy = value;
                if (value != null)
                {
                    f.BookCopyCodeTB.Text = value.Code;
                    f.PublishersCB.SelectedItem = value.Publisher;
                    f.BookCopyAcquisitionCB.SelectedIndex = value.AcquisitionType;
                    f.BookCopyAcquisitionDTP.Value = value.AcquisitionDate;
                    f.BookCopyYearNUD.Value = value.Year;
                }
                else
                {
                    f.BookCopyCodeTB.Text = "";
                    f.PublishersCB.SelectedIndex = -1;
                    f.BookCopyAcquisitionCB.SelectedIndex = -1;
                    f.BookCopyAcquisitionDTP.Value = DateTime.Now;
                    f.BookCopyYearNUD.Value = DateTime.Now.Year;
                }
            }
        }

        private List<BookCopy> newBookCopies;
        private List<Author> newAuthors;

        public void AuthorQuery()
        {

        }

        private void DisplayBookCopy(BookCopy copy)
        {
            var s = new string[]
            {
                copy.Code,
                copy.Publisher.Name,
                ((AcquisitionType)copy.AcquisitionType).ToString(),
                copy.Year.ToString()
            };

            var i = new ListViewItem(s);

            newBookCopies.Add(copy);
            f.BookCopyLW.Items.Add(i);
        }

        public async void AddBookCopy()
        {
            var code = f.BookCopyCodeTB.Text;

            if (code == "")
            {
                MessageBox.Show("Izvod mora imeti inventarno številko.");
                return;
            }

            if (f.PublishersCB.SelectedItem == null)
            {
                MessageBox.Show("Izvod mora imeti založnika.");
                return;
            }

            if (f.BookCopyAcquisitionCB.SelectedItem == null)
            {
                MessageBox.Show("Izberite način pridobitve izvoda.");
                return;
            }

            if (!await DatabaseManager.IsCodeUnique(code) || newBookCopies.Exists(b => b.Code == code))
            {
                MessageBox.Show("Inventarna številka že obstaja");
                return;
            }

            var bc = new BookCopy()
            {
                Code = f.BookCopyCodeTB.Text,
                AcquisitionDate = f.BookCopyAcquisitionDTP.Value,
                Publisher = (f.PublishersCB.SelectedItem as Publisher),
                AcquisitionType = f.BookCopyAcquisitionCB.SelectedIndex,
                Year = Convert.ToInt32(f.BookCopyYearNUD.Value)
            };

            SelectedBookCopy = null;
            DisplayBookCopy(bc);
        }

        public async void AddBook()
        {
            switch (ValidateBook())
            {
                case BookValidationResult.MissingTitleOrDescription:
                    MessageBox.Show("Naslov in opis ne more biti prazna.");
                    return;

                case BookValidationResult.NoAuthors:
                    MessageBox.Show("Knjiga imora imeti vsaj enega avtorja.");
                    return;

                case BookValidationResult.NoBookCopies:
                    MessageBox.Show("Knjiga mora imeti vsaj en izvod.");
                    return;
            }

            var b = new Book()
            {
                Title = f.BookTitleTB.Text,
                Description = f.BookDescriptionTB.Text,
                Authors = newAuthors,
                BookCopies = newBookCopies,
                Section = f.BookSectionCB.SelectedItem as Section
            };

            if (await DatabaseManager.AddBook(b))
            {
                MessageBox.Show("Knjiga uspešno dodana");
                return;
            }

            MessageBox.Show("Napaka!");
        }

        public async void UpdateBook()
        {
            switch (ValidateBook())
            {
                case BookValidationResult.MissingTitleOrDescription:
                    MessageBox.Show("Naslov in opis ne more biti prazna.");
                    return;

                case BookValidationResult.NoAuthors:
                    MessageBox.Show("Knjiga imora imeti vsaj enega avtorja.");
                    return;

                case BookValidationResult.NoBookCopies:
                    MessageBox.Show("Knjiga mora imeti vsaj en izvod.");
                    return;
            }

            var b = new Book()
            {
                ID = CurrentBook.ID,
                Title = f.BookTitleTB.Text,
                Description = f.BookDescriptionTB.Text,
                Authors = newAuthors,
                BookCopies = newBookCopies,
                Section = f.BookSectionCB.SelectedItem as Section
            };

            if (await DatabaseManager.UpdateBook(b))
            {
                MessageBox.Show("Spremembe uspešno shranjene.");
                return;
            }

            MessageBox.Show("Napaka pri shranjevanju sprememb.");
        }

        public async void DeleteBook()
        {
            if (await DatabaseManager.DeleteBook(CurrentBook))
            {
                MessageBox.Show("Brisanje knjige uspešno.");
                CurrentBook = null;
                return;
            }

            MessageBox.Show("Napaka pri brisanju knjige.");
        }

        public void RemoveBookCopy()
        {
            if (SelectedBookCopy == null) return;
            CurrentBook.BookCopies.Remove(SelectedBookCopy);
            f.BookCopyLW.Items.Remove(f.BookCopyLW.SelectedItems[0]);
            SelectedBookCopy = null;
        }

        public void SelectAuthor()
        {
            var a = f.AuthorSelectionCB.SelectedItem;
            if (a == null) return;

            newAuthors.Add(a as Author);
            f.AuthorsBookLB.Items.Add(a);
        }

        private BookValidationResult ValidateBook()
        {
            if (f.BookTitleTB.Text == "" || f.BookDescriptionTB.Text == "")
                return BookValidationResult.MissingTitleOrDescription;

            if (newBookCopies.Count < 1)
                return BookValidationResult.NoBookCopies;

            if (newAuthors.Count < 1)
                return BookValidationResult.NoAuthors;

            return BookValidationResult.OK;
        }

        private enum BookValidationResult
        {
            OK = 0,
            MissingTitleOrDescription = 1,
            NoAuthors = 2,
            NoBookCopies = 3
        }

        private enum AcquisitionType
        {
            Kupljeno = 0,
            Darilo = 1,
            Sposojeno = 2,
            Najdeno = 3,
            Drugo = 4
        }

        #endregion

        #region Authors page

        private Author selectedAuthor;
        public Author SelectedAuthor
        {
            get => selectedAuthor;
            set
            {
                selectedAuthor = value;

                IsAuthorEditMode = false;

                if (value != null)
                {
                    f.AuthorNameTB.Text = value.Name;
                    f.AuthorDescriptionTB.Text = value.Description;
                    f.AddAuthorButton.Text = "Uredi";
                }
                else
                {
                    f.AuthorNameTB.Text = "";
                    f.AuthorDescriptionTB.Text = "";
                    f.AddAuthorButton.Text = "Dodaj";
                }
            }
        }

        private bool isAuthorEditMode;
        public bool IsAuthorEditMode
        {
            get => isAuthorEditMode;
            set
            {
                isAuthorEditMode = value;

                bool b = value || SelectedSection == null;

                f.AuthorNameTB.Enabled = b;
                f.AuthorDescriptionTB.Enabled = b;
                f.CancelAuthorEditButton.Text = value ? "Prekliči" : "Počisti";
                if (SelectedAuthor != null) f.AddAuthorButton.Text = value ? "Potrdi" : "Uredi";
            }
        }

        public async void AddAuthor()
        {
            if (!ValidateAuthor())
            {
                MessageBox.Show("Izpolnite vsa polja.");
                return;
            }

            var a = new Author()
            {
                Name = f.AuthorNameTB.Text,
                Description = f.AuthorDescriptionTB.Text
            };

            if (await DatabaseManager.AddAuthor(a))
            {
                SelectedAuthor = a;
                AuthorsUpdated.Invoke();
                MessageBox.Show("Avtor uspešno shranjen.");
                return;
            }

            MessageBox.Show("Napaka pri sharnjevanju avtorja");
        }

        public async void EditAuthor()
        {
            if (!IsAuthorEditMode)
            {
                IsAuthorEditMode = true;
                return;
            }

            if (!ValidateAuthor())
            {
                MessageBox.Show("Izpolnite vsa polja.");
                return;
            }

            var a = new Author()
            {
                ID = SelectedAuthor.ID,
                Name = f.AuthorNameTB.Text,
                Description = f.AuthorDescriptionTB.Text
            };

            if (await DatabaseManager.UpdateAuthor(a))
            {
                SelectedAuthor = a;
                AuthorsUpdated.Invoke();
                MessageBox.Show("Spremembe uspešno shranjene.");
                return;
            }

            MessageBox.Show("Napaka pri sharnjevanju sprememb.");
        }


        private bool ValidateAuthor()
        {
            return f.AuthorNameTB.Text != "" && f.AuthorDescriptionTB.Text != "";
        }

        #endregion

        #region Sections

        private Section selectedSection;
        public Section SelectedSection
        {
            get => selectedSection;
            set
            {
                selectedSection = value;
                IsSectionEditMode = false;
                if (value != null)
                {
                    f.SectionNameTB.Text = value.Name;
                    f.SectionDescriptionTB.Text = value.Description;
                    f.AddSectionButton.Text = "Uredi";
                }
                else
                {
                    f.SectionNameTB.Text = "";
                    f.SectionDescriptionTB.Text = "";
                    f.AddSectionButton.Text = "Dodaj";
                }
            }
        }

        private bool isSectionEditMode;
        public bool IsSectionEditMode
        {
            get => isSectionEditMode;
            set
            {
                isSectionEditMode = value;

                bool b = value || SelectedSection == null;

                f.SectionNameTB.Enabled = b;
                f.SectionDescriptionTB.Enabled = b;

                f.CancelSectionEditButton.Text = value ? "Prekliči" : "Počisti";
                if (SelectedSection != null) f.AddSectionButton.Text = value ? "Potrdi" : "Uredi";
            }
        }

        public async void AddSection()
        {
            if (f.SectionNameTB.Text == "")
            {
                MessageBox.Show("Vpišite ime žanra");
                return;
            }

            var s = new Section()
            {
                Name = f.SectionNameTB.Text,
                Description = f.SectionDescriptionTB.Text
            };

            if (await DatabaseManager.AddSection(s))
            {
                MessageBox.Show("Žanr uspešno shranjen.");
                SectionsUpdated.Invoke();
                SelectedSection = s;
                return;
            }

            MessageBox.Show("Napaka pri shranjevanju žanra.");
        }

        public async void EditSection()
        {
            if (!IsSectionEditMode)
            {
                IsSectionEditMode = true;
                return;
            }

            if (f.SectionNameTB.Text == "")
            {
                MessageBox.Show("Vpišite ime žanra");
                return;
            }

            var s = new Section()
            {
                ID = SelectedSection.ID,
                Name = f.SectionNameTB.Text,
                Description = f.SectionDescriptionTB.Text
            };

            if (await DatabaseManager.UpdateSection(s))
            {
                MessageBox.Show("Spremembe uspešno shranjene.");
                SectionsUpdated.Invoke();
                return;
            }

            MessageBox.Show("Napaka pri shranjevanju sprememb.");
        }

        #endregion

        #region Publishers

        private Publisher selectedPublisher;
        public Publisher SelectedPublisher
        {
            get => selectedPublisher;
            set
            {
                selectedPublisher = value;
                if (value == null)
                {
                    f.PublisherNameTB.Text = "";
                }
                else
                {
                    f.PublisherNameTB.Text = value.Name;
                }
            }
        }

        public async void AddPubliser()
        {
            if(f.PublisherNameTB.Text == "")
            {
                MessageBox.Show("Ime založnika ne sme biti prazno.");
                return;
            }

            var p = new Publisher()
            {
                Name = f.PublisherNameTB.Text
            };

            if(await DatabaseManager.AddPublisher(p))
            {
                MessageBox.Show("Založba uspešno shranjena.");
                SelectedPublisher = p;
                return;
            }

            MessageBox.Show("Napaka pri shranjevanju založbe.");
        }

        public async void EditPublisher()
        {
            if (f.PublisherNameTB.Text == "")
            {
                MessageBox.Show("Ime založnika ne sme biti prazno.");
                return;
            }

            var p = new Publisher()
            {
                ID = SelectedPublisher.ID,
                Name = f.PublisherNameTB.Text
            };

            if (await DatabaseManager.UpdatePublisher(p))
            {
                MessageBox.Show("Spremembe uspešno shranjene.");
                SelectedPublisher = p;
                return;
            }

            MessageBox.Show("Napaka pri shranjevanju sprememb.");
        }

        #endregion
    }
}
