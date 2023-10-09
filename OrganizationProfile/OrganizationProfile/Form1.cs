using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Security.Policy;

namespace OrganizationProfile
{
    public partial class frmRegistration : Form
    {
        private string _FullName;
        private int _Age;
        private long _ContactNo;
        private long _StudentNo;
        public frmRegistration()
        {
            InitializeComponent();
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            string[] ListofProgram = new string[]
            {
                "BS Information Technology",
                "BS Computer Science",
                "BS Information System",
                "BS in Accountancy",
                "BS in Hospitality Management",
                "BS in Tourism Management"
            };
            for(int i = 0; i < 6; i++) 
            {
                cbPrograms.Items.Add(ListofProgram[i].ToString());
            }

            string[] ListofGender = new string[]
            {
                "Female", "Male"
            };
            for(int j = 0; j < 2; j++) 
            {
                cbGenders.Items.Add(ListofGender[j].ToString());
            }
        }
        public long StudentNumber(string studentnumber)
        {
            _StudentNo = long.Parse(studentnumber);
            return _StudentNo;
        }
        public long ContactNumber(string contactnumber) 
        {
            if (Regex.IsMatch(contactnumber, @"^[0-9]{10,11}$"))
            {
                _ContactNo = long.Parse(contactnumber);
            }
            return _ContactNo;
        }
        public string FullName(string Lastname, string Firstname, string Middleinitial)
        {
            if (Regex.IsMatch(Lastname, @"^[a-zA-Z]+$") || Regex.IsMatch(Firstname, @"^[a-zA-Z]+$") || Regex.IsMatch(Middleinitial, @"^[a-zA-Z]+$"))
            {
                _FullName = Lastname + ", " + Firstname  + Middleinitial;
            }
            return _FullName;
        }
        public int Age(string age) 
        {
            if (Regex.IsMatch(age, @"^[0-9]{1,3}$"))
            {
                _Age = Int32.Parse(age);
            }

            return _Age;
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtStudentNo.Text))
                {
                    throw new ArgumentNullException("", "Student Number cannot be empty!");
                }
                if (cbPrograms.SelectedItem == null)
                {
                    throw new ArgumentNullException("", "Select Your Program!");
                }
                if (string.IsNullOrEmpty(txtLastName.Text))
                {
                    throw new ArgumentNullException("", "Last Name cannot be empty!");
                }
                if (string.IsNullOrEmpty(txtFirstName.Text))
                {
                    throw new ArgumentNullException("", "First Name cannot be empty!");
                }
                if (string.IsNullOrEmpty(txtMiddleInitial.Text))
                {
                    throw new ArgumentNullException("", "Middle Initial cannot be empty!");
                }
                if (string.IsNullOrEmpty(txtAge.Text))
                {
                    throw new ArgumentNullException("", "Age cannot be empty!");
                }
                if (cbGenders.SelectedItem == null)
                {
                    throw new ArgumentNullException("", "Select your Gender!");
                }
                if (string.IsNullOrEmpty(txtContactNo.Text))
                {
                    throw new ArgumentNullException("", "Contact Number cannot be empty!");
                }

                if (txtLastName.Text.Length > 15)
                {
                    throw new OverflowException("Last Name is too long Common length is 15 Characters max");
                }
                if (txtMiddleInitial.Text.Length > 2)
                {
                    throw new OverflowException("Middle Initial is too long Common length is 2 Characters max");
                }
                if (txtFirstName.Text.Length > 15)
                {
                    throw new OverflowException("First Name is too long Common length is 15 Characters max");
                }

                if (Regex.IsMatch(txtLastName.Text, "[0-9]+$"))
                {
                    throw new FormatException("Last Name cannot contain any number or Symbol!");
                }
                if (Regex.IsMatch(txtFirstName.Text, "[0-9]+$"))
                {
                    throw new FormatException("First Name cannot contain any number or Symbol!");
                }
                if (Regex.IsMatch(txtMiddleInitial.Text, "[0-9]"))
                {
                    throw new FormatException("Middle Initial cannot contain any number!");
                }
                if (Regex.IsMatch(txtContactNo.Text, "[a-zA-Z\\W_]+$"))
                {
                    throw new FormatException("Contact Number does not contain any Letters or Symbols!");
                }

                if (txtContactNo.Text.Length < 10 || txtContactNo.Text.Length > 11)
                {
                    throw new IndexOutOfRangeException("This is an Invalid Contact Number!");
                }
                if (Convert.ToInt32(txtAge.Text) > 150)
                {
                    throw new IndexOutOfRangeException("This age is out of Bounds!");
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            StudentInformationClass.SetFullName = FullName(txtLastName.Text, txtFirstName.Text, txtMiddleInitial.Text);
            StudentInformationClass.SetStudentNo = (int)StudentNumber(txtStudentNo.Text);
            StudentInformationClass.SetProgram = cbPrograms.Text;
            StudentInformationClass.SetGender = cbGenders.Text;
            StudentInformationClass.SetContactNo = ContactNumber(txtContactNo.Text.ToString());
            StudentInformationClass.SetAge = Age(txtAge.Text);
            StudentInformationClass.SetBirthday = datePickerBirthday.Value.ToString("yyyy-MM-dd");

            frmConfirmation frm2 = new frmConfirmation();
            frm2.ShowDialog();
        }
    }
}
