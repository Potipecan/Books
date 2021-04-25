using Database;
using Database.TableClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.TabControllers
{
    public class MaterialTabController
    {
        private MainForm f;

        public MaterialTabController(MainForm form)
        {
            f = form;

            Task.Run(async () =>
            {
                Sections = await DatabaseManager.GetSections();
            });
        }

        #region Books page

        private List<Section> sections;
        public List<Section> Sections
        {
            get => sections;
            set
            {
                sections = value;

                f.BookSectionComboBox.Items.Clear();

                foreach (var s in sections)
                {
                    f.BookSectionComboBox.Items.Add(s.Name);
                }
            }
        }

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

                    if (value.Authors.Count == 0 || value.Section == null)
                        Task.Run(async () => value = await DatabaseManager.GetBook(value));


                    f.BookSectionComboBox.SelectedIndex = Sections.FindIndex(s => s.ID == value.SectionID);
                    f.AuthorsBookLB.Items.Clear();
                    foreach(var a in value.Authors)
                    {
                        f.AuthorsBookLB.Items.Add(a.Name);
                    }
                }

                currentBook = value;
            }
        }

        #endregion
    }
}
