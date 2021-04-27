﻿using System;
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
            if(utc != null) utc.DeadlineSelectionChanged();
        }

        private void ConfirmBookLoanButton_Click(object sender, EventArgs e)
        {
            utc.ConfirmBookLoan();
        }



        #endregion

        #endregion

        #region Materials page

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
            if (mtc.CurrentBook != null) mtc.UpdateBook();
            else mtc.AddBook();
        }

        private void AddAuthorBookCB_Click(object sender, EventArgs e)
        {
            mtc.SelectAuthor();
        }

        private void AddBookCopyButton_Click(object sender, EventArgs e)
        {
            mtc.AddBookCopy();
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

        private void AddPublisherButton_Click(object sender, EventArgs e)
        {
            if (mtc.SelectedPublisher == null) mtc.AddPubliser();
            else mtc.EditPublisher();
        }
    }
}
