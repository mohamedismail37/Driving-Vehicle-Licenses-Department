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
using DVLD.People;

namespace DVLD.Users
{
    public partial class frmAddUpdateUser: Form
    {
        public enum enModes { AddNewUser = 0, UpdateUser = 1 };
        private enModes _CurrentMode;

        private clsUser _User = new clsUser();
        public frmAddUpdateUser()
        {
            InitializeComponent();
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();

            _User.UserID = UserID;
            _CurrentMode = enModes.UpdateUser;
        }

        private void _ResetDefaultValues()
        {
            // This will intialize the default values

            if (_CurrentMode == enModes.AddNewUser)
            {
                lblAddEditUser.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();

                tpLoginInfo.Enabled = false;

            }
            else
            {
                lblAddEditUser.Text = "Update User";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_User.UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User.UserID, "User Not Found!", MessageBoxButtons.OK);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            cbIsActive.Checked = _User.IsActive;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_CurrentMode == enModes.UpdateUser)
            {
                tpLoginInfo.Enabled = true;
                tabControl1.SelectedTab = tpLoginInfo;
                btnSave.Enabled = true;
                return;
            }

            // Incase of Add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExist(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person Is already user, Select another one", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tabControl1.SelectedTab = tpLoginInfo;
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "UserName should have a value");
            }
            else if (clsUser.IsUserExist(txtUserName.Text))
            {
                if (!(_User.Mode == clsUser.enMode.Update && _User.UserName == txtUserName.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "UserName is used for another person");
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, null);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password should have a value");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password should equal Password");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) // ValidateChildren() -> Depends on checks controls with Validating events 
            {
                MessageBox.Show("Some fields are NOT valid! Put the mouse on Red Icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                // Won't continue if the form not Valid
            }


            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            if (cbIsActive.Checked)
                _User.IsActive = true;
            else
                _User.IsActive = false;
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;


            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();

                _User.Mode = clsUser.enMode.Update;

                lblAddEditUser.Text = "Update User Info";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK);

            }
            else
                MessageBox.Show("Error: Data is NOT Saved Successfully.", "Error", MessageBoxButtons.OK);

        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_CurrentMode == enModes.UpdateUser)
                _LoadData();
        }
    }
}
