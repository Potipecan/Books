using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Database.TableClasses;

namespace Database
{
    public class DatabaseManager
    {
        private SQLiteAsyncConnection conn;

        public DatabaseManager()
        {
            conn = new SQLiteAsyncConnection("database.sqlite");
            Task.Run(async () =>
            {
                await conn.CreateTableAsync<User>();
                await conn.CreateTableAsync<Book>();
                await conn.CreateTableAsync<Section>();
                await conn.CreateTableAsync<Author>();
                await conn.CreateTableAsync<AuthorBook>();
                await conn.CreateTableAsync<BookCopy>();
                await conn.CreateTableAsync<BookRent>();
                await conn.CreateTableAsync<BookRentRecord>();
            }).Wait();
        }
    }
}
