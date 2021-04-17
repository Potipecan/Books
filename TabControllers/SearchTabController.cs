using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Books.Utils;
using Database;
using Database.TableClasses;

namespace Books.TabControllers
{
    public class SearchTabController
    {
        private Form1 f;

        private List<ListViewPreset> Presets;
        private List<object> SearchResults;


        public SearchTabController(Form1 form)
        {
            f = form;

            SearchResults = new List<object>();

            f.SearchBookComboBox.SelectedIndex = 3;


            #region Preset initialization
            var authorspreset = new Dictionary<string, int>()
            {
                ["Ime in priiimek"] = 50,
                ["Opis"] = 300
            };

            var bookcopypreset = new Dictionary<string, int>()
            {
                ["Naslov"] = 100,
                ["Inventarna št."] = 50,
                ["Založba"] = 100,
                ["Avtor"] = 100,
                ["Letnik"] = 50,
            };

            var bookpreset = new Dictionary<string, int>()
            {
                ["Naslov"] = 100,
                ["Šifra"] = 50,
                ["Avtor"] = 100,
                ["Povzetek"] = 200
            };

            var userpreset = new Dictionary<string, int>()
            {
                ["Ime"] = 100,
                ["Priimek"] = 100,
                ["Telefon"] = 50,
                ["Email"] = 100,
            };

            Presets = new List<ListViewPreset>()
            {
                new ListViewPreset(authorspreset),
                new ListViewPreset(bookcopypreset),
                new ListViewPreset(bookpreset),
                new ListViewPreset(userpreset)
            };
            #endregion

        }

        public async Task Search()
        {
            Presets[f.SearchBookComboBox.SelectedIndex].SetListView(f.SearchResultLW);
            f.SearchResultLW.Items.Clear();
            SearchResults.Clear();

            switch (f.SearchBookComboBox.SelectedIndex)
            {
                case 0: // avtor
                    await SearchAuthor();
                    break;
                case 1: // inventarna
                    await SearchBookCopy();
                    break;
                case 2: // naslov
                    await SearchBook();
                    break;
                case 3: // član
                    await SearchUser();
                    break;
            }

        }

        public void InspectSearchResult()
        {
            switch (f.SearchBookComboBox.SelectedIndex)
            {
                case 0: // avtor

                    break;
                case 1: // inventarna
                    break;
                case 2: // naslov
                    break;
                case 3: // član
                    f.GoToUserPage(SearchResults[f.SearchResultLW.SelectedItems[0].Index] as User);
                    break;
            }

        }

        #region Search functions

        private async Task SearchAuthor()
        {
            var authors = await DatabaseManager.GetAuthors(f.SearchTextBox.Text);

            foreach (var a in authors)
            {
                var row = new ListViewItem(new string[] {
                    a.Name,
                    a.Description
                });
                f.SearchResultLW.Items.Add(row);

                SearchResults.Add(a);
            }

        }

        private async Task SearchBookCopy()
        {
            var bookcopy = await DatabaseManager.GetBookCopy(f.SearchTextBox.Text);

        }

        private async Task SearchBook()
        {
            var books = await DatabaseManager.GetBooks(f.SearchTextBox.Text);

            foreach (var b in books)
            {
                string authors = "";

                foreach (var a in b.Authors)
                {
                    var s = a.Name.Split(' ');
                    authors += $"{s[0].Substring(0, 1)}. {s[1]}, ";
                }
                authors.TrimEnd(',', ' ');

                var row = new ListViewItem(new string[] {
                    b.Title,
                    b.Code,
                    authors,
                    b.Description
                });
                f.SearchResultLW.Items.Add(row);

                SearchResults.Add(b);
            }
        }

        private async Task SearchUser()
        {
            var users = await DatabaseManager.GetUsers(f.SearchTextBox.Text);

            foreach (var u in users)
            {

                var row = new ListViewItem(new string[] {
                    u.Name,
                    u.Surname,
                    u.Phone,
                    u.Email != "" ? u.Email : "Null"
                });
                f.SearchResultLW.Items.Add(row);

                SearchResults.Add(u);
            }
        }

        #endregion

    }
}
