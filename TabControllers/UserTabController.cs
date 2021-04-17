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
    public class UserTabController
    {
        private Form1 f;

        private bool isUserEditMode;
        public bool IsUserEditMode
        {
            get => isUserEditMode; set
            {
                isUserEditMode = value;
                if (value)
                {
                    f.EditUserButton.Text = "Potrdi";
                    f.CancelEditButton.Show();
                }
                else
                {
                    f.EditUserButton.Text = "Uredi";
                    f.CancelEditButton.Hide();
                }

                f.UserNameTB.Enabled = value;
                f.UserSurnameTB.Enabled = value;
                f.UserPhoneTB.Enabled = value;
                f.UserAddressTB.Enabled = value;
                f.UserEmailTB.Enabled = value;
                f.UserNotesTB.Enabled = value;
            }
        }

        private User selectedUser;
        private User SelectedUser
        {
            get => selectedUser; set
            {
                isUserEditMode = false;
                if (value != null)
                {
                    selectedUser = value;
                    f.UserNameTB.Text = value.Name;
                    f.UserSurnameTB.Text = value.Surname;
                    f.UserPhoneTB.Text = value.Phone;
                    f.UserAddressTB.Text = value.Address;
                    f.UserEmailTB.Text = value.Email;
                    f.UserNotesTB.Text = value.Notes;

                    if (value.BookRents != null) FillBookRents(value.BookRents);
                }
            }
        }

        public UserTabController(Form1 form)
        {
            f = form;

            IsUserEditMode = false;

        }

        public async Task GoHere(User user)
        {
            SelectedUser = await DatabaseManager.GetUser(user);

            if (SelectedUser != null)
            {
                f.MainTabControl.SelectedTab = f.UserTabPage;
            }
        }

        private void FillBookRents(List<BookRent> rents)
        {
            foreach (var r in rents)
            {
                var row = new ListViewItem(new string[]{
                    r.BookCopy.Code,
                    r.BookCopy.Book.Title,
                    r.BookCopy.Publisher.Name,
                    r.RentDate.ToShortDateString(),
                    r.DeadLine.ToShortDateString()
                }); ;

                f.BookRentsLW.Items.Add(row);
            }
        }

        public async void ConfirmUserEdit()
        {
            var u = new User()
            {
                ID = SelectedUser.ID,
                Name = f.UserNameTB.Text,
                Surname = f.UserSurnameTB.Text,
                Phone = f.UserPhoneTB.Text,
                Address = f.UserAddressTB.Text,
                Email = f.UserEmailTB.Text,
                Notes = f.UserNotesTB.Text
            };

            if (await DatabaseManager.UpdateUser(u))
            {
                MessageBox.Show("Spremembe uspešno shranjene.");
                SelectedUser = u;
            }
            else MessageBox.Show("Napaka!");
        }

        public void InitiateUserEdit()
        {
            if (SelectedUser != null) IsUserEditMode = true;
        }

    }
}
