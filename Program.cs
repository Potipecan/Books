using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Books.SubWindows;
using Database;

namespace Books
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            int c = 0;

            Task.Run(async () =>
            {
                c = await DatabaseManager.CountLibrarians();
            }).Wait();

            if (c <= 0) Application.Run(new FirstUserRegistration());

            Application.Run(new LoginForm());
        }
    }
}
