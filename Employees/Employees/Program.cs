using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Employees.DB;
using Employees.LOGIN;

namespace Employees
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

            mysql.db.mysql_host = "localhost";
            mysql.db.mysql_user = "root";
            mysql.db.mysql_password = "";
            mysql.db.mysql_database = "";

            if (mysql.db.connect()) {
                Application.Run(new Login());
            } else {
                Application.Exit();
            }
        }
    }
}
