using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Attributes;
using SQLitePCL;

namespace Database.TableClasses
{
    [Table("books")]
    public class Book
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int ID { get; set; }
        [Column("title"), NotNull]
        public string Title { get; set; }
        [Column("description"), NotNull]
        public string Description { get; set; }

        [Column("section_id"), NotNull, ForeignKey(typeof(Section))]
        public int SectionID { get; set; }
        [ManyToOne]
        public Section Section { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BookCopy> BookCopies { get; set; }

        [ManyToMany(typeof(AuthorBook))]
        public List<Author> Authors { get; set; }
    }
}
