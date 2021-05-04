using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Database.TableClasses
{
    [Table("publishers")]
    public class Publisher
    {
        [Column("id"), PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Column("name")]
        public string Name { get; set; }

        [OneToMany]
        public List<BookCopy> BookCopies { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
