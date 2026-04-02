using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;
using DVLD.Licenses;

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationInfo: UserControl
    {
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
        private int _LocalDrivingLicenseAppID = -1;
        private clsApplication _application = new clsApplication();

        public clsLocalDrivingLicenseApplication SelectedLocalDrivingAppInfo
        {
            get { return _LocalDrivingLicenseApplication; }
        }

        public clsApplication SelectedApplicationInfo
        {
            get { return _application; }
        }

        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        private void _FillApplicationInfo()
        {
            int numberOfPassedTests = BusinessLogicLayer.clsTest.GetNumberOfPassedTests(_LocalDrivingLicenseAppID);
            lblDLAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedForLicense.Text = BusinessLogicLayer.clsLicenseClass.GetClassNameByClassID(_LocalDrivingLicenseApplication.LicenseClassID);
            lblPassedTests.Text = numberOfPassedTests + "/3";
            if (clsLicense.FindByApplicationID(_application.ApplicationID) == null)
                llShowLicenseInfo.Enabled = false;
            else
                llShowLicenseInfo.Enabled = true;


            lblID.Text = _application.ApplicationID.ToString();
            lblStatus.Text = _application.ApplicationStatus.ToString();/// if there is ERROR -> // See here
            lblFees.Text = _application.PaidFees.ToString();
            lblType.Text = BusinessLogicLayer.clsApplicationType.GetApplicationTypeTitle(_application.ApplicationTypeID);
            lblDate.Text = _application.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _application.LastStatusDate.ToShortDateString();
            lblApplicant.Text = BusinessLogicLayer.clsPerson.Find(_application.ApplicantPersonID).FullName;
            lblCreatedBy.Text = BusinessLogicLayer.clsUser.FindByUserID(_application.CreatedByUserID).UserName;
        }

        public void LoadLocalDrivingLicenseApplicationInfo(int localDrivingLicenseAppID)
        {
            _LocalDrivingLicenseAppID = localDrivingLicenseAppID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseAppInfo(localDrivingLicenseAppID);
            _application = clsApplication.FindByApplicationID(_LocalDrivingLicenseApplication.ApplicationID);
            _FillApplicationInfo();
        }
        // He made 2 functions LoadByAppID | LoadByLDLAppID
        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_application.ApplicantPersonID);
            frm.ShowDialog();

            _FillApplicationInfo();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(clsLicense.FindByApplicationID(int.Parse(lblID.Text)).LicenseID);
            frm.ShowDialog();
        }
    }
}
