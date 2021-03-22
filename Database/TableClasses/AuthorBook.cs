using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.TableClasses
{
    [Table("authors_books")]
    class AuthorBook
    {
        [Column("author_id"), NotNull, ForeignKey(typeof(Author))]
        public int AuthorID { get; set; }
        [Column("book_id"), NotNull, ForeignKey(typeof(Book))]
        public int BookID { get; set; }
    }
}
