using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Books.TabControllers;
using Utils;
using Database;
using Database.TableClasses;

namespace Books
{
    public partial class MainForm : Form
    {
        private SearchTabController stc;
        private UserTabController utc;
        public SettingsTabController settc;
        private MaterialTabController mtc;

        public MainForm(Librarian librarian)
        {
            InitializeComponent();

            stc = new SearchTabController(this);
            utc = new UserTabController(this);
            settc = new SettingsTabController(this, librarian);
            mtc = new MaterialTabController(this);
        }

        #region SearchPage

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseManager.ResetDatabase();
        }

        private async void MainSearchButton_Click(object sender, EventArgs e)
        {
            await stc.Search();
        }

        private void SearchResultLW_DoubleClick(object sender, EventArgs e)
        {
            stc.InspectSearchResult();
        }

        #endregion

        #region User page

        public async void GoToUserPage(User user)
        {
            await utc.GoHere(user);
        }

        #region User details

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            if (utc.SelectedUser != null) utc.EditUser();
            else utc.AddUser();
        }

        private void CancelUserEditButton_Click(object sender, EventArgs e)
        {
            if (utc.IsUserEditMode) utc.SelectedUser = utc.SelectedUser;
            else utc.SelectedUser = null;
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            utc.DeleteUser();
        }


        private void ReturnBookOneMemberButton_Click(object sender, EventArgs e)
        {
            utc.ReturnBooks();
        }

        #endregion

        #region Book loans


        private void LoanBookCodeTB_TextChanged(object sender, EventArgs e)
        {
            BookCopyQueryTimer.Start();
        }

        private void DeadlineSelectionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(utc != null) utc.DeadlineSelectionChanged();
        }

        private void ConfirmBookLoanButton_Click(object sender, EventArgs e)
        {
            utc.ConfirmBookLoan();
        }



        #endregion

        #endregion

        #region Materials page

        public void GoToMaterialsPage(object o)
        {
            switch (o.GetType().Name)
            {
                case "Book":
                    mtc.GoHere(o as Book);
                    break;
                case "Author":
                    mtc.GoHere(o as Author);
                    break;
                case "Publisher":
                    mtc.GoHere(o as Publisher);
                    break;
                case "Section":
                    mtc.GoHere(o as Section);
                    break;

                default:
                    throw new ArgumentException("Argument can only be of Book, Publisher, Section or Author class");
            }
        }

        public void AuthorSelectionCB_TextChanged(object sender, EventArgs e)
        {
            mtc.AuthorQuery();
        }

        private void AddAuthorButton_Click(object sender, EventArgs e)
        {
            if (mtc.SelectedAuthor != null) mtc.EditAuthor();
            else mtc.AddAuthor();
        }

        private void CancelAuthorEditButton_Click(object sender, EventArgs e)
        {
            if (mtc.IsAuthorEditMode) mtc.IsAuthorEditMode = false;
            else mtc.SelectedAuthor = null;
        }

        private void AddSectionButton_Click(object sender, EventArgs e)
        {
            if (mtc.SelectedSection != null) mtc.EditSection();
            else mtc.AddSection();
        }

        private void CancelSectionEditButton_Click(object sender, EventArgs e)
        {
            if (mtc.IsSectionEditMode) mtc.IsSectionEditMode = false;
            else mtc.SelectedSection = null;
        }

        private void BookConfirmationButton_Click(object sender, EventArgs e)
        {
            if (mtc.SelectedBook != null) mtc.UpdateBook();
            else mtc.AddBook();
        }

        private void AddAuthorBookCB_Click(object sender, EventArgs e)
        {
            mtc.SelectAuthor();
        }

        private void AddBookCopyButton_Click(object sender, EventArgs e)
        {
            if (mtc.SelectedBookCopy == null) mtc.AddBookCopy();
            else mtc.UpdateBookCopy();
        }

        private void AddPublisherButton_Click(object sender, EventArgs e)
        {
            if (mtc.SelectedPublisher == null) mtc.AddPublisher();
            else mtc.EditPublisher();
        }

        private void CancelBookEditButton_Click(object sender, EventArgs e)
        {
            if (mtc.IsBookEditMode) mtc.SelectedBook = mtc.SelectedBook;
            else mtc.SelectedBook = null;
        }

        private void DeleteBookButton_Click(object sender, EventArgs e)
        {
            mtc.DeleteBook();
        }

        private void BookCopyLW_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtc.BookCopySelectionChanged();
        }

        private void CancelBookCopyEditButton_Click(object sender, EventArgs e)
        {
            if (mtc.SelectedBookCopy == null) mtc.SelectedBookCopy = null;
            else if (mtc.IsBookCopyEditMode) mtc.SelectedBookCopy = mtc.SelectedBookCopy;
            else mtc.SelectedBookCopy = null;
        }

        private void RemoveBookCopyButton_Click(object sender, EventArgs e)
        {
            mtc.RemoveBookCopy();
        }

        private void DeleteAuthorButton_Click(object sender, EventArgs e)
        {
            mtc.DeleteAuthor();
        }


        #endregion

        #region Settings page

        private async void ChangeLibrarianDataButton_Click(object sender, EventArgs e)
        {
            if (settc.IsEditMode)
            {
                await settc.EditLibrarianInfo();
            }
            else settc.IsEditMode = true;
        }

        private void CancelLibrarianChangeButton_Click(object sender, EventArgs e)
        {
            settc.Admin = settc.Admin;
        }

        private async void ConfirmAddLibrarianButton_Click(object sender, EventArgs e)
        {
            await settc.AddNewLibrarian();
        }


        #endregion

        private void BookCopyQueryTimer_Tick(object sender, EventArgs e)
        {
            BookCopyQueryTimer.Stop();
            utc.BookQuery();
        }

        private void AddBookLoanButton_Click(object sender, EventArgs e)
        {
            if (utc.SelectedBookLoan == null) utc.AddBookLoan();
            else utc.
        }
    }
}
