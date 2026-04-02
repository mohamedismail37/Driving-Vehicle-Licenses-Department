using BusinessLogicLayer;
using DVLD.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests.Test_Appointments
{
    public partial class frmScheduleTest: Form
    {
        // He created it as a userContol instead of Form 

        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
        private clsApplication _application = new clsApplication();
        private clsTestAppointment _testAppointment = new clsTestAppointment();
        private int _testTypeID;
        private void _FillInfo(int TestTypeID, int LocalDrivingLicesneApplicationID)
        {
            _testTypeID = TestTypeID;
            int numberOfTrials = clsTest.NumberOfTrails(LocalDrivingLicesneApplicationID,_testTypeID);
            float testTypeFees = clsTestType.GetTestTypeData(TestTypeID).TestTypeFees;
            float retakeTestApplicationFees = 0;

            lblDLAppID.Text = LocalDrivingLicesneApplicationID.ToString();
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseAppInfo(LocalDrivingLicesneApplicationID);
            _application = clsApplication.FindByApplicationID(_localDrivingLicenseApplication.ApplicationID);
            lblDClass.Text = clsLicenseClass.GetClassNameByClassID(_localDrivingLicenseApplication.LicenseClassID);
            lblName.Text = clsPerson.Find(_application.ApplicantPersonID).FullName;
            lblTrial.Text = numberOfTrials.ToString();
            lblFees.Text = testTypeFees.ToString();
            lblLocked.Visible = false;
            dateTimePicker1.MinDate = DateTime.Today;

            switch (TestTypeID)
            {
                case 1:
                    gb1Name.Text = "Vision Test";
                    break;
                case 2:
                    gb1Name.Text = "Written Test";
                    pictureBox1.ImageLocation = "C:\\Users\\moham\\Desktop\\C19)Full Real Project\\Icons\\Written Test 512.png";
                    break;
                case 3:
                    gb1Name.Text = "Street Test";
                    pictureBox1.ImageLocation = "C:\\Users\\moham\\Desktop\\C19)Full Real Project\\Icons\\driving-test 512.png";
                    break;
            }


            if (numberOfTrials > 0)
            {
                groupBox2.Enabled = true;
                retakeTestApplicationFees = float.Parse(clsApplicationType.Find(7).ApplicationFees.ToString()); // Stands for Retake Test Application 
                // Better Solution, to make ApplicationTypes as enum in BLL in clsApplicationTypes
                lblRAppFees.Text = retakeTestApplicationFees.ToString();
                lblTotalFees.Text = (retakeTestApplicationFees + testTypeFees).ToString();
                lblTitle.Text = "Shedule Retake Test";

                if (_testAppointment.RetakeTestApplicationID != -1)
                    lblRTestAppId.Text = _testAppointment.RetakeTestApplicationID.ToString();
            }
            else
            {
                groupBox2.Enabled = false;
                lblRAppFees.Text = retakeTestApplicationFees.ToString();
                lblTotalFees.Text = testTypeFees.ToString(); 
            }
        }

        public frmScheduleTest(int TestTypeID, int LocalDrivingLicesneApplicationID, int TestAppointmentID =-1)
        {
            InitializeComponent();
            _testAppointment.TestAppointmentID = TestAppointmentID;
            if (TestAppointmentID != -1)
            {
                _testAppointment = clsTestAppointment.Find(TestAppointmentID);
            }
            _FillInfo(TestTypeID, LocalDrivingLicesneApplicationID);
            if (clsTestAppointment.IsApplicationHasUnlockedAppointment(LocalDrivingLicesneApplicationID, _localDrivingLicenseApplication.LicenseClassID, TestTypeID))
            {
                _testAppointment.mode = clsTestAppointment.enMode.Update;
                dateTimePicker1.Value = _testAppointment.AppointmentDate;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _testAppointment.TestTypeID = _testTypeID;
            _testAppointment.LocalDrivingLicenseApplicationID = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _testAppointment.AppointmentDate = dateTimePicker1.Value;
            _testAppointment.PaidFees = clsTestType.GetTestTypeData(_testTypeID).TestTypeFees;
            _testAppointment.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;
            _testAppointment.IsLocked = false;
            _testAppointment.RetakeTestApplicationID = -1;

            if (_testAppointment.mode == clsTestAppointment.enMode.AddNew)
            {
                if (clsTest.NumberOfTrails(_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID, _testTypeID) > 0)
                {
                    _application.currentMode = clsApplication.enMode.AddNewApplication;
                    _application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                    _application.ApplicationDate = DateTime.Now;
                    _application.LastStatusDate = DateTime.Now;
                    _application.PaidFees = float.Parse(clsApplicationType.Find(7).ApplicationFees.ToString());
                    _application.ApplicantPersonID = clsApplication.FindByApplicationID(_localDrivingLicenseApplication.ApplicationID).ApplicantPersonID;
                    _application.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;
                    _application.ApplicationTypeID = 7;
                    _testAppointment.RetakeTestApplicationID = _application.ApplicationID;
                }

                if (_testAppointment.Save() && _application.Save())
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Error: Data is NOT Saved Successfully.", "Error", MessageBoxButtons.OK);
                }
            }
            else if (_testAppointment.mode == clsTestAppointment.enMode.Update)
            {
                if (_testAppointment.Save())
                {
                    MessageBox.Show("Data Updated Successfully.", "Saved", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Error: Data is NOT Updated Successfully.", "Error", MessageBoxButtons.OK);
                }
            }
            this.Close();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            if (_testAppointment.IsLocked == true)
            {
                lblLocked.Visible = true;
                dateTimePicker1.Enabled = false;
                btnSave.Enabled = false;
            }
        }
    }
}
