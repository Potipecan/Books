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
using Utils;

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

            #region quick tests
            Debug.WriteLine(((AcquisitionType)0).ToString());
            #endregion

        }

        #region Go here functions

        public void GoHere(Book book)
        {
            SelectedBook = book;
            f.MainTabControl.SelectedTab = f.BookTab;
            f.MaterialTabControl.SelectedTab = f.BooksPage;
        }

        public void GoHere(Author author)
        {
            SelectedAuthor = author;
            f.MainTabControl.SelectedTab = f.BookTab;
            f.MaterialTabControl.SelectedTab = f.AuthorsPage;
        }

        public void GoHere(Section section)
        {
            SelectedSection = section;
            f.MainTabControl.SelectedTab = f.BookTab;
            f.MaterialTabControl.SelectedTab = f.SectionsPage;
        }

        public void GoHere(Publisher publisher)
        {
            SelectedPublisher = publisher;
            f.MainTabControl.SelectedTab = f.BookTab;
            f.MaterialTabControl.SelectedTab = f.PublishersPage;
        }

        #endregion

        #region Signal bindings

        private void OnSectionsUpdated()
        {
            Task.Run(async () =>
            {
                Sections = await DatabaseManager.GetSections();
            }).Wait();
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
                Publishers = await DatabaseManager.GetPublishers(t);
            }).Wait();
        }

        #endregion

        #region Books page

        private List<Section> sections;
        public List<Section> Sections
        {
            get => sections;
            set
            {
                if (value == null) throw new Exception("Cannot be null");
                sections = value;

                int id = -1;
                if (f.BookSectionCB.SelectedIndex >= 0) id = (f.BookSectionCB.SelectedItem as Section).ID;

                f.BookSectionCB.Items.Clear();
                foreach (var v in value)
                {
                    f.BookSectionCB.Items.Add(v);
                    if (v.ID == id) id = Sections.Count - 1;
                }

                f.BookSectionCB.SelectedIndex = id;
            }
        }

        private List<Publisher> publishers;
        private List<Publisher> Publishers
        {
            get => publishers;
            set
            {
                if (value == null) throw new Exception("Cannot be null");
                publishers = value;

                int id = -1;
                if (f.PublishersCB.SelectedIndex >= 0) id = (f.PublishersCB.SelectedItem as Section).ID;

                f.PublishersCB.Items.Clear();
                foreach (var v in value)
                {
                    f.PublishersCB.Items.Add(v);
                    if (v.ID == id) id = Sections.Count - 1;
                }

                f.PublishersCB.SelectedIndex = id;
            }
        }

        private Book selectedBook;
        public Book SelectedBook
        {
            get => selectedBook;
            set
            {
                f.DeleteBookButton.Visible = value != null;
                if (value != null)
                {
                    f.BookTitleTB.Text = value.Title;
                    f.BookDescriptionTB.Text = value.Description;

                    if (value.Authors == null ||
                        value.Section == null ||
                        value.BookCopies == null)
                        Task.Run(async () => value = await DatabaseManager.GetBook(value)).Wait();


                    f.BookSectionCB.SelectedIndex = Sections.FindIndex(s => s.ID == value.SectionID);

                    f.AuthorsBookLB.Items.Clear();
                    foreach (var a in value.Authors) f.AuthorsBookLB.Items.Add(a.Name);

                    f.BookCopyLW.Items.Clear();
                    foreach (var bc in value.BookCopies) DisplayBookCopy(bc);

                    newBookCopies = value.BookCopies;
                    newAuthors = value.Authors;

                    f.AddBookButton.Text = "Uredi";
                    f.CancelBookEditButton.Text = "Počisti";
                }
                else
                {
                    f.BookTitleTB.Text = "";
                    f.BookDescriptionTB.Text = "";

                    f.BookSectionCB.SelectedItem = null;
                    f.AuthorsBookLB.Items.Clear();
                    f.AuthorSelectionCB.SelectedIndex = -1;
                    f.BookCopyLW.Items.Clear();


                    newBookCopies = new List<BookCopy>();
                    newAuthors = new List<Author>();

                    f.AddBookButton.Text = "Dodaj";
                    f.CancelBookEditButton.Text = "Počisti";
                }

                selectedBook = value;
                SelectedBookCopy = null;

                IsBookEditMode = false;
            }
        }

        private bool isBookEditMode;
        public bool IsBookEditMode
        {
            get => isBookEditMode;
            set
            {
                if (SelectedBook == null)
                {
                    Helper.ToggleCommonControls(f.BookGB.Controls, true);
                    isBookEditMode = false;
                    f.DeleteBookButton.Enabled = false;
                    return;
                }

                isBookEditMode = value;
                f.DeleteBookButton.Enabled = value;

                Helper.ToggleCommonControls(f.BookGB.Controls, value);

                if (value)
                {
                    f.AddBookButton.Text = "Potrdi";
                    f.CancelBookEditButton.Text = "Prekliči";
                }
                else
                {
                    f.AddBookButton.Text = "Uredi";
                    f.CancelBookEditButton.Text = "Počisti";
                }
            }
        }

        private BookCopy selectedBookCopy;
        public BookCopy SelectedBookCopy
        {
            get => selectedBookCopy;
            set
            {
                selectedBookCopy = value;

                IsBookCopyEditMode = false;

                if (value != null)
                {
                    f.BookCopyCodeTB.Text = value.Code;
                    f.PublishersCB.SelectedIndex = Publishers.FindIndex(p => p.ID == value.PublisherID);
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

        private bool isBookCopyEditMode;
        public bool IsBookCopyEditMode
        {
            get => isBookCopyEditMode;
            set
            {
                if (SelectedBookCopy == null)
                {
                    Helper.ToggleCommonControls(f.BookCopyGB.Controls, false);
                    isBookCopyEditMode = false;
                    f.RemoveBookCopyButton.Enabled = false;
                    return;
                }

                isBookCopyEditMode = value;
                f.RemoveBookCopyButton.Enabled = value;

                Helper.ToggleCommonControls(f.BookCopyGB.Controls, value);

                if (value)
                {
                    f.AddBookCopyButton.Text = "Potrdi";
                    f.CancelBookCopyEditButton.Text = "Prekliči";
                }
                else
                {
                    f.AddBookCopyButton.Text = "Uredi";
                    f.CancelBookCopyEditButton.Text = "Počisti";
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
            int i = newBookCopies.IndexOf(copy);
            var s = new string[]
            {
                copy.Code,
                copy.Publisher.Name,
                $"{(AcquisitionType)copy.AcquisitionType}",
                copy.AcquisitionDate.ToShortDateString()
            };

            var l = new ListViewItem(s);

            if (i < 0)
            {
                f.BookCopyLW.Items.Add(l);
                newBookCopies.Add(copy);
            }
            else
            {
                f.BookCopyLW.Items.RemoveAt(i);
                f.BookCopyLW.Items.Insert(i, l);
            }
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
            if (!IsBookEditMode) return;

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
                ID = SelectedBook.ID,
                Title = f.BookTitleTB.Text,
                Description = f.BookDescriptionTB.Text,
                Authors = newAuthors,
                BookCopies = newBookCopies,
                Section = f.BookSectionCB.SelectedItem as Section
            };

            if (await DatabaseManager.UpdateBook(b))
            {
                MessageBox.Show("Spremembe uspešno shranjene.");
                SelectedBook = b;
                return;
            }

            MessageBox.Show("Napaka pri shranjevanju sprememb.");
        }

        public async void DeleteBook()
        {
            if (!IsBookEditMode) return;

            var a = MessageBox.Show("Knjiga in vsi njeni izvodi bodo izbrisani iz podatkovne baze.\n" +
                "Dejanja ne bo mogoče razveljaviti.", "Brisanje knjige.", MessageBoxButtons.YesNo);

            if (a != DialogResult.Yes) return;

            if (await DatabaseManager.DeleteBook(SelectedBook))
            {
                MessageBox.Show("Brisanje knjige uspešno.");
                SelectedBook = null;
                return;
            }

            MessageBox.Show("Napaka pri brisanju knjige.");
        }

        public void SelectAuthor()
        {
            var a = f.AuthorSelectionCB.SelectedItem;
            if (a == null) return;

            newAuthors.Add(a as Author);
            f.AuthorsBookLB.Items.Add(a);
        }



        public void BookCopySelectionChanged()
        {
            if (f.BookCopyLW.SelectedItems.Count < 1) SelectedBookCopy = null;
            else SelectedBookCopy = newBookCopies[f.BookCopyLW.SelectedItems[0].Index];
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

        public void RemoveBookCopy()
        {
            if (SelectedBookCopy == null) return;
            int i = newBookCopies.IndexOf(SelectedBookCopy);
            newBookCopies.RemoveAt(i);
            f.BookCopyLW.Items.RemoveAt(i);
        }

        public async void UpdateBookCopy()
        {
            if (!IsBookCopyEditMode)
            {
                IsBookCopyEditMode = true;
                return;
            }

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


            SelectedBookCopy.Code = f.BookCopyCodeTB.Text;
            SelectedBookCopy.AcquisitionDate = f.BookCopyAcquisitionDTP.Value;
            SelectedBookCopy.Publisher = (f.PublishersCB.SelectedItem as Publisher);
            SelectedBookCopy.AcquisitionType = f.BookCopyAcquisitionCB.SelectedIndex;
            SelectedBookCopy.Year = Convert.ToInt32(f.BookCopyYearNUD.Value);

            DisplayBookCopy(SelectedBookCopy);
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
                if (SelectedAuthor == null)
                {
                    Helper.ToggleCommonControls(f.AuthorInfoGB.Controls, true);
                    isAuthorEditMode = false;
                    f.DeleteAuthorButton.Enabled = false;
                    return;
                }

                isAuthorEditMode = value;

                Helper.ToggleCommonControls(f.AuthorInfoGB.Controls, value);

                f.CancelAuthorEditButton.Text = value ? "Prekliči" : "Počisti";
                f.AddAuthorButton.Text = value ? "Potrdi" : "Uredi";
                f.DeleteAuthorButton.Enabled = value;
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

        public async void DeleteAuthor()
        {
            if (!IsAuthorEditMode) return;
            var a = MessageBox.Show("Dejanja ni mogoče razveljaviti.\nAli želite nadaljevati?", "Brisanje avtorja.", MessageBoxButtons.YesNo);
            if (a != DialogResult.OK) return;

            if (await DatabaseManager.DeleteAuthor(SelectedAuthor))
            {
                MessageBox.Show("Avtor uspoešno izbrisan.");
                SelectedAuthor = null;
                return;
            }

            MessageBox.Show("Napaka pri brisanju avtorja.");
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
                if (value.Books == null) Task.Run(async () => value = await DatabaseManager.GetSectionWithChildren(value)).Wait();

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
                if (SelectedSection == null)
                {
                    Helper.ToggleCommonControls(f.SectionInfoGB.Controls, true);
                    isSectionEditMode = false;
                    f.DeleteSectionButton.Enabled = false;
                    return;
                }

                isSectionEditMode = value;

                f.CancelSectionEditButton.Text = value ? "Prekliči" : "Počisti";
                f.AddSectionButton.Text = value ? "Potrdi" : "Uredi";
                f.DeleteSectionButton.Enabled = value;
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
                Description = f.SectionDescriptionTB.Text,
                Books = SelectedSection.Books
            };

            if (await DatabaseManager.UpdateSection(s))
            {
                MessageBox.Show("Spremembe uspešno shranjene.");
                SelectedSection = s;
                SectionsUpdated.Invoke();
                return;
            }

            MessageBox.Show("Napaka pri shranjevanju sprememb.");
        }

        public async void DeleteSection()
        {
            if (!IsSectionEditMode) return;

            var a = MessageBox.Show("To bo izbrisalo tudi vse knjige v žanru.\nDejanja ni mogoče razveljaviti.\nŽelite nadaljevati.", "Brisanje žanra", MessageBoxButtons.YesNo);
            if (a != DialogResult.Yes) return;

            if (await DatabaseManager.DeleteSection(SelectedSection))
            {
                MessageBox.Show("Žanr uspešno izbrisan");
                SelectedSection = null;
                return;
            }

            MessageBox.Show("Napaka pri brisanju žanra.");
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
                IsPublisherEditMode = false;

                f.PublisherNameTB.Text = value == null ? "" : value.Name;
            }
        }

        private bool isPublisherEditMode;
        public bool IsPublisherEditMode
        {
            get => isPublisherEditMode;
            set
            {
                if (SelectedPublisher == null)
                {
                    Helper.ToggleCommonControls(f.PublisherInfoGB.Controls, true);
                    isPublisherEditMode = false;
                    f.DeletePublisherButton.Enabled = false;
                    return;
                }

                isPublisherEditMode = value;

                Helper.ToggleCommonControls(f.PublisherInfoGB.Controls, value);

                f.AddPublisherButton.Text = value ? "Potrdi" : "Dodaj";
                f.CancelPublisherEditButton.Text = value ? "Prekliči" : "Počisti";
                f.DeletePublisherButton.Enabled = value;
            }
        }

        public async void AddPublisher()
        {
            if (f.PublisherNameTB.Text == "")
            {
                MessageBox.Show("Ime založnika ne sme biti prazno.");
                return;
            }

            var p = new Publisher()
            {
                Name = f.PublisherNameTB.Text
            };

            if (await DatabaseManager.AddPublisher(p))
            {
                MessageBox.Show("Založba uspešno shranjena.");
                SelectedPublisher = p;
                PublishersUpdated.Invoke();
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
                PublishersUpdated.Invoke();
                return;
            }

            MessageBox.Show("Napaka pri shranjevanju sprememb.");
        }

        #endregion
    }
}
