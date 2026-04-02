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
using DVLD.Global_Classes;
using DVLD.Licenses;

namespace DVLD.Applications.ReplacementForDamagedOrLostLicense
{
    public partial class frmReplacementForDamagedOrLostLicense: Form
    {
        private clsApplication _Application = new clsApplication();
        private clsLicense _NewLicense = new clsLicense();

        public frmReplacementForDamagedOrLostLicense()
        {
            InitializeComponent();
            ctrlLicenseInfoWithFilter1.OnLicenseSelected += CtrlLicenseInfoWithFilter1_OnLicenseSelected;
        }

        private void CtrlLicenseInfoWithFilter1_OnLicenseSelected(object sender, EventArgs e)
        {
            frmReplacementForDamagedOrLostLicense_Load(null, null);
            if (ctrlLicenseInfoWithFilter1.IsFound)
            {
                llShowLicenseHistory.Enabled = true;
                lblOldLicenseID.Text = ctrlLicenseInfoWithFilter1.License().LicenseID.ToString();
            }
            else
            {
                llShowLicenseHistory.Enabled = false;
            }

            if (!ctrlLicenseInfoWithFilter1.License().IsActive)
            {
                MessageBox.Show("Selected License is not Active, Choose an active License: " , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReplacement.Enabled = false;
            }
            else
            {
                btnReplacement.Enabled = true;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReplacementForDamagedOrLostLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = clsApplicationType.Find(4).ApplicationFees.ToString(); // (4) -> Damaged
            lblCreatedBy.Text = clsGlobalSettings.LoggedInUser.UserName;
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLostLicense.Checked)
            {
                this.Text = "Replacement For Lost License";
                labelTitle.Text = "Replacement For Lost License";
                lblApplicationFees.Text = clsApplicationType.Find(3).ApplicationFees.ToString(); // (3) -> Lost
            }
            else
            {
                this.Text = "Replacement For Damaged License";
                labelTitle.Text = "Replacement For Damaged License";
                lblApplicationFees.Text = clsApplicationType.Find(4).ApplicationFees.ToString(); // (4) -> Damaged
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(ctrlLicenseInfoWithFilter1.License().Driver.PersonID);
            frm.ShowDialog();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue a Replacement for this license?.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _Application.ApplicantPersonID = ctrlLicenseInfoWithFilter1.License().Driver.PersonID;
                _Application.ApplicationDate = DateTime.Now;
                if (rbLostLicense.Checked)
                    _Application.ApplicationTypeID = 3;
                else
                    _Application.ApplicationTypeID = 4;

                _Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;//
                _Application.LastStatusDate = DateTime.Now;

                if (rbLostLicense.Checked)
                    _Application.PaidFees = float.Parse(clsApplicationType.Find(3).ApplicationFees.ToString());
                else
                    _Application.PaidFees = float.Parse(clsApplicationType.Find(4).ApplicationFees.ToString());

                _Application.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;
                if (_Application.Save())
                {
                    _NewLicense.ApplicationID = _Application.ApplicationID;
                    _NewLicense.IssueDate = DateTime.Now;
                    _NewLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.Find(ctrlLicenseInfoWithFilter1.License().LicenseClassID).DefaultValidityLength);
                    _NewLicense.IsActive = true;
                    _NewLicense.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;
                    _NewLicense.Notes = "";
                    if (rbLostLicense.Checked)
                        _NewLicense.IssueReason = 3;
                    else
                        _NewLicense.IssueReason = 4;

                    _NewLicense.Driver = clsDriver.FindByDriverID(ctrlLicenseInfoWithFilter1.License().Driver.DriverID);
                    _NewLicense.LicenseClassID = ctrlLicenseInfoWithFilter1.License().LicenseClassID;
                    _NewLicense.PaidFees = clsLicenseClass.Find(ctrlLicenseInfoWithFilter1.License().LicenseClassID).ClassFees;

                    if (_NewLicense.Save() && clsLicense.DecativatedLicense(ctrlLicenseInfoWithFilter1.License().LicenseID))
                    {
                        MessageBox.Show("New License Issued Successfully with ID = " + _NewLicense.LicenseID, "License Replaced", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        llShowNewLicesesInfo.Enabled = true;
                        lbLRApplicationID.Text = _Application.ApplicationID.ToString();
                        lblReplacedLicenseID.Text = _NewLicense.LicenseID.ToString();
                        btnReplacement.Enabled = false;
                        gbReplacementReason.Enabled = false;
                        ctrlLicenseInfoWithFilter1.SearchFilter(false);
                    }
                    else
                        MessageBox.Show("Error, License did NOT Replaced ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Error, License did NOT Replaced ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void llShowNewLicesesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_NewLicense.LicenseID);
            frm.ShowDialog();
        }
    }
}
