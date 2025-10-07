using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;
using DVLD.Properties;
using System.Linq.Expressions;

namespace DVLD
{
    enum enModes {AddNewPerson = 1, EditPerson =2 };

    public partial class frmAddEditPersonInfo: Form
    {
        enModes CurrentMode;
        int PersonID = -1;

        public frmAddEditPersonInfo()
        {
            InitializeComponent();
            CurrentMode = enModes.AddNewPerson;
        }

        public frmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();
            CurrentMode = enModes.EditPerson;
            lblAddEditPerson.Text = "Update Person";
            lblPersonID.Text = PersonID.ToString();

            DataTable dt = BLL.FillInfoUpdatePerson(PersonID);

            DataRow row = dt.Rows[0];

            tbNationalNo.Text = row["NationalNo"].ToString();
            tbFirstName.Text = row["FirstName"].ToString();
            tbSecondName.Text = row["SecondName"].ToString();
            tbThirdName.Text = row["ThirdName"].ToString();
            tbLastName.Text = row["LastName"].ToString();
            dtpDateOfBirth.Value = DateTime.Parse(row["DateOfBirth"].ToString());
            cbCountries.SelectedValue = int.Parse((row["NationalityCountryID"].ToString()));
            if (row["Gendor"].ToString() == "1")
                rbFemale.Checked = true;
            else
                rbMale.Checked = true;

            tbPhone.Text = row["Phone"].ToString();
            tbEmail.Text = row["Email"].ToString();
            tbAddress.Text = row["Address"].ToString();
            pbProfilePic.ImageLocation = row["ImagePath"].ToString();

            this.PersonID = PersonID;
        }

        private void _EditPersonMode()
        {
            CurrentMode = enModes.EditPerson;
            lblAddEditPerson.Text = "Update Person";
            lblPersonID.Text = PersonID.ToString();
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = BusinessLogicLayer.BLL.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _FillCountriesInComboBox();
            cbCountries.SelectedIndex = 50;

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(- 18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            if (CurrentMode == enModes.AddNewPerson)
                LLRemoveImage.Visible = false;
        }

        private bool _IsValidEmail(string Email)
        {
            bool at = false, DotCom = false;

            for (int i = 0; i < Email.Length; i++)
            {
                if (Email[i] == '@')
                {
                    at = true;
                    break;
                }
            }

            if (Email[Email.Length - 1] == 'm' && Email[Email.Length - 2] == 'o' && Email[Email.Length - 3] == 'c' && Email[Email.Length - 4] == '.')
                DotCom = true;

            return (at && DotCom);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbProfilePic.Image == Resources.Male_512 || pbProfilePic.ImageLocation == null)
                pbProfilePic.Image = Resources.Female_512;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbProfilePic.Image == Resources.Female_512 || pbProfilePic.ImageLocation == null)
                pbProfilePic.Image = Resources.Male_512;
        }

        private void LLSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a photo";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbProfilePic.ImageLocation = ofd.FileName;                
                LLRemoveImage.Visible = true;
            }
        }

        private void LLRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (rbMale.Checked)
            {
                pbProfilePic.Image = Resources.Male_512;
            }
            else
            {
                pbProfilePic.Image = Resources.Female_512;
            }
            pbProfilePic.ImageLocation = "";
            LLRemoveImage.Visible = false;
        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                e.Cancel = true;
                tbFirstName.Focus();
                epFirstName.SetError(tbFirstName, "First Name should have a value");
            }
            else
            {
                e.Cancel = false;
                epFirstName.SetError(tbFirstName, "");
            
            }
        }

        private void tbSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSecondName.Text))
            {
                e.Cancel = true;
                tbSecondName.Focus();
                epSecondName.SetError(tbSecondName, "Second Name should have a value");
            }
            else
            {
                e.Cancel = false;
                epSecondName.SetError(tbSecondName, "");
            }
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                e.Cancel = true;
                tbLastName.Focus();
                epLastName.SetError(tbLastName, "Last Name should have a value");
            }
            else
            {
                e.Cancel = false;
                epLastName.SetError(tbLastName, "");
            }
        }

        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNationalNo.Text))
            {
                e.Cancel = true;
                tbNationalNo.Focus();
                epNationalNo.SetError(tbNationalNo, "National Number should have a value");
            }
            else if (BusinessLogicLayer.BLL.IsFound(tbNationalNo.Text))
            {
                e.Cancel = true;
                tbNationalNo.Focus();
                epNationalNo.SetError(tbNationalNo, "National Number is used for another person");
            }
            else
            {
                e.Cancel = false;
                epNationalNo.SetError(tbNationalNo, "");
            }
        }

        private void tbPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                e.Cancel = true;
                tbPhone.Focus();
                epPhoneNo.SetError(tbPhone, "Phone Number should have a value");
            }
            else
            {
                e.Cancel = false;
                epPhoneNo.SetError(tbPhone, "");
            }
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (tbEmail.Text.Length != 0)
            {
                if (tbEmail.Text.Length < 6)
                {
                    e.Cancel = true;
                    tbEmail.Focus();
                    epWrongEmailAddress.SetError(tbEmail, "Please enter a correct Email!");
                }
                else if (!_IsValidEmail(tbEmail.Text))
                {
                    e.Cancel = true;
                    tbEmail.Focus();
                    epWrongEmailAddress.SetError(tbEmail, "Please enter a correct Email!");
                }
                else
                {
                    e.Cancel = false;
                    epWrongEmailAddress.SetError(tbEmail, "");
                }
            }
        }

        private void tbAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                e.Cancel = true;
                tbAddress.Focus();
                epAddress.SetError(tbAddress, "Address should have a value");
            }
            else
            {
                e.Cancel = false;
                epAddress.SetError(tbAddress, "");
            }
        }

        private void _ValidateAllForm()
        {
            if (tbFirstName.Text == "")
            {
                tbFirstName.Focus();
                epFirstName.SetError(tbFirstName, "First Name should have a value");
            }
            if (tbSecondName.Text == "")
            {
                tbSecondName.Focus();
                epSecondName.SetError(tbSecondName, "Second Name should have a value");
            }
            if (tbLastName.Text == "")
            {
                tbLastName.Focus();
                epLastName.SetError(tbLastName, "Last Name should have a value");
            }
            if (tbNationalNo.Text == "")
            {
                tbNationalNo.Focus();
                epNationalNo.SetError(tbNationalNo, "National should have a value");
            }
            if (tbAddress.Text == "")
            {
                tbAddress.Focus();
                epAddress.SetError(tbAddress, "Address should have a value");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _ValidateAllForm();

            string FirstName = tbFirstName.Text;
            string SecondName = tbSecondName.Text;
            string ThirdName = tbThirdName.Text;
            string LastName = tbLastName.Text;
            string NationalNo = tbNationalNo.Text;
            DateTime DateOfBirth = dtpDateOfBirth.Value;
            bool Gender = rbFemale.Checked;
            string Phone = tbPhone.Text;
            string Email = tbEmail.Text;
            int CountryID = cbCountries.SelectedIndex++;
            string Address = tbAddress.Text;
            string ImagePath = pbProfilePic.ImageLocation;

            if (CurrentMode == enModes.AddNewPerson)
            {
                
                PersonID = BusinessLogicLayer.BLL.SavePerson(FirstName, SecondName, ThirdName, LastName, NationalNo,
                    DateOfBirth, Gender, Phone, Email, CountryID, Address, ImagePath);

            }
            else if (CurrentMode == enModes.EditPerson)
            {

                     BusinessLogicLayer.BLL.UpdatePerson(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNo,
                    DateOfBirth, Gender, Phone, Email, CountryID, Address, ImagePath);

            }

            if (PersonID == -1)
            {
                MessageBox.Show("The person doesn't save", "Error!", MessageBoxButtons.OK);
            }
            else
            {
                _EditPersonMode();
                MessageBox.Show("Data Saved Successfully","Saved");
            }
        }

        private void tbPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
