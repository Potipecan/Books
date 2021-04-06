using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Database.TableClasses;
using System.IO;
using System.Diagnostics;

namespace Database
{
    public class DatabaseManager
    {
        private const string _databaseFilename = "library.sqlite";
        private string _path;

        private SQLiteAsyncConnection conn;
        

        public DatabaseManager()
        {
            _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _databaseFilename);
            Debug.WriteLine(_path);

            conn = new SQLiteAsyncConnection(_path, Constants.Flags);
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
