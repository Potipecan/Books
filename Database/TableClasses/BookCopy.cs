using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.TableClasses
{
    [Table("book_copies")]
    public class BookCopy
    {
        [Column("id"), PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Column("code"), NotNull, Unique]
        public string Code { get; set; }
        [Column("year"), NotNull]
        public int Year { get; set; }
        [Column("acquisition_type"), NotNull]
        public int AcquisitionType { get; set; }
        [Column("acquisition_date"), NotNull]
        public DateTime AcquisitionDate { get; set; }
        [Column("state"), NotNull]
        public int State { get; set; } = (int)BookCopyState.Available;

        [Column("publisher_id"), ForeignKey(typeof(Publisher))]
        public int PublisherID { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Publisher Publisher { get; set; }

        [Column("book_id"), NotNull, ForeignKey(typeof(Book))]
        public int BookID { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Book Book { get; set; }

        public BookCopy() { }
        public BookCopy(BookCopy bookcopy)
        {
            ID = bookcopy.ID;
            //Code = bookcopy.Code;
            Year = bookcopy.Year;
            AcquisitionType = bookcopy.AcquisitionType;
            AcquisitionDate = bookcopy.AcquisitionDate;
            State = bookcopy.State;
            PublisherID = bookcopy.PublisherID;
            Publisher = bookcopy.Publisher;
            BookID = bookcopy.BookID;
            Book = bookcopy.Book;
        }
    }
}
