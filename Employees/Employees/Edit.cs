using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Employees.LIST;
using Employees.QUERIES;
using System.Globalization;

namespace Employees.EDIT
{
    public partial class Edit : Form
    {
        public List list = new List();
        queries action = new queries();
        public int selectedID = 0;

        public Edit()
        {
            InitializeComponent();
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            if (this.selectedID > 0)
            {
                action.loadEmployee(this.selectedID);
                this.textBoxName.Text = action.name;
                this.textBoxSurname.Text = action.surname;
                this.textBoxBirthDate.Text = action.birth_date;
                this.comboBoxGender.Text = action.gender;
                this.textBoxCountry.Text = action.country;
                this.textBoxAddress.Text = action.address;
                this.textBoxTelephone.Text = action.telephone;
                this.buttonEdit.Text = "Update";
            }
        }

        //zdroj: http://www.dotnetedu.com/Kb2/Validate-Date-in-Different-formats-using-C
        private bool ValidateDate(string stringDate)
        {
            try
            {
                CultureInfo CultureInfoDateCulture = new CultureInfo("en-US");
                DateTime d = DateTime.ParseExact(stringDate, "yyyy-MM-dd", CultureInfoDateCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (this.textBoxName.Text == "" || this.textBoxSurname.Text == "")
            {
                MessageBox.Show("Error! Name and Surname can not be empty.");
            }
            else if (!ValidateDate(this.textBoxBirthDate.Text))
            {
                MessageBox.Show("Error! Birth date format should be YYYY-MM-DD.");
            }
            else if (this.comboBoxGender.Text != "Male" && this.comboBoxGender.Text != "Female")
            {
                MessageBox.Show("Error! Gender should be Male or Female.");
            }
            else
            {
                if (this.buttonEdit.Text == "Update")
                {
                    action.name = textBoxName.Text;
                    action.surname = textBoxSurname.Text;
                    action.birth_date = textBoxBirthDate.Text;
                    action.gender = comboBoxGender.Text;
                    action.country = textBoxCountry.Text;
                    action.address = textBoxAddress.Text;
                    action.telephone = textBoxTelephone.Text;

                    action.updateEmployee();
                }
                else
                {
                    action.name = textBoxName.Text;
                    action.surname = textBoxSurname.Text;
                    action.birth_date = textBoxBirthDate.Text;
                    action.gender = comboBoxGender.Text;
                    action.country = textBoxCountry.Text;
                    action.address = textBoxAddress.Text;
                    action.telephone = textBoxTelephone.Text;

                    action.addEmployee();
                }

                this.Close();
                list.refreshList();
            }
        }
    }
}
