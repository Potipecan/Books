using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Books.Utils;
using Database;
using Database.TableClasses;

namespace Books
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            SearchResults = new List<object>();

            SearchBookComboBox.SelectedIndex = 3;

            isUserEditMode = false;

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

        #region SearchPage

        private List<ListViewPreset> Presets;
        private List<object> SearchResults;


        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseManager.DeleteDatabase();
        }

        private async void MainSearchButton_Click(object sender, EventArgs e)
        {
            Presets[SearchBookComboBox.SelectedIndex].SetListView(SearchResultLW);
            SearchResultLW.Items.Clear();
            SearchResults.Clear();

            switch (SearchBookComboBox.SelectedIndex)
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

        private void SearchResultLW_DoubleClick(object sender, EventArgs e)
        {
            switch (SearchBookComboBox.SelectedIndex)
            {
                case 0: // avtor
                    break;
                case 1: // inventarna
                    break;
                case 2: // naslov
                    break;
                case 3: // član
                    GoToUserPage(SearchResults[SearchResultLW.SelectedItems[0].Index] as User);
                    break;
            }
        }


        #region Search functions

        private async Task SearchAuthor()
        {
            var authors = await DatabaseManager.GetAuthors(SearchTextBox.Text);

            foreach (var a in authors)
            {
                var row = new ListViewItem(new string[] {
                    a.Name,
                    a.Description
                });
                SearchResultLW.Items.Add(row);

                SearchResults.Add(a);
            }

        }

        private async Task SearchBookCopy()
        {
            var bookcopy = await DatabaseManager.GetBookCopy(SearchTextBox.Text);

        }

        private async Task SearchBook()
        {
            var books = await DatabaseManager.GetBooks(SearchTextBox.Text);

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
                SearchResultLW.Items.Add(row);

                SearchResults.Add(b);
            }
        }

        private async Task SearchUser()
        {
            var users = await DatabaseManager.GetUsers(SearchTextBox.Text);

            foreach (var u in users)
            {

                var row = new ListViewItem(new string[] {
                    u.Name,
                    u.Surname,
                    u.Phone,
                    u.Email != "" ? u.Email : "Null"
                });
                SearchResultLW.Items.Add(row);

                SearchResults.Add(u);
            }
        }

        #endregion

        #endregion

        #region User page

        private bool isUserEditMode;
        private bool IsUserEditMode
        {
            get => isUserEditMode; set
            {
                isUserEditMode = value;
                if (value)
                {
                    EditUserButton.Text = "Potrdi";
                    EditUserButton.Click -= InitiateUserEdit;
                    EditUserButton.Click += ConfirmUserEdit;
                    CancelEditButton.Show();
                }
                else
                {
                    EditUserButton.Text = "Uredi";
                    EditUserButton.Click += InitiateUserEdit;
                    EditUserButton.Click -= ConfirmUserEdit;
                    CancelEditButton.Hide();
                }

                UserNameTB.Enabled = value;
                UserSurnameTB.Enabled = value;
                UserPhoneTB.Enabled = value;
                UserAddressTB.Enabled = value;
                UserEmailTB.Enabled = value;
                UserNotesTB.Enabled = value;
            }
        }

        private User selectedUser;
        private User SelectedUser
        {
            get => selectedUser; set
            {
                isUserEditMode = false;
                if(value != null)
                {
                    selectedUser = value;
                    UserNameTB.Text = value.Name;
                    UserSurnameTB.Text = value.Surname;
                    UserPhoneTB.Text = value.Phone;
                    UserAddressTB.Text = value.Address;
                    UserEmailTB.Text = value.Email;
                    UserNotesTB.Text = value.Notes;
                }
            }
        }

        private async void GoToUserPage(User user)
        {
            SelectedUser = await DatabaseManager.GetUser(user);

            if(SelectedUser != null)
            {
                MainTabControl.SelectedTab = UserTabPage;
                FillBookRents(SelectedUser.BookRents);
            }
        }

        private void FillBookRents(List<BookRent> rents)
        {
            foreach(var r in rents)
            {
                var row = new ListViewItem(new string[]{
                    r.BookCopy.Code,
                    r.BookCopy.Book.Title,
                    r.BookCopy.Publisher.Name,
                    r.RentDate.ToShortDateString(),
                    r.DeadLine.ToShortDateString()
                }); ;

                BookRentsLW.Items.Add(row);
            }
        }

        private async void ConfirmUserEdit(object sender, EventArgs e)
        {
            var u = new User()
            {
                ID = SelectedUser.ID,
                Name = UserNameTB.Text,
                Surname = UserSurnameTB.Text,
                Phone = UserPhoneTB.Text,
                Address = UserAddressTB.Text,
                Email = UserEmailTB.Text,
                Notes = UserNotesTB.Text
            };

            if (await DatabaseManager.UpdateUser(u))
            {
                MessageBox.Show("Spremembe uspešno shranjene.");
                SelectedUser = u;
            }
            else MessageBox.Show("Napaka!");
        }

        private void InitiateUserEdit(object sender, EventArgs e)
        {
            if (SelectedUser != null) IsUserEditMode = true;
        }

        private void CancelEditButton_Click(object sender, EventArgs e)
        {
            IsUserEditMode = false;
        }



        #endregion

    }
}
