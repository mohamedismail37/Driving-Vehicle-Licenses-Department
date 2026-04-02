using BusinessLogicLayer;
using DVLD.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Licenses.Detained_Licenses
{
    public partial class frmDetainLicense: Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
            ctrlLicenseInfoWithFilter1.OnLicenseSelected += CtrlLicenseInfoWithFilter1_OnLicenseSelected;
        }
        private void CtrlLicenseInfoWithFilter1_OnLicenseSelected(object sender, EventArgs e)
        {
            frmDetainLicense_Load(null, null);
            if (ctrlLicenseInfoWithFilter1.IsFound)
            {
                llShowLicenseHistory.Enabled = true;
            }
            else
            {
                llShowLicenseHistory.Enabled = false;
            }


            if (clsDetainedLicense.IsDetainedLicense(ctrlLicenseInfoWithFilter1.License().LicenseID))
            {
                MessageBox.Show("Selected License is already Detained, Choose other License License: ", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
            }
            else
            {
                btnDetain.Enabled = true;
            }

            lblLicenseID.Text = ctrlLicenseInfoWithFilter1.License().LicenseID.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobalSettings.LoggedInUser.UserName;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(ctrlLicenseInfoWithFilter1.License().Driver.PersonID);
            frm.ShowDialog();
        }

        private void llShowNewLicesesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(ctrlLicenseInfoWithFilter1.License().LicenseID);
            frm.ShowDialog();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to detain this license?.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int licenseID = ctrlLicenseInfoWithFilter1.License().LicenseID;
                float fineFees = float.Parse(tbFineFees.Text.Trim().ToString());
                int createdByUserID = clsGlobalSettings.LoggedInUser.UserID;

                int DetainedLicenseID = clsDetainedLicense.DetainLicense(licenseID, fineFees, createdByUserID);

                if ((DetainedLicenseID != -1) && clsLicense.DecativatedLicense(licenseID))
                {
                    MessageBox.Show("License Detained Successfully with ID = " + DetainedLicenseID, "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    llShowNewLicesesInfo.Enabled = true;
                    lblDetainID.Text = DetainedLicenseID.ToString();
                    btnDetain.Enabled = false;
                    ctrlLicenseInfoWithFilter1.RefreshControl();
                    ctrlLicenseInfoWithFilter1.SearchFilter(false);
                }
                else
                    MessageBox.Show("Error, License did NOT Replaced ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
