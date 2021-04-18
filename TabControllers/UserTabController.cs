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
        private Form1 f;

        private CancellationTokenSource cancelBookQuerySource;
        private CancellationToken cancelBookQuery;

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
                selectedBookCopy = value;
                if (value != null)
                {
                    f.BookTitleLabel.Text = value.Book.Title;
                }
                else
                {
                    f.BookTitleLabel.Text = "/";
                }

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

            foreach(var b in newBookLoans)
            {
                booklist += $"{b.BookCopy.Book.Title}\n";
            }

            var a = MessageBox.Show($"Posodili boste naslednje knjige: \n{booklist}", "Posoja knjig", MessageBoxButtons.OKCancel);

            if(a == DialogResult.OK)
            {
                Task.Run(async () => await DatabaseManager.LoanBooks(newBookLoans)).Wait();
            }
        }

        #endregion

    }
}
