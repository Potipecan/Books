using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.TableClasses
{
    [Table("book_rent")]
    public class BookLoan
    {
        [Column("id"), PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Column("rent_date"), NotNull]
        public DateTime RentDate { get; set; }
        [Column("deadline"), NotNull]
        public DateTime DeadLine { get; set; }
        [Column("deadline_mode")]
        public int DeadlineMode { get; set; } = 0;

        [Column("book_copy_id"), NotNull, ForeignKey(typeof(BookCopy))]
        public int BookCopyID { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public BookCopy BookCopy { get; set; }

        [Column("user_id"), ForeignKey(typeof(User))]
        public int UserID { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }
    }
}
