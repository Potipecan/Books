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

namespace Books
{
    public partial class Form1 : Form
    { 

        public Form1()
        {
            InitializeComponent();
        }

        private async void SearchBookComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SearchBookComboBox.SelectedIndex == 0)
            {
                var books = await DatabaseManager.GetBooks();
                foreach (var item in books)
                {
                    string a = "";
                    foreach (var author in item.Authors)
                    {
                        var s = author.Name.Split(' ');
                        a += $"{s[0].Substring(0, 1)}. {s[1]}, ";
                    }
                    a = a.TrimEnd(',', ' ');


                    var row = new string[] { null, Convert.ToString(item.ID), item.Title, a, Convert.ToString(item.BookCopies.Count) };
                    var lvl = new ListViewItem(row);
                    listView1.Items.Add(lvl);
                }

            }
            else if(SearchBookComboBox.SelectedIndex == 1)
            {

            }
            else
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseManager.DeleteDatabase();
        }
    }
}
