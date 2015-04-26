using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Employees.LOGIN;
using Employees.EDIT;
using Employees.QUERIES;

namespace Employees.LIST
{
    public partial class List : Form
    {
        public List()
        {
            InitializeComponent();
        }

        private void ToolStripMenuItemLogout_Click(object sender, EventArgs e)
        {
            this.Hide();

            Login showLogin = new Login();
            showLogin.ShowDialog();

            this.Close();
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBoxSearchKeyword_TextChanged(object sender, EventArgs e)
        {
            queries action = new queries();
            action.searchEmployee(this.listView1, this.textBoxSearchKeyword.Text);

            this.refreshEmployeesCount();
        }

        private void List_Load(object sender, EventArgs e)
        {
            this.refreshList();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit showEdit = new Edit();
            showEdit.list = this;
            showEdit.ShowDialog();
        }

        public void refreshList()
        {
            queries action = new queries();
            action.loadList(this.listView1);

            this.refreshEmployeesCount();
        }

        public void refreshEmployeesCount()
        {
            this.labelEmployeesCount.Text = "Employees found: " + this.listView1.Items.Count;
        }

        private void loadEdit()
        {
            try
            {
                int selectedID = Convert.ToInt32(this.listView1.SelectedItems[0].Text);
                Edit showEdit = new Edit();
                showEdit.list = this;
                showEdit.selectedID = selectedID;
                showEdit.ShowDialog();
            }
            catch { }
        }

        private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            this.loadEdit();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.loadEdit();
        }

        private void toolStripMenuItemRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedID = Convert.ToInt32(this.listView1.SelectedItems[0].Text);
                queries action = new queries();
                action.id = selectedID;
                action.removeEmployee();

                this.refreshList();
            }
            catch { }
        }
    }
}
