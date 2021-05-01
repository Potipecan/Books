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
using System.Linq.Expressions;

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
                await conn.CreateTablesAsync(
                    CreateFlags.None,
                    typeof(User),
                    typeof(Book),
                    typeof(Author),
                    typeof(BookCopy),
                    typeof(Publisher),
                    typeof(Librarian),
                    typeof(BookLoan),
                    typeof(BookRentRecord),
                    typeof(AuthorBook),
                    typeof(Section)
                    );

                Initialize();
            });
        }

        #region Administration

        public static async Task<Librarian> LibrarianLogin(string email, string password)
        {
            password = Helper.CreateMD5(password);

            var l = await conn.Table<Librarian>().Where(a => a.Email == email && a.Password == password).ToListAsync();

            if (l.Count > 0) return l[0];
            return null;
        }

        public static async Task<bool> UpdateLibrarian(Librarian librarian)
        {
            return await conn.UpdateAsync(librarian) == 1;
        }

        public static async Task<bool> AddLibrarian(Librarian librarian)
        {
            return await conn.InsertAsync(librarian) == 1;
        }

        public static bool ResetDatabase()
        {
            try
            {
                if (File.Exists(_path)) File.Delete(_path);
                Initialize();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private static async void Initialize()
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

        public static async Task<bool> AddBookRent(BookLoan bookrent)
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

        public static async Task<bool> UpdateSection(Section section)
        {
            return await conn.UpdateAsync(section) == 1;
        }

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
            try
            {
                await conn.DeleteAsync(author, true);
            }
            catch (SQLiteException sqlex)
            {
                Debug.Fail(sqlex.Message);
                return false;
            }
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

        public static async Task<bool> DeleteSection(Section section)
        {
            try
            {
                await conn.DeleteAsync(section, true);
            }
            catch (SQLiteException sqlex)
            {
                Debug.Fail(sqlex.Message);
                return false;
            }
            return true;
        }

        #endregion

        #region Get functions

        public static async Task<string> GetNextBookCopyCode(int extra = 0)
        {
            string sql = "SELECT seq FROM sqlite_sequence WHERE name = 'book_copies';";

            int seq = await conn.ExecuteScalarAsync<int>(sql);

            return $"{(seq + extra + 1):D6}";
        }

        public static async Task<Section> GetSectionWithChildren(Section section)
        {
            return await conn.GetWithChildrenAsync<Section>(section.ID, true);
        }

        public static async Task<List<Publisher>> GetPublishers(string name = "")
        {
            bool search = name != "";
            name = name.Replace(" ", "").ToLower();

            var l = await conn.Table<Publisher>().Where(p => search || p.Name.Replace(" ", "").ToLower().Contains(name)).ToListAsync();

            return l;
        }

        public static async Task<bool> IsCodeUnique(string code)
        {
            int i = await conn.ExecuteScalarAsync<int>($"SELECT count(*) FROM book_copies WHERE code LIKE '%{code}%';");

            return i == 0;
        }

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
            title = title.ToLower().Replace(" ", "");
            return await conn.GetAllWithChildrenAsync<Book>(b => b.Title.ToLower().Replace(" ", "").Contains(title), true);
        }

        /// <summary>
        /// Gets a book and its connected properties.
        /// </summary>
        /// <param name="book">Book to retrieve.</param>
        /// <returns>A book object with all its connected properties.</returns>
        public static async Task<Book> GetBook(Book book)
        {
            return await conn.GetWithChildrenAsync<Book>(book.ID, true);
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

        public static async Task<BookCopy> GetBookCopy(BookCopy bookcopy)
        {
            return await conn.GetWithChildrenAsync<BookCopy>(bookcopy.ID, true);
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
            return await conn.GetWithChildrenAsync<User>(user.ID, true);
        }

        public static async Task<List<Section>> GetSections()
        {
            return await conn.Table<Section>().ToListAsync();
        }

        public static async Task<List<BookCopy>> GetBookCopies(string code)
        {
            var res = new List<BookCopy>();

            res = await conn.GetAllWithChildrenAsync<BookCopy>(bc => bc.Code.Contains(code), true);

            return res;
        }

        #endregion

        public static async Task ReturnBook(BookLoan bookrent)
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

        public static async Task ReturnBooks(List<BookLoan> bookrents)
        {
            var rs = new List<BookRentRecord>();

            foreach (var bookrent in bookrents)
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

        public static async Task LoanBooks(List<BookLoan> bookrents)
        {
            await conn.InsertAllAsync(bookrents);
        }
    }
}
