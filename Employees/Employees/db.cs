using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Employees.DB
{
    class mysql
    {
        public static db db = new db();
    }

    class db
    {
        public string mysql_host = "";
        public string mysql_user = "";
        public string mysql_password = "";
        public string mysql_database = "";
        public MySqlDataReader reader;
        public MySqlConnection connection = new MySqlConnection();

        public bool connect()
        {
            string connection_credentials;
            connection_credentials = "server = " + this.mysql_host +
                                     ";username = " + this.mysql_user +
                                     ";password = " + this.mysql_password +
                                     ";database =" + this.mysql_database;
            this.connection.ConnectionString = connection_credentials;

            try
            {
                connection.Open();

                return true;
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);

                return false;
            }
        }

        //INSERT, UPDATE, DELETE
        public void executeNonReader(string sql)
        {
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = this.connection;
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
            }
        }

        //SELECT
        public MySqlDataReader execute(string sql)
        {
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = this.connection;
                command.CommandText = sql;
                this.reader = command.ExecuteReader();
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
            }

            return this.reader;
        }

        ~db()
        {
            this.connection.Close();
        }
    }
}
