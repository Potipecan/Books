using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Database.TableClasses
{
    class User
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
    }
}
