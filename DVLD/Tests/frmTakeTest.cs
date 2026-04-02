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

namespace DVLD.Tests
{
    public partial class frmTakeTest: Form
    {
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
        private int _testTypeID;
        private int _testAppointmentID;
        public frmTakeTest(int LocalDrivingLicenseAppID,int TestTypeID,int TestAppointmentID)
        {
            InitializeComponent();
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseAppInfo(LocalDrivingLicenseAppID);
            _testTypeID = TestTypeID;
            _testAppointmentID = TestAppointmentID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillInfo()
        {
            lblDLAppID.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = clsLicenseClass.GetClassNameByClassID(_localDrivingLicenseApplication.LicenseClassID);
            lblName.Text = clsPerson.Find(clsApplication.FindByApplicationID(_localDrivingLicenseApplication.ApplicationID).ApplicantPersonID).FullName;
            lblTrial.Text = clsTest.NumberOfTrails(_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID,_testTypeID).ToString();
            lblDate.Text = clsTestAppointment.Find(_testAppointmentID).AppointmentDate.ToShortDateString();
            lblFees.Text = clsTestType.GetTestTypeData(_testTypeID).TestTypeFees.ToString();
            lblTestID.Text = "Not Taken yet";

            switch (_testTypeID)
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

            rbPass.Checked = true;

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _FillInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isPassed = rbPass.Checked;
            int TestID = clsTest.Save(_testAppointmentID, isPassed, tbNotes.Text, clsGlobalSettings.LoggedInUser.UserID);
            if (MessageBox.Show("Are you sure you want to save?\nAfter that you can't change the Pass/Fail results after you save", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                if ((TestID != -1) && clsTestAppointment.LockTestAppointment(_testAppointmentID))
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            this.Close();
        }
    }
}
