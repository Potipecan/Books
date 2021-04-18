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
using Books.Utils;
using Database;
using Database.TableClasses;

namespace Books
{
    public partial class Form1 : Form
    {
        private SearchTabController stc;
        private UserTabController utc;

        public Form1()
        {
            InitializeComponent();

            stc = new SearchTabController(this);
            utc = new UserTabController(this);
        }

        #region SearchPage

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseManager.DeleteDatabase();
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

        private void EditUserButton_Click(object sender, EventArgs e)
        {
            if (utc.IsUserEditMode) utc.ConfirmUserEdit();
            else utc.InitiateUserEdit();
        }

        private void CancelEditButton_Click(object sender, EventArgs e)
        {
            utc.IsUserEditMode = false;
        }

        private void ReturnBookOneMemberButton_Click(object sender, EventArgs e)
        {
            utc.ReturnBooks();
        }

        #endregion

        #region Book loans


        private void LoanBookCodeTB_TextChanged(object sender, EventArgs e)
        {
            utc.BookQuery();
        }

        private void DeadlineSelectionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            utc.DeadlineSelectionChanged();
        }

        private void ConfirmBookLoanButton_Click(object sender, EventArgs e)
        {
            utc.ConfirmBookLoan();
        }



        #endregion

        #endregion

        #region Books page
        #endregion

    }
}
