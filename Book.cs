using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    public class Book
    {
        private string Author, Publisher, Type, Number_Id, Aquired;
        private bool Borrowed;
        private DateTime Date;

        public Book()
        {

        }

        public Book(string author, string publisher, string type, string number_id, string aquired, bool borrowed, DateTime date)
        {
            Author = author;
            Publisher = publisher;
            Type = type;
            Number_Id = number_id;
            Aquired = aquired;
            Borrowed = borrowed;
            Date = date; 
        }
    }
}
