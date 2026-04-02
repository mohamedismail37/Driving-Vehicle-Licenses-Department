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

namespace DVLD.Licenses.Detained_Licenses
{
    public partial class frmReleaseDetainedLicense: Form
    {
        private clsApplication _Application = new clsApplication();
        private clsDetainedLicense _DetainedLicense = new clsDetainedLicense();
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
            ctrlLicenseInfoWithFilter1.OnLicenseSelected += CtrlLicenseInfoWithFilter1_OnLicenseSelected;
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            ctrlLicenseInfoWithFilter1.OnLicenseSelected += CtrlLicenseInfoWithFilter1_OnLicenseSelected;
            ctrlLicenseInfoWithFilter1.LoadLicense(LicenseID);

        }

        private void CtrlLicenseInfoWithFilter1_OnLicenseSelected(object sender, EventArgs e)
        {
            if (ctrlLicenseInfoWithFilter1.IsFound)
            {
                llShowLicenseHistory.Enabled = true;
            }
            else
            {
                llShowLicenseHistory.Enabled = false;
            }

            if (!clsDetainedLicense.IsDetainedLicense(ctrlLicenseInfoWithFilter1.License().LicenseID))
            {
                MessageBox.Show("Selected License is not detained, Choose another one: ", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
            }
            else
            {
                float ApplicationFees = float.Parse(clsApplicationType.Find(5).ApplicationFees.ToString()); // (5) -> release

                btnRelease.Enabled = true;
                _DetainedLicense = clsDetainedLicense.Find(ctrlLicenseInfoWithFilter1.License().LicenseID);
                lblDetainID.Text = _DetainedLicense.DetainID.ToString();
                lblLicenseID.Text = _DetainedLicense.LicenseID.ToString();
                lblDetainDate.Text = _DetainedLicense.DetainDate.ToShortDateString();
                lblCreatedBy.Text = clsUser.FindByUserID(_DetainedLicense.CreatedByUserID).UserName;
                lblApplicationFees.Text = ApplicationFees.ToString();
                lblFineFees.Text = _DetainedLicense.FineFees.ToString();
                lblTotalFees.Text = (ApplicationFees + _DetainedLicense.FineFees).ToString();
            }
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

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this Detained license?.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _Application.ApplicantPersonID = ctrlLicenseInfoWithFilter1.License().Driver.PersonID;
                _Application.ApplicationDate = DateTime.Now;
                _Application.ApplicationTypeID = 5;
                _Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;//
                _Application.LastStatusDate = DateTime.Now;
                _Application.PaidFees = float.Parse(clsApplicationType.Find(5).ApplicationFees.ToString());
                _Application.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;

                if (_Application.Save())
                {
                    if (clsDetainedLicense.ReleaseDetainLicense(_DetainedLicense.DetainID,clsGlobalSettings.LoggedInUser.UserID,_Application.ApplicationID,ctrlLicenseInfoWithFilter1.License().LicenseID))
                    {
                        MessageBox.Show("Detained License Released Successfully", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        llShowNewLicesesInfo.Enabled = true;
                        lblApplicationID.Text = _Application.ApplicationID.ToString();
                        btnRelease.Enabled = false;
                        ctrlLicenseInfoWithFilter1.RefreshControl();
                        ctrlLicenseInfoWithFilter1.SearchFilter(false);
                    }
                    else
                        MessageBox.Show("Error, License did NOT Released ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Error, License did NOT Released ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llShowNewLicesesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(ctrlLicenseInfoWithFilter1.License().LicenseID);
            frm.ShowDialog();
        }

    }
}
