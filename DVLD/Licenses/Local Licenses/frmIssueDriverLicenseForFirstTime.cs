using BusinessLogicLayer;
using DVLD.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmIssueDriverLicenseForFirstTime: Form
    {
        public frmIssueDriverLicenseForFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            ctrlApplicationInfo1.LoadLocalDrivingLicenseApplicationInfo(LocalDrivingLicenseApplicationID);
        }

        // in frm_Load(), you should protect yourself
        // like make method IsPassedAllTests(), bec. if the frm opened from other place
        // should this frm check, else this.Close() | and the same for checking he don't
        // already have license from this class, etc.

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int applicantPersonID = ctrlApplicationInfo1.SelectedApplicationInfo.ApplicantPersonID;
            clsDriver driver = new clsDriver();
            clsLicense license = new clsLicense();
            clsLicenseClass licenseClass = clsLicenseClass.Find(ctrlApplicationInfo1.SelectedLocalDrivingAppInfo.LicenseClassID); // Instead of this, make composition for it in the clsLicense

            if (clsDriver.IsDriverExistByPersonID(applicantPersonID))
            {
                driver = clsDriver.FindByPersonID(applicantPersonID);
            }
            else
            {
                driver.PersonID = applicantPersonID;
                driver.CreatedDate = DateTime.Now;
                driver.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;
                driver.Save();
            }

            license.ApplicationID = ctrlApplicationInfo1.SelectedApplicationInfo.ApplicationID;
            license.Driver.DriverID = driver.DriverID;
            license.LicenseClassID = ctrlApplicationInfo1.SelectedLocalDrivingAppInfo.LicenseClassID;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = DateTime.Now.AddYears(licenseClass.DefaultValidityLength);
            license.Notes = tbNotes.Text;
            license.PaidFees = licenseClass.ClassFees;
            license.IsActive = true;
            license.IssueReason = 1; // New Local Driving License -> he did it enum
            license.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;

            ctrlApplicationInfo1.SelectedApplicationInfo.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            ctrlApplicationInfo1.SelectedApplicationInfo.currentMode = clsApplication.enMode.UpdateApplication;

            if (license.Save() && ctrlApplicationInfo1.SelectedApplicationInfo.Save())
            {
                MessageBox.Show("License Issued Successfully with LicenseID = " + license.LicenseID, "Succeeded", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Error: License was NOT Issued!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }
    }
}
