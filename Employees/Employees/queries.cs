using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Employees.DB;

namespace Employees.QUERIES
{
    class queries
    {
        public int id = 0;
        public string name = "";
        public string surname = "";
        public string birth_date = "";
        public string gender = "";
        public string country = "";
        public string address = "";
        public string telephone = "";

        public bool AuthorizeUser(string user_name, string user_pass)
        {
            try
            {
                user_name = MySql.Data.MySqlClient.MySqlHelper.EscapeString(user_name);
                user_pass = MySql.Data.MySqlClient.MySqlHelper.EscapeString(user_pass);

                //source: http://stackoverflow.com/a/9790651
                string user_pass_sha1_hex = BitConverter.ToString(SHA1CryptoServiceProvider.Create().ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(user_pass))).Replace("-", "").ToLower();

                string sql = "SELECT * FROM csharp_accounts " +
                             "WHERE user_name = '" + user_name + "'" +
                             "AND user_pass = '" + user_pass_sha1_hex + "';";

                mysql.db.execute(sql);
                if (mysql.db.reader.HasRows)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                mysql.db.reader.Close();
            }

            return false;
        }

        public void loadList(ListView list)
        {
            try
            {
                list.Items.Clear();

                string sql = "SELECT * FROM csharp_employees;";

                mysql.db.execute(sql);
                if (mysql.db.reader.HasRows)
                {
                    while (mysql.db.reader.Read())
                    {
                        int i = list.Items.Count;
                        list.Items.Add(mysql.db.reader["id"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["name"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["surname"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["birth_date"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["gender"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["country"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["address"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["telephone"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                mysql.db.reader.Close();
            }
        }

        public void loadEmployee(int id)
        {
            try
            {
                string sql = "SELECT * FROM csharp_employees " +
                             "WHERE id = " + id + ";";

                mysql.db.execute(sql);
                if (mysql.db.reader.HasRows)
                {
                    mysql.db.reader.Read();
                    this.id = Convert.ToInt32(mysql.db.reader["id"]);
                    this.name = mysql.db.reader["name"].ToString();
                    this.surname = mysql.db.reader["surname"].ToString();
                    this.birth_date = mysql.db.reader["birth_date"].ToString();
                    this.gender = mysql.db.reader["gender"].ToString();
                    this.country = mysql.db.reader["country"].ToString();
                    this.address = mysql.db.reader["address"].ToString();
                    this.telephone = mysql.db.reader["telephone"].ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                mysql.db.reader.Close();
            }
        }

        public void searchEmployee(ListView list, string searchKeyword)
        {
            try
            {   
                list.Items.Clear();

                searchKeyword = MySql.Data.MySqlClient.MySqlHelper.EscapeString(searchKeyword);

                string sql = "SELECT * FROM csharp_employees " +
                             "WHERE name LIKE '" + searchKeyword + "%'" +
                             "OR surname LIKE '" + searchKeyword + "%'" +
                             "OR birth_date LIKE '" + searchKeyword + "%'" +
                             "OR gender LIKE '" + searchKeyword + "%'" +
                             "OR country LIKE '" + searchKeyword + "%'" +
                             "OR address LIKE '" + searchKeyword + "%'" +
                             "OR telephone LIKE '" + searchKeyword + "%';";

                mysql.db.execute(sql);
                if (mysql.db.reader.HasRows)
                {
                    while (mysql.db.reader.Read())
                    {
                        int i = list.Items.Count;
                        list.Items.Add(mysql.db.reader["id"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["name"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["surname"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["birth_date"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["gender"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["country"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["address"].ToString());
                        list.Items[i].SubItems.Add(mysql.db.reader["telephone"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                mysql.db.reader.Close();
            }
        }

        public void addEmployee()
        {
            try
            {
                this.name = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.name);
                this.surname = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.surname);
                this.birth_date = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.birth_date);
                this.gender = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.gender);
                this.country = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.country);
                this.address = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.address);
                this.telephone = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.telephone);

                string sql = "INSERT INTO csharp_employees " +
                             "VALUES ('null', " +
                             "'" + this.name + "', " +
                             "'" + this.surname + "', " +
                             "'" + this.birth_date + "', " +
                             "'" + this.gender + "', " +
                             "'" + this.country + "', " +
                             "'" + this.address + "', " +
                             "'" + this.telephone + "')";

                mysql.db.executeNonReader(sql);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                mysql.db.reader.Close();
            }
        }

        public void updateEmployee()
        {
            try
            {
                this.name = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.name);
                this.surname = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.surname);
                this.birth_date = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.birth_date);
                this.gender = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.gender);
                this.country = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.country);
                this.address = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.address);
                this.telephone = MySql.Data.MySqlClient.MySqlHelper.EscapeString(this.telephone);

                string sql = "UPDATE csharp_employees " +
                             "SET name = '" + this.name + "', " +
                             "surname = '" + this.surname + "', " +
                             "birth_date = '" + this.birth_date + "', " +
                             "gender = '" + this.gender + "', " +
                             "country = '" + this.country + "', " +
                             "address = '" + this.address + "', " +
                             "telephone = '" + this.telephone + "' " +
                             "WHERE id = " + this.id + ";";

                mysql.db.executeNonReader(sql);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                mysql.db.reader.Close();
            }
        }

        public void removeEmployee()
        {
            try
            {
                string sql = "DELETE FROM csharp_employees " +
                             "WHERE id = " + this.id + ";";

                mysql.db.executeNonReader(sql);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                mysql.db.reader.Close();
            }
        }
    }
}
