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
    class BookCopy
    {
        [Column("id"), PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Column("code"), NotNull, Unique]
        public string Code { get; set; }
        [Column("publisher"), NotNull]
        public string Publisher { get; set; }
        [Column("year"), NotNull]
        public int Year { get; set; }
        [Column("acquisition_type"), NotNull]
        public int AcquisitionType { get; set; }
        [Column("acquisition_date"), NotNull]
        public DateTime AcquisitionDate { get; set; }

        [Column("book_id"), NotNull, ForeignKey(typeof(Book))]
        public int BookID { get; set; }
        [ManyToOne]
        public Book Book { get; set; }
    }
}
