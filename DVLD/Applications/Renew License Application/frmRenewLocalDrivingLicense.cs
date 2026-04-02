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

namespace DVLD.Licenses.Renew_License_Application
{
    public partial class frmRenewLocalDrivingLicense: Form
    {
        private clsApplication _Application = new clsApplication();
        private clsLicense _NewLicense = new clsLicense();
        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
            ctrlLicenseInfoWithFilter1.OnLicenseSelected += CtrlLicenseInfoWithFilter1_OnLicenseSelected;
        }
        private void CtrlLicenseInfoWithFilter1_OnLicenseSelected(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicense_Load(null, null);
            if (ctrlLicenseInfoWithFilter1.IsFound)
            {
                llShowLicenseHistory.Enabled = true;
                _FilInfo();
            }
            else
            {
                llShowLicenseHistory.Enabled = false;
            }

            if (ctrlLicenseInfoWithFilter1.License().ExpirationDate > DateTime.Now)
            {
                MessageBox.Show("Selected License is not expired, it will expireson: " + ctrlLicenseInfoWithFilter1.License().ExpirationDate.ToShortDateString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
            }
            else
            {
                btnRenew.Enabled = true;
            }
        }

        private void _FilInfo()
        {
            float applicationFees = float.Parse(clsApplicationType.Find(2).ApplicationFees.ToString()); // (2) Stands for Renew License Application
            float licenseFees = clsLicenseClass.Find(ctrlLicenseInfoWithFilter1.License().LicenseClassID).ClassFees;

            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblLicenseFees.Text = licenseFees.ToString();
            lblOldLicenseID.Text = ctrlLicenseInfoWithFilter1.License().LicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(clsLicenseClass.Find(ctrlLicenseInfoWithFilter1.License().LicenseClassID).DefaultValidityLength).ToShortDateString();
            lblTotalFees.Text = (applicationFees + licenseFees).ToString();

        }
        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            lblCreatedBy.Text = clsGlobalSettings.LoggedInUser.UserName;
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = clsApplicationType.Find(2).ApplicationFees.ToString(); // Enum was better than (2)
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(ctrlLicenseInfoWithFilter1.License().Driver.PersonID);
            frm.ShowDialog();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to renew the license?.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // He defined the _app here
                _Application.ApplicantPersonID = ctrlLicenseInfoWithFilter1.License().Driver.PersonID;
                _Application.ApplicationDate = DateTime.Now;
                _Application.ApplicationTypeID = 2;
                _Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;//
                _Application.LastStatusDate = DateTime.Now;
                _Application.PaidFees = float.Parse(clsApplicationType.Find(2).ApplicationFees.ToString());
                _Application.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;
                if (_Application.Save())
                {
                    _NewLicense.ApplicationID = _Application.ApplicationID;
                    _NewLicense.IssueDate = DateTime.Now;
                    _NewLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.Find(ctrlLicenseInfoWithFilter1.License().LicenseClassID).DefaultValidityLength);
                    _NewLicense.IsActive = true;
                    _NewLicense.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;
                    _NewLicense.Notes = tbNotes.Text.Trim();
                    _NewLicense.IssueReason = 2;
                    _NewLicense.Driver = clsDriver.FindByDriverID(ctrlLicenseInfoWithFilter1.License().Driver.DriverID);
                    _NewLicense.LicenseClassID = ctrlLicenseInfoWithFilter1.License().LicenseClassID;
                    _NewLicense.PaidFees = clsLicenseClass.Find(ctrlLicenseInfoWithFilter1.License().LicenseClassID).ClassFees;

                    if (_NewLicense.Save() && clsLicense.DecativatedLicense(ctrlLicenseInfoWithFilter1.License().LicenseID))
                    {
                        MessageBox.Show("New License Issued Successfully with ID = " + _NewLicense.LicenseID, "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        llShowNewLicesesInfo.Enabled = true;
                        lblRLApplicationID.Text = _Application.ApplicationID.ToString();
                        lblRenewedLicenseID.Text = _NewLicense.LicenseID.ToString();
                        btnRenew.Enabled = false;

                    }
                    else
                        MessageBox.Show("Error, License did NOT renewed ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Error, License did NOT renewed ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void llShowNewLicesesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_NewLicense.LicenseID);
            frm.ShowDialog();
        }
    }
}
