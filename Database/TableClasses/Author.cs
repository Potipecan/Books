using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Database.TableClasses
{
    [Table("authors")]
    public class Author
    {
        [Column("id"), PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Column("name"), NotNull]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        
        [ManyToMany(typeof(AuthorBook))]
        public List<Book> Books { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
