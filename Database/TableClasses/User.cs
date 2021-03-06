using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Database.TableClasses
{
    [Table("users")]
    public class User
    {
        [Column("id"), AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        [Column("name"), NotNull]
        public string Name { get; set; }
        [Column("surname"), NotNull]
        public string Surname { get; set; }
        [Column("phone"), Unique, NotNull]
        public string Phone { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("email"), Unique]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("notes")]
        public string Notes { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BookLoan> BookLoans { get; set; }

    }
}
