using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;
using Database;

namespace Books.SubWindows
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void Login(string email, string password)
        {
            var l = await DatabaseManager.LibrarianLogin(email, password);

            if (l == null)
            {
                MessageBox.Show("Preverite prijavne podatke.", "Prijava neuspešna");
                return;
            }

            Hide();
            var f = new MainForm(l);
            f.FormClosed += F_FormClosed;


            f.Show();
        }


        private void LoginButton_Click(object sender, EventArgs e)
        {
            Login(LoginEmailTB.Text, LoginPasswordTB.Text);            
        }


        private void F_FormClosed(object sender, FormClosedEventArgs e)
        {
            Show();
        }

        private void DeleteDBButton_Click(object sender, EventArgs e)
        {
            DatabaseManager.ResetDatabase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login("test@test.si", "test");
        }
    }
}
