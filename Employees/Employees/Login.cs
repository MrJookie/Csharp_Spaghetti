using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Employees.DB;
using Employees.QUERIES;
using Employees.LIST;

namespace Employees.LOGIN
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (this.textBoxUsername.Text == "" || this.textBoxPassword.Text == "")
            {
                MessageBox.Show("All fields are required.");
            }
            else
            {
                queries action = new queries();

                if (action.AuthorizeUser(textBoxUsername.Text, textBoxPassword.Text))
                {
                    //MessageBox.Show("Login ok.");
                    this.Hide();

                    List showEmployees = new List();
                    showEmployees.ShowDialog();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error logging in.");
                    textBoxPassword.Text = "";
                }
            }
        }
    }
}
