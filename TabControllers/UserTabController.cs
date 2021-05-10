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
using System.Diagnostics;

namespace Books.TabControllers
{
    public class UserTabController
    {
        private MainForm f;

        public UserTabController(MainForm form)
        {
            f = form;

            SelectedUser = null;

            newBookLoans = new List<BookLoan>();

            cachedDeadline = DateTime.Now;

            f.DeadlineSelectionCB.SelectedIndex = 4;
            oldDeadlineMode = 4;

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
            get => selectedUser;
            set
            {
                f.BorrowedBooksLW.Items.Clear();

                if (value != null)
                {
                    f.UserNameTB.Text = value.Name;
                    f.UserSurnameTB.Text = value.Surname;
                    f.UserPhoneTB.Text = value.Phone;
                    f.UserAddressTB.Text = value.Address;
                    f.UserEmailTB.Text = value.Email;
                    f.UserNotesTB.Text = value.Notes;

                    f.AddUserButton.Text = "Uredi";

                    if (value.BookLoans == null) Task.Run(async () => value = await DatabaseManager.GetUser(value)).Wait();
                    FillBookRents(value.BookLoans);
                }
                else
                {
                    foreach (Control c in f.UserGB.Controls) if (c.GetType() == typeof(TextBox)) (c as TextBox).Text = "";
                    f.AddUserButton.Text = "Dodaj";
                }

                f.DeleteUserButton.Visible = value != null;
                f.CancelUserEditButton.Text = "Počisti";

                selectedUser = value;
                IsUserEditMode = false;
            }
        }

        private bool isUserEditMode;
        public bool IsUserEditMode
        {
            get => isUserEditMode;
            set
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

        private void FillBookRents(List<BookLoan> rents)
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

                f.BorrowedBooksLW.Items.Add(row);
            }
        }

        public async void AddUser()
        {
            if (ValidateUser() != UserValidationResult.OK)
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

            if (await DatabaseManager.AddUser(u))
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

            if (ValidateUser() != UserValidationResult.OK)
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

            if (await DatabaseManager.DeleteUser(SelectedUser))
            {
                MessageBox.Show("Brisanje uporabnika uspešno.");
                SelectedUser = null;
                return;
            }

            MessageBox.Show("Napaka pri brisanju uporabnika.");
        }

        public async void ReturnBooks()
        {
            if (f.BorrowedBooksLW.SelectedItems.Count < 1) return;

            var returns = new List<BookLoan>();
            foreach (int i in f.BorrowedBooksLW.SelectedIndices)
            {
                returns.Add(SelectedUser.BookLoans[i]);
            }

            await DatabaseManager.ReturnBooks(returns);

            SelectedUser = await DatabaseManager.GetUser(SelectedUser);
        }

        public async void ExtendBooks()
        {
            if (f.BorrowedBooksLW.SelectedItems.Count < 1) return;

            var returns = new List<BookLoan>();
            foreach (int i in f.BorrowedBooksLW.SelectedIndices)
            {
                var item = SelectedUser.BookLoans[i];
                if (((DeadlineMode)item.DeadlineMode) != DeadlineMode.Extended) returns.Add(item);
            }

            await DatabaseManager.ExtendDeadlines(returns);

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

        private List<BookLoan> newBookLoans;

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
                f.LoanBookCodeTB.Text = value != null ? value.Code : "";
                f.AddBookLoanButton.Enabled = value != null;
            }
        }

        private BookLoan selectedBookLoan;
        public BookLoan SelectedBookLoan
        {
            get => selectedBookLoan;
            set
            {
                selectedBookLoan = value;

                IsBookLoanEditMode = false;

                if (value != null)
                {
                    SelectedBookCopy = value.BookCopy;

                    var dm = (DeadlineMode)value.DeadlineMode;
                    if (dm != DeadlineMode.Extended) f.DeadlineSelectionCB.SelectedIndex = (int)dm;

                    Deadline = value.DeadLine;

                }
                else
                {
                    SelectedBookCopy = null;
                    f.DeadlineSelectionCB.SelectedIndex = 4;
                }

                f.AddBookLoanButton.Text = value != null ? "Uredi" : "Dodaj";
                f.CancelBookLoanEditButton.Text = "Počisti";
                f.RemoveBookLoanButton.Visible = value != null;
            }
        }

        private bool isBookLoanEditMode;
        public bool IsBookLoanEditMode
        {
            get => isBookLoanEditMode;
            set
            {
                if (SelectedBookLoan == null)
                {
                    Helper.ToggleCommonControls(f.BookLoanGB.Controls, true);
                    isBookLoanEditMode = false;
                    f.RemoveBookLoanButton.Enabled = false;
                    f.LoanBookCodeTB.ReadOnly = false;
                    return;
                }

                isBookLoanEditMode = value;

                f.LoanBookCodeTB.ReadOnly = true;

                Helper.ToggleCommonControls(f.BookLoanGB.Controls, value);

                f.AddBookLoanButton.Text = value ? "Potrdi" : "Uredi";
                f.CancelBookLoanEditButton.Text = value ? "Prekliči" : "Počisti";
                f.RemoveBookLoanButton.Enabled = value;
            }
        }

        public async void BookQuery()
        {
            var bc = await DatabaseManager.GetBookCopy(f.LoanBookCodeTB.Text, true);
            if (bc != SelectedBookCopy && !newBookLoans.Exists(bl => bc.ID == bl.BookCopy.ID)) SelectedBookCopy = bc;
        }

        public void AddBookLoan()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Uporabnik ni izbran.");
                return;
            }

            if (SelectedBookCopy == null)
            {
                MessageBox.Show("Izvod ni izbran.");
                return;
            }

            var l = new BookLoan()
            {
                BookCopyID = SelectedBookCopy.ID,
                BookCopy = SelectedBookCopy,
                UserID = SelectedUser.ID,
                User = SelectedUser,
                RentDate = DateTime.Now,
                DeadLine = Deadline,
                DeadlineMode = f.DeadlineSelectionCB.SelectedIndex
            };

            SelectedBookCopy = null;
            AddBookLoanToList(l);
            BookQuery();
        }

        public void EditBookLoan()
        {
            if (!IsBookLoanEditMode)
            {
                IsBookLoanEditMode = true;
                return;
            }

            if (SelectedUser == null)
            {
                MessageBox.Show("Uporabnik ni izbran.");
                return;
            }

            if (SelectedBookCopy == null)
            {
                MessageBox.Show("Izvod ni izbran.");
                return;
            }

            SelectedBookLoan.DeadLine = Deadline;
            SelectedBookLoan.DeadlineMode = f.DeadlineSelectionCB.SelectedIndex;
            SelectedBookLoan.RentDate = DateTime.Now;

            AddBookLoanToList(SelectedBookLoan);
            SelectedBookCopy = null;
        }

        private async void AddBookLoanToList(BookLoan loan)
        {
            int i = newBookLoans.IndexOf(loan);

            if (loan.BookCopy == null || loan.BookCopy.Book == null)
            {
                if (loan.ID > 0) loan = await DatabaseManager.GetBookLoan(loan);
                else loan.BookCopy = await DatabaseManager.GetBookCopy(new BookCopy() { ID = loan.BookCopyID });
            }

            var n = new ListViewItem(
               new string[]
               {
                    loan.BookCopy.Code,
                    loan.BookCopy.Book.Title,
                    loan.DeadLine.ToShortDateString()
               });

            if (i >= 0)
            {
                f.AddedBookLoansLW.Items.RemoveAt(i);
                f.AddedBookLoansLW.Items.Insert(i, n);
            }
            else
            {
                newBookLoans.Add(loan);
                f.AddedBookLoansLW.Items.Add(n);
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

        public async void ConfirmBookLoans()
        {
            string booklist = "";

            foreach (var b in newBookLoans)
            {
                booklist += $"{b.BookCopy.Book.Title}\n";
            }

            var a = MessageBox.Show($"Posodili boste naslednje knjige: \n{booklist}", "Posoja knjig", MessageBoxButtons.OKCancel);
            if (a != DialogResult.OK) return;

            await DatabaseManager.LoanBooks(newBookLoans);

            newBookLoans.Clear();
            f.AddedBookLoansLW.Items.Clear();
            SelectedUser = await DatabaseManager.GetUser(SelectedUser);
        }

        private enum DeadlineMode
        {
            Custom = 0,
            OneWorkWeek = 1,
            TwoWorkWeeks = 2,
            ThreeWorkWeeks = 3,
            FourWorkWeeks = 4,
            OneYear = 5,
            Extended = 6
        }

        #endregion
    }
}