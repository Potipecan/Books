using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Database.TableClasses;
using Database;

namespace Books.SubWindows
{
    public partial class UserBookLoanArchiveForm : Form
    {
        private User u;

        private List<BookRentRecord> records;

        public UserBookLoanArchiveForm(User user)
        {
            InitializeComponent();

            u = user;
            Text = $"Arhiv izposoje - {u.Name} {u.Surname}";

            Task.Run(async () => records = await DatabaseManager.GetUserBookLoanArchive(u)).Wait();

            ///TODO archive print
        }
    }
}
