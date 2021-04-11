using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.TableClasses
{
    [Table("sections")]
    public class Section
    {
        [Column("id"), PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Column("name"), NotNull]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
    }
}
