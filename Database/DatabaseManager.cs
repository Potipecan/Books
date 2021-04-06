using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Database.TableClasses;
using System.IO;
using System.Diagnostics;
using SQLiteNetExtensionsAsync.Extensions;

namespace Database
{
    public static class DatabaseManager
    {
        private static string _databaseFilename = "library.sqlite";
        private static string _directory = "LibraryApp";
        private static string _path;

        private static SQLiteAsyncConnection conn;
        

        static DatabaseManager()
        {
            _directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _directory);

            if (!Directory.Exists(_directory)) Directory.CreateDirectory(_directory);

            _path = Path.Combine(_directory, _databaseFilename);

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

        #region Administration

        public static bool DeleteDatabase()
        {
            try
            {
                if (File.Exists(_path)) File.Delete(_path);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Add functions

        public static async Task<bool> AddUser(User user)
        {
            int i = await conn.InsertAsync(user);

            return i == 1;
        }

        public static async Task<bool> AddAuthor(Author author)
        {
            return await conn.InsertAsync(author) == 1;
        }

        public static async Task<bool> AddBook(Book book)
        {
            if (book.BookCopies.Count < 1 || book.Section == null || book.Authors.Count < 1) return false;

            await conn.InsertWithChildrenAsync(book, true);
            return true;
        }

        public static async Task<bool> AddSection(Section section)
        {
            return await conn.InsertAsync(section) == 1;
        }

        public static async Task<bool> AddBookCopy(BookCopy bookcopy)
        {
            if (bookcopy.Book == null) return false;

            return await conn.InsertAsync(bookcopy) == 1;
        }

        public static async Task<bool> AddBookRent(BookRent bookrent)
        {
            if (bookrent.BookCopy == null || bookrent.User == null) return false;
            return await conn.InsertAsync(bookrent) == 1;
        }

        public static async Task<bool> AddPublisher(Publisher publisher)
        {
            return await conn.InsertAsync(publisher) == 1;
        }

        #endregion

        #region

        #endregion
    }
}
