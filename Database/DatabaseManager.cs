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
using Utils;

namespace Database
{
    public static class DatabaseManager
    {
        private static readonly string _databaseFilename = "library.sqlite";
        private static readonly string _directory = "LibraryApp";
        private static readonly string _path;

        private static readonly SQLiteAsyncConnection conn;
        

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
                await conn.CreateTableAsync<Publisher>();
                await conn.CreateTableAsync<Librarian>();
            }).Wait();


            // adding test librarian
            Task.Run(async () =>
            {
                var l = new Librarian()
                {
                    Name = "Test",
                    Surname = "Test",
                    Email = "test@test.si",
                    Phone = "123456789",
                    Address = "Testna ulica 123, 9999 Testno mesto",
                    Password = Helper.CreateMD5("test")
                };

            if ((await conn.Table<Librarian>().Where(t => t.Email == l.Email && t.Password == l.Password).ToListAsync()).Count == 0)
                {
                    await conn.InsertAsync(l);
                }
            });
           
        }

        #region Administration

        public static async Task<Librarian> LibrarianLogin(string email, string password)
        {
            var l = await conn.Table<Librarian>().Where(a => a.Email == email && a.Password == password).ToListAsync();

            if (l.Count > 0) return l[0];
            return null;
        }

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

        #region Update functions

        public static async Task<bool> UpdateAuthor(Author author)
        {
            return await conn.UpdateAsync(author) == 1;
        }

        public static async Task<bool> UpdateUser(User user)
        {
            return await conn.UpdateAsync(user) == 1;
        }

        public static async Task<bool> UpdateBook(Book book)
        {
            if (book.Authors.Count < 1 || book.BookCopies.Count < 1 || book.Section == null) return false;

            await conn.UpdateWithChildrenAsync(book);

            return true;
        }

        public static async Task<bool> UpdateBookCopy(BookCopy bookcopy)
        {
            return await conn.UpdateAsync(bookcopy) == 1;
        }

        public static async Task<bool> UpdatePublisher(Publisher publisher)
        {
            return await conn.UpdateAsync(publisher) == 1;
        }

        #endregion

        #region Deletion functions

        public static async Task<bool> DeleteUser(User user)
        {
            await conn.DeleteAsync(user, true);
            return true;
        }

        public static async Task<bool> DeleteBook(Book book)
        {
            await conn.DeleteAsync(book, true);
            return true;
        }

        public static async Task<bool> DeleteAuthor(Author author)
        {
            await conn.DeleteAsync(author, true);
            return true;
        }

        public static async Task<bool> DeletePublisher(Publisher publisher)
        {
            await conn.DeleteAsync(publisher, true);
            return true;
        }

        public static async Task<bool> DeleteBookCopy(BookCopy bookcopy)
        {
            return await conn.DeleteAsync(bookcopy) == 1;
        }

        #endregion

        #region Get functions

        /// <summary>
        /// Retrieves a list of books by section and author.
        /// </summary>
        /// <param name="section">Section to look by. Leave null to ignore search parameter.</param>
        /// <param name="author">Author to look by. Leave null to ignore search parameter.</param>
        /// <returns>A list of books that match the search parameters.</returns>
        public static async Task<List<Book>> GetBooks(Section section = null, Author author = null)
        {
            List<Book> res = null;

            res = (await conn.GetAllWithChildrenAsync<Book>()).FindAll(b => 
            (section == null || b.SectionID == section.ID) && 
            (author == null || b.Authors.Contains(author)
            ));

            return res;
        }

        /// <summary>
        /// Retrieves a list of books by title.
        /// </summary>
        /// <param name="title">Book title to search by.</param>
        /// <returns>A list of books with connected properties.</returns>
        public static async Task<List<Book>> GetBooks(string title)
        {
            title = title.ToLower();
            return await conn.GetAllWithChildrenAsync<Book>(b => b.Title.ToLower().Contains(title));
        }

        /// <summary>
        /// Gets a book and its connected properties.
        /// </summary>
        /// <param name="book">Book to retrieve.</param>
        /// <returns>A book object with all its connected properties.</returns>
        public static async Task<Book> GetBook(Book book)
        {
            return await conn.GetWithChildrenAsync<Book>(book);
        }

        /// <summary>
        /// Gets a book copy by code.
        /// </summary>
        /// <param name="code">Code search parameter.</param>
        /// <returns>A book copy that matches the search parameters with all its connected properties.</returns>
        public static async Task<BookCopy> GetBookCopy(string code)
        {
            var b = (await conn.GetAllWithChildrenAsync<BookCopy>(bc => bc.Code == code, true));

            if (b.Count > 0) return b[0];
            return null;
        }

        public static async Task<List<Author>> GetAuthors(string name = "")
        {
            name = name.ToLower();
            var res = await conn.Table<Author>().Where(a => a.Name.ToLower().Contains(name)).ToListAsync();
            return res;
        }

        public static async Task<List<User>> GetUsers(string search)
        {
            var res = new List<User>();

            var lowerName = search.ToLower().Replace(" ", string.Empty);

            res = await conn.Table<User>().Where(u =>
            u.Name.ToLower().Replace(" ", string.Empty).Contains(lowerName) ||
            u.Phone == search ||
            u.Email.Contains(lowerName)
            ).ToListAsync();

            return res;
        }

        public static async Task<User> GetUser(User user)
        {
            return await conn.GetWithChildrenAsync<User>(user, true);
        }

        #endregion

        public static async Task ReturnBook(BookRent bookrent)
        {
            var r = new BookRentRecord()
            {
                BookCopyID = bookrent.BookCopyID,
                RentDate = bookrent.RentDate,
                DeadLine = bookrent.DeadLine,
                RetrunDate = DateTime.Now,
                UserID = bookrent.UserID,
            };

            await conn.DeleteAsync(bookrent);
            await conn.InsertAsync(r);
        }

        public static async Task ReturnBooks(List<BookRent> bookrents)
        {
            var rs = new List<BookRentRecord>();

            foreach(var bookrent in bookrents)
            {
                rs.Add(new BookRentRecord()
                {
                    BookCopyID = bookrent.BookCopyID,
                    RentDate = bookrent.RentDate,
                    DeadLine = bookrent.DeadLine,
                    RetrunDate = DateTime.Now,
                    UserID = bookrent.UserID,
                });
            }

            await conn.DeleteAllAsync(bookrents);
            await conn.InsertAllAsync(rs);
        }

        public static async Task<List<BookRentRecord>> GetUserBookLoanArchive(User user)
        {
            var res = new List<BookRentRecord>();

            res = await conn.Table<BookRentRecord>().Where(b => b.UserID == user.ID).ToListAsync();

            return res;
        }

        public static async Task LoanBooks(List<BookRent> bookrents)
        {
            await conn.InsertAllAsync(bookrents);
        }
    }
}
