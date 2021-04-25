using Database;
using Database.TableClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;

namespace Books.TabControllers
{
    public class SettingsTabController
    {
        private MainForm f;

        private Librarian admin;
        public Librarian Admin
        {
            get => admin;
            set
            {
                if (value == null)
                {
                    f.Close();
                    return;
                }

                IsEditMode = false;
                admin = value;

                f.LibrarianNameTB.Text = value.Name;
                f.LibrarianSurnameTB.Text = value.Surname;
                f.LibrarianEmailTB.Text = value.Email;
                f.LibrarianPhoneTB.Text = value.Phone;
                f.LibrarianAddressTB.Text = value.Address;

                f.LibrarianNewPassTB.Text = "";
                f.LibrarianPassChkTB.Text = "";
                f.LibrarianSecurityPassTB.Text = "";
            }
        }

        public SettingsTabController(MainForm form, Librarian admin)
        {
            f = form;
            Admin = admin;
        }

        #region Info edit

        private bool isEditMode;
        public bool IsEditMode
        {
            get => isEditMode;
            set
            {
                isEditMode = value;


                f.CancelLibrarianChangeButton.Visible = value;

                f.LibrarianInfoGB.Enabled = value;
                f.LibrarianPasswordChangeGB.Enabled = value;

                f.ChangeLibrarianDataButton.Text = value ? "Potrdi spremembe" : "Spremeni";
                f.CancelLibrarianChangeButton.Enabled = value;
            }
        }

        public async Task EditLibrarianInfo()
        {
            foreach (var c in f.LibrarianInfoGB.Controls)
            {
                if (c.GetType() == typeof(TextBox) && (c as TextBox).Text == "")
                {
                    MessageBox.Show("Izpolnite vsa polja.");
                    return;
                }
            }

            var newLib = new Librarian()
            {
                ID = Admin.ID,
                Name = f.LibrarianNameTB.Text,
                Surname = f.LibrarianSurnameTB.Text,
                Email = f.LibrarianEmailTB.Text,
                Phone = f.LibrarianPhoneTB.Text,
                Address = f.LibrarianAddressTB.Text,
                Password = Admin.Password
            };


            if (f.LibrarianNewPassTB.Text != "")
            {
                if (Helper.CreateMD5(f.LibrarianSecurityPassTB.Text) == Admin.Password)
                {
                    if (f.LibrarianNewPassTB.Text == f.LibrarianPassChkTB.Text)
                    {
                        newLib.Password = Helper.CreateMD5(f.LibrarianNewPassTB.Text);
                    }
                    else MessageBox.Show("Gesli se ne ujemata.");
                }
                else MessageBox.Show("Staro geslo je napačno.");
            }

            if (await DatabaseManager.UpdateLibrarian(newLib))
            {
                Admin = newLib;
            }
        }

        public async Task AddNewLibrarian()
        {
            if (Helper.CreateMD5(f.AddLibrarianSecurityPassTB.Text) == Admin.Password)
            {
                foreach (var c in f.NewLibrarianGB.Controls)
                {
                    if (c.GetType() == typeof(TextBox) && (c as TextBox).Text == "")
                    {
                        MessageBox.Show("Izpolnite vsa polja.");
                        return;
                    }
                }

                if (f.AddLibrarianPasswordTB.Text == "" || f.AddLibrarianPassChkTB.Text == "")
                {
                    MessageBox.Show("Izpolnite vsa polja za gesla.");
                    return;
                }

                if (f.AddLibrarianPasswordTB.Text != f.AddLibrarianPassChkTB.Text)
                {
                    MessageBox.Show("Gesli se ne ujemata.");
                    return;
                }

                var newLib = new Librarian()
                {
                    Name = f.AddLibrarianNameTB.Text,
                    Surname = f.AddLibrarianSurnameTB.Text,
                    Email = f.AddLibrarianEmailTB.Text,
                    Phone = f.AddLibrarianPhoneTB.Text,
                    Address = f.AddLibrarianAddressTB.Text,
                    Password = Helper.CreateMD5(f.AddLibrarianPasswordTB.Text)
                };

                if(await DatabaseManager.AddLibrarian(newLib))
                {
                    MessageBox.Show("Dodajanje novega knjižničarja uspešno.");

                    foreach (var c in f.NewLibrarianGB.Controls)
                    {
                        if (c.GetType() == typeof(TextBox))
                        {
                            (c as TextBox).Text = "";
                        }
                    }

                    foreach (var c in f.NewLibrarianPasswordGB.Controls)
                    {
                        if (c.GetType() == typeof(TextBox))
                        {
                            (c as TextBox).Text = "";
                        }
                    }


                    return;
                }
            }
            else MessageBox.Show("Vaše geslo je napačno.");
        }



        #endregion
    }
}
