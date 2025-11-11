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
using DVLD.Global_Classes;
using System.IO;

namespace DVLD
{
    public partial class frmAddEditPersonInfo: Form
    {

        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        public enum enModes { AddNewPerson = 0, EditPerson = 1 };
        public enum enGender { Male = 0, Female = 1};
        
        private enModes _CurrentMode;
        private int _PersonID = -1;
        private clsPerson _Person; // now it's = null, don't have object in the memory (Just declaration !Definition), still need to define.

        public frmAddEditPersonInfo()
        {
            InitializeComponent();
            _CurrentMode = enModes.AddNewPerson;
        }
        // The constructors Just to Setup the mode(_CurrentMode).
        // While handling the (Add || Update) in the Form_Load()
        public frmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();

            _CurrentMode = enModes.EditPerson;
            _PersonID = PersonID;

        }
        private void _ResetDefaultValues()
        {
            // This will initialize the Default Values
            _FillCountriesInComboBox();

            if (_CurrentMode == enModes.AddNewPerson)
            {
                lblAddEditPerson.Text = "Add New Person";
                _Person = new clsPerson(); // Here is the Object definition (As a new person{AddPersonInfo}).
            }
            else
            {
                lblAddEditPerson.Text = "Update Person";
            }

            // set default image for the person
            if (rbMale.Checked)
                pbProfilePic.Image = Resources.Male_512;
            else
                pbProfilePic.Image = Resources.Female_512;

            // Hide/Show the Remove(Img) LinkLabel incase there isn't image for the Person.
            LLRemoveImage.Visible = (pbProfilePic.ImageLocation != null);

            // Date:
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            /// Should NOT allow adding age more than 100 years
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            // This Will set default Country to Egypt.
            cbCountries.SelectedIndex = cbCountries.FindString("Egypt");

            //He did this for the textboxes, but I see its useless
            // txtFirstName.Text = "";
            //       //
        }
        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries(); //BusinessLogicLayer.BLL.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }
        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);// Here is the Object definition (As Update Person).

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found!", MessageBoxButtons.OK);
                this.Close();
                return;
            }

            // The following Code will NOT execute if the person not found

            lblPersonID.Text = _PersonID.ToString();
            tbFirstName.Text = _Person.FirstName;
            tbSecondName.Text = _Person.SecondName;
            tbThirdName.Text = _Person.ThirdName;
            tbLastName.Text = _Person.LastName;
            tbNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            tbAddress.Text = _Person.Address;
            tbPhone.Text = _Person.Phone;
            tbEmail.Text = _Person.Email;

            // Look how smart it is!
            cbCountries.SelectedIndex = cbCountries.FindString(_Person.CountryInfo.CountryName);
            //

            // Load Person Image incase it was set
            if (!string.IsNullOrEmpty(_Person.ImagePath))// <- That's more safe than this -> //if (_Person.ImagePath != "")
            {
                pbProfilePic.ImageLocation = _Person.ImagePath;
            }

            // (Hide/Show) the llRemove incase there is no image for the person
            LLRemoveImage.Visible = !string.IsNullOrEmpty(_Person.ImagePath);
        }
        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_CurrentMode == enModes.EditPerson)
                _LoadData();
        }

        private bool _HandlePersonImage()
        {
            
            // This method will handle the person image, 
            // it will take car of Deleting the old image from the folder
            // in case the image changed. and it will rename the new image with GUID and 
            // place it in the images folder.


            // _Person.ImagePath contains the old Image, we check if it changes then we copy the new image
            if (_Person.ImagePath != pbProfilePic.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    // Firstly, we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We couldn't delete the file.
                        // log it later
                    }
                }

                if (pbProfilePic.ImageLocation != null)
                {
                    // Then we copy the new image to the images folder after we rename it
                    string SourceImageFile = pbProfilePic.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbProfilePic.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //_ValidateAllForm(); Instead of this, he did this: 
            if (!this.ValidateChildren()) // ValidateChildren() -> Depends on Checking all [Buttons, TextBoxs, etc] which conected to the general function ValidateEmptyTextBox(-) 
            {
                MessageBox.Show("Some fields are NOT valid! Put the mouse on Red Icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                // Won't continue if the form not Valid
            }

            if (!_HandlePersonImage())
                return;

            int NationalityCountryID = clsCountry.Find(cbCountries.Text).ID;


            _Person.FirstName = tbFirstName.Text.Trim();
            _Person.SecondName = tbSecondName.Text.Trim();
            _Person.ThirdName = tbThirdName.Text.Trim();
            _Person.LastName = tbLastName.Text.Trim();
            _Person.NationalNo = tbNationalNo.Text.Trim();
            _Person.Phone = tbPhone.Text.Trim();
            _Person.Email = tbEmail.Text.Trim();
            _Person.Address = tbAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = NationalityCountryID;

            if (rbMale.Checked)
                _Person.Gender = (short)enGender.Male;
            else
                _Person.Gender = (short)enGender.Female;

            if (pbProfilePic.Image != null)
                _Person.ImagePath = pbProfilePic.ImageLocation;
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                // Change Form mode to Update.
                _CurrentMode = enModes.EditPerson;
                lblAddEditPerson.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK);

                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Error: Data is NOT Saved Successfully.", "Error", MessageBoxButtons.OK);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbProfilePic.ImageLocation == null)
                pbProfilePic.Image = Resources.Female_512;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbProfilePic.ImageLocation == null)
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
            pbProfilePic.ImageLocation = null;
            LLRemoveImage.Visible = false;
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            // This function is a generic validation method that checks if a TextBox is empty.
            // It should be connected to the Validating eventof each TextBoxyou want to validate it's not Empty.
            /// (DRY)
            /// I Excepted tbNationalNo & tbEmail from this method, Because they have a different type of validation

            TextBox Temp = ((TextBox)sender);

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true; // Cancel the event: this prevents the user from leaving the TextBox.

                errorProvider1.SetError(Temp, "This field required!");
            }
            else
            {
                // If the TextBox has text, clear any previous error message.
                errorProvider1.SetError(Temp, null);
            }
        }

        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbNationalNo, "National Number should have a value");
            }
            else if (clsPerson.isPersonExist(tbNationalNo.Text)) // Checks that the National Number isn't used for other person
            {
                e.Cancel = true;
                errorProvider1.SetError(tbNationalNo, "National Number is used for another person");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbNationalNo, null);
            }
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (tbEmail.Text.Length != 0)
            {
                if (!clsValidation.ValidateEmail(tbEmail.Text))
                {
                    e.Cancel = true; // Cancel the event: this prevents the user from leaving the TextBox.
                    errorProvider1.SetError(tbEmail, "Invalid Email Address Format!");
                }
                else
                {
                    errorProvider1.SetError(tbEmail, null); // Becuase if the email become valid make the error provider disapear.
                }
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
