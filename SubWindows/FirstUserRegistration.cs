using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Database;
using Database.TableClasses;
using Utils;

namespace Books.SubWindows
{
    public partial class FirstUserRegistration : Form
    {
        public FirstUserRegistration()
        {
            InitializeComponent();
        }

        private async void ChangeLibrarianDataButton_Click(object sender, EventArgs e)
        {
            ChangeLibrarianDataButton.Enabled = false;
            if(LibrarianNameTB.Text == "" || LibrarianSurnameTB.Text == "" || LibrarianAddressTB.Text == "" || LibrarianEmailTB.Text == "" || LibrarianPhoneTB.Text == "")
            {
                MessageBox.Show("Izpolnite vsa polja.");
                return;
            }

            if(LibrarianNewPassTB.Text == "")
            {
                MessageBox.Show("Vpišite geslo.");
                return;
            }

            if(LibrarianNewPassTB.Text != LibrarianPassChkTB.Text)
            {
                MessageBox.Show("Gesli se ne ujemata.");
                return;
            }

            MessageBox.Show("Zapomnite si geslo!", "Opozorilo!");

            var u = new Librarian()
            {
                Name = LibrarianNameTB.Text,
                Surname = LibrarianSurnameTB.Text,
                Address = LibrarianAddressTB.Text,
                Phone = LibrarianPhoneTB.Text,
                Email = LibrarianEmailTB.Text,
                Password = Helper.CreateMD5(LibrarianNewPassTB.Text)
            };

            if(await DatabaseManager.AddLibrarian(u)){
                MessageBox.Show("Registracija uspešna.");

                new LoginForm().Show();
                Dispose();
            }

            MessageBox.Show("Napaka pri registraciji.");
        }
    }
}
