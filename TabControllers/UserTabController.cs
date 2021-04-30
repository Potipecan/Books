using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;
using Database;
using Database.TableClasses;


namespace Books.TabControllers
{
    public class UserTabController
    {
        private MainForm f;

        private CancellationTokenSource cancelBookQuerySource;
        private CancellationToken cancelBookQuery;


        public UserTabController(MainForm form)
        {
            f = form;

            IsUserEditMode = false;

            cancelBookQuerySource = new CancellationTokenSource();
            cancelBookQuery = cancelBookQuerySource.Token;

            newBookLoans = new List<BookRent>();

            cachedDeadline = DateTime.Now;

            f.DeadlineSelectionCB.SelectedIndex = 1;
            oldDeadlineMode = 1;
        }

        public async Task GoHere(User user)
        {
            SelectedUser = await DatabaseManager.GetUser(user);

            if (SelectedUser != null)
            {
                f.MainTabControl.SelectedTab = f.UserTabPage;
            }
        }

        #region user info tab

        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser; set
            {
                if (value != null)
                {
                    f.UserNameTB.Text = value.Name;
                    f.UserSurnameTB.Text = value.Surname;
                    f.UserPhoneTB.Text = value.Phone;
                    f.UserAddressTB.Text = value.Address;
                    f.UserEmailTB.Text = value.Email;
                    f.UserNotesTB.Text = value.Notes;

                    if (value.BookRents == null)
                        Task.Run(async () => value = await DatabaseManager.GetUser(value)).Wait();
                    FillBookRents(value.BookRents);
                }
                else
                {
                    foreach (Control c in f.UserGB.Controls) if (c.GetType() == typeof(TextBox)) (c as TextBox).Text = "";
                    f.BookRentsLW.Items.Clear();
                }

                f.DeleteUserButton.Visible = value != null;

                selectedUser = value;
                IsUserEditMode = false;
            }
        }

        private bool isUserEditMode;
        public bool IsUserEditMode
        {
            get => isUserEditMode; set
            {
                if (SelectedUser == null)
                {
                    Helper.ToggleCommonControls(f.UserGB.Controls, true);
                    isUserEditMode = false;
                    f.DeleteUserButton.Enabled = false;
                    return;
                }

                isUserEditMode = value;

                Helper.ToggleCommonControls(f.UserGB.Controls, value);

                f.AddUserButton.Text = value ? "Potrdi" : "Uredi";
                f.CancelUserEditButton.Text = value ? "Prekliči" : "Počisti";
                f.DeleteUserButton.Enabled = value;
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
                });

                f.BookRentsLW.Items.Add(row);
            }
        }

        public async void AddUser()
        {
            if(ValidateUser() != UserValidationResult.OK)
            {
                MessageBox.Show("Izpolnite vsa potrebna polja.");
                return;
            }

            var u = new User()
            {
                Name = f.UserNameTB.Text,
                Surname = f.UserSurnameTB.Text,
                Phone = f.UserPhoneTB.Text,
                Address = f.UserAddressTB.Text,
                Email = f.UserEmailTB.Text,
                Notes = f.UserNotesTB.Text
            };

            if(await DatabaseManager.AddUser(u))
            {
                MessageBox.Show("Uporabnik uspešno dodan.");
                SelectedUser = u;
                return;
            }

            MessageBox.Show("Napaka pri dodajanju uporabnika.");
        }

        public async void EditUser()
        {
            if (!IsUserEditMode)
            {
                IsUserEditMode = true;
                return;
            }

            if(ValidateUser() != UserValidationResult.OK)
            {
                MessageBox.Show("Izpolnite vsa potrebna polja.");
                return;
            }

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
                return;
            }

            else MessageBox.Show("Napaka!");
        }

        public async void DeleteUser()
        {
            if (!IsUserEditMode) return;

            var a = MessageBox.Show("Izbris uporabnika bo izbrisal tudi vse podatke, povezane z njim.\nDejanja ni mogoče razveljaviti.", "Brisanje uporabnika.", MessageBoxButtons.YesNo);
            if (a != DialogResult.Yes) return;

            if(await DatabaseManager.DeleteUser(SelectedUser))
            {
                MessageBox.Show("Brisanje uporabnika uspešno.");
                SelectedUser = null;
                return;
            }

            MessageBox.Show("Napaka pri brisanju uporabnika.");
        }

        public async void ReturnBooks()
        {
            if (f.BookRentsLW.SelectedItems.Count < 1) return;

            var returns = new List<BookRent>();
            foreach (int i in f.BookRentsLW.SelectedIndices)
            {
                returns.Add(SelectedUser.BookRents[i]);
            }

            await DatabaseManager.ReturnBooks(returns);

            SelectedUser = await DatabaseManager.GetUser(SelectedUser);
        }

        private UserValidationResult ValidateUser()
        {
            if (f.UserNameTB.Text == "" || f.UserSurnameTB.Text == "" || f.UserAddressTB.Text == "" || f.UserPhoneTB.Text == "")
            {
                return UserValidationResult.MissingInfo;
            }
            return UserValidationResult.OK;
        }

        private enum UserValidationResult
        {
            OK = 0,
            MissingInfo = 1,
        }

        #endregion

        #region book loaning tab

        private List<BookRent> newBookLoans;

        private int oldDeadlineMode;

        private DateTime cachedDeadline;
        private DateTime Deadline
        {
            get => f.DeadlineDateTimePicker.Value;
            set
            {
                f.DeadlineDateTimePicker.Value = value;
            }
        }

        private BookCopy selectedBookCopy;
        private BookCopy SelectedBookCopy
        {
            get => selectedBookCopy;
            set
            {
                if (value != null && value.Book == null) Task.Run(async () => value = await DatabaseManager.GetBookCopy(value)).Wait();

                selectedBookCopy = value;

                f.BookTitleLabel.Text = value != null ? value.Book.Title : "/";

                f.AddBookLoanButton.Enabled = value != null;
            }
        }

        public void BookQuery()
        {
            cancelBookQuerySource.Cancel();
            Task.Run(async () =>
            {
                Thread.Sleep(500);
                SelectedBookCopy = await DatabaseManager.GetBookCopy(f.LoanBookCodeTB.Text);
            }, cancelBookQuery);
        }

        public void AddBookLoan()
        {
            if (SelectedBookCopy != null && SelectedUser != null)
            {
                var l = new BookRent()
                {
                    BookCopyID = SelectedBookCopy.ID,
                    UserID = SelectedUser.ID,
                    RentDate = DateTime.Now,
                    DeadLine = Deadline,
                };

                newBookLoans.Add(l);
                f.AddedBookLoansLW.Items.Add(new ListViewItem(new string[]
                {
                    SelectedBookCopy.Code,
                    SelectedBookCopy.Book.Title,
                    l.DeadLine.ToShortDateString()
                }));
            }
        }

        public void DeadlineSelectionChanged()
        {
            f.DeadlineDateTimePicker.Enabled = f.DeadlineSelectionCB.SelectedIndex == 0;
            if (oldDeadlineMode == 0) cachedDeadline = Deadline;

            oldDeadlineMode = f.DeadlineSelectionCB.SelectedIndex;

            switch (f.DeadlineSelectionCB.SelectedIndex)
            {
                case 0: // custom value
                    Deadline = cachedDeadline;
                    break;
                case 1: // 1 work week
                    Deadline = Helper.AddBusinessDays(DateTime.Now, 5);
                    break;
                case 2: // 2 work weeks
                    Deadline = Helper.AddBusinessDays(DateTime.Now, 10);
                    break;
                case 3: // 3 work weeks
                    Deadline = Helper.AddBusinessDays(DateTime.Now, 15);
                    break;
                case 4: //4 work weeks
                    Deadline = Helper.AddBusinessDays(DateTime.Now, 20);
                    break;
                case 5:
                    Deadline = DateTime.Now.AddYears(1);
                    break;
            }

        }

        public void ConfirmBookLoan()
        {
            string booklist = "";

            foreach (var b in newBookLoans)
            {
                booklist += $"{b.BookCopy.Book.Title}\n";
            }

            var a = MessageBox.Show($"Posodili boste naslednje knjige: \n{booklist}", "Posoja knjig", MessageBoxButtons.OKCancel);

            if (a == DialogResult.OK)
            {
                Task.Run(async () => await DatabaseManager.LoanBooks(newBookLoans)).Wait();
            }
        }

        #endregion

    }
}
