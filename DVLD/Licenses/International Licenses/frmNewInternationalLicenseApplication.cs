using BusinessLogicLayer;
using DVLD.Global_Classes;
using DVLD.Licenses.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.International_Licenses
{
    public partial class frmNewInternationalLicenseApplication: Form
    {
        private clsInternationalLicense _InternationalLicense = new clsInternationalLicense();
        private clsApplication _Application = new clsApplication();

        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
            ctrlLicenseInfoWithFilter1.OnLicenseSelected += CtrlLicenseInfoWithFilter1_OnLicenseSelected;
        }

        private void CtrlLicenseInfoWithFilter1_OnLicenseSelected(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication_Load(null, null);
            if (clsInternationalLicense.IsHavePreviousActiveInternationalLicense(ctrlLicenseInfoWithFilter1.License().Driver.DriverID))
            {
                _InternationalLicense = clsInternationalLicense.FindByLocalLicenseID(ctrlLicenseInfoWithFilter1.License().LicenseID);
                llShowLicesesInfo.Enabled = true;
            }
            else
            {
                llShowLicesesInfo.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblCreatedBy.Text = clsGlobalSettings.LoggedInUser.UserName.ToString();
            lblFees.Text = clsApplicationType.Find(6).ApplicationFees.ToString();// composition was better


            if (ctrlLicenseInfoWithFilter1.IsFound)
            {
                llShowLicenseHistory.Enabled = true;
                lblLocalLicenseID.Text = ctrlLicenseInfoWithFilter1.License().LicenseID.ToString();
            }
            else
            {
                llShowLicenseHistory.Enabled = false;
            }

            if (ctrlLicenseInfoWithFilter1.PermissionToIssue)
            {
                btnIssue.Enabled = true;
            }
            else
            {
                btnIssue.Enabled = false;
            }

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(ctrlLicenseInfoWithFilter1.License().Driver.PersonID);
            frm.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (clsInternationalLicense.IsHavePreviousActiveInternationalLicense(ctrlLicenseInfoWithFilter1.License().Driver.DriverID))
            {
                MessageBox.Show("The Driver already have an international License.", "Can't Issue", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else if (ctrlLicenseInfoWithFilter1.License().LicenseClassID != 3)
            {
                MessageBox.Show("Can NOT issue for not Ordinary License Class!", "Not Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlLicenseInfoWithFilter1.PermissionToIssue = false;
                ctrlLicenseInfoWithFilter1.IsFound = true;
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to issue the license?.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _Application.ApplicantPersonID = ctrlLicenseInfoWithFilter1.License().Driver.PersonID;
                    _Application.ApplicationDate = DateTime.Now;
                    _Application.ApplicationTypeID = 6;
                    _Application.ApplicationStatus = clsApplication.enApplicationStatus.New;//
                    _Application.LastStatusDate = DateTime.Now;
                    _Application.PaidFees = float.Parse(clsApplicationType.Find(6).ApplicationFees.ToString());
                    _Application.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;
                    if (_Application.Save())
                    {
                        _InternationalLicense.ApplicationID = _Application.ApplicationID;
                        _InternationalLicense.DriverID = ctrlLicenseInfoWithFilter1.License().Driver.DriverID;
                        _InternationalLicense.IssuedUsingLocalLicenseID = ctrlLicenseInfoWithFilter1.License().LicenseID;
                        _InternationalLicense.IssueDate = DateTime.Now;
                        _InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
                        _InternationalLicense.IsActive = true;
                        _InternationalLicense.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;

                        if (_InternationalLicense.Save())
                        {
                            MessageBox.Show("International License Issued Successfully with ID = " + _InternationalLicense.InternationalLicenseID, "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            llShowLicesesInfo.Enabled = true;
                            lblILApplicationID.Text = _Application.ApplicationID.ToString();
                            lblILLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
                        }
                        else
                            MessageBox.Show("Error, License did NOT Issue ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Error, License did NOT Issue ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void llShowLicesesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalDriverInfo frm = new frmInternationalDriverInfo(_InternationalLicense.InternationalLicenseID);
            frm.ShowDialog();
        }
    }
}
