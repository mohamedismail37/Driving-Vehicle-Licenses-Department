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

namespace DVLD.Tests.Test_Appointments
{
    public partial class frmTestAppointment: Form
    {
        private int _testTypeID = -1;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
        private void _FillInfo(int LocalDrivingLicenseAppID)
        {
            this.ctrlApplicationInfo1.LoadLocalDrivingLicenseApplicationInfo(LocalDrivingLicenseAppID);
            _localDrivingLicenseApplication = BusinessLogicLayer.clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseAppInfo(LocalDrivingLicenseAppID);
            DataTable _dtTestAppointments = BusinessLogicLayer.clsTestAppointment.GetTestAppointments(LocalDrivingLicenseAppID,_localDrivingLicenseApplication.LicenseClassID, _testTypeID);
            dgvTestAppointments.DataSource = _dtTestAppointments;
            lblNumRecords.Text = dgvTestAppointments.RowCount.ToString();

            switch (_testTypeID)
            {
                case 1:
                    this.Text = "Vision Test Appointment";
                    lblTitle.Text = "Vision Test Appointment";
                    break;
                case 2:
                    this.Text = "Written Test Appointment";
                    lblTitle.Text = "Written Test Appointment";
                    pictureBox1.ImageLocation = "C:\\Users\\moham\\Desktop\\C19)Full Real Project\\Icons\\Written Test 512.png";
                    break;
                case 3:
                    this.Text = "Street Test Appointment";
                    lblTitle.Text = "Street Test Appointment";
                    pictureBox1.ImageLocation = "C:\\Users\\moham\\Desktop\\C19)Full Real Project\\Icons\\driving-test 512.png";
                    break;
            }

        }

        public frmTestAppointment(int LocalDrivingLicenseAppID, int TestTypeID)
        {
            InitializeComponent();
            _testTypeID = TestTypeID;
            _FillInfo(LocalDrivingLicenseAppID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            if (BusinessLogicLayer.clsTestAppointment.IsApplicationHasUnlockedAppointment(_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID, _localDrivingLicenseApplication.LicenseClassID, _testTypeID))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not Allowed" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (BusinessLogicLayer.clsTestAppointment.IsApplicationPassedTheTest(_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID, _localDrivingLicenseApplication.LicenseClassID, _testTypeID))
            {
                MessageBox.Show("Person Already Passed this test, You cannot add new appointment", "Not Allowed" , MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                frmScheduleTest frm = new frmScheduleTest(_testTypeID, _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                frm.ShowDialog();

                _FillInfo(_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
            }

        }

        private void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
            if (dgvTestAppointments.Rows.Count > 0)
            {
                dgvTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvTestAppointments.Columns[0].Width = 150;

                dgvTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns[1].Width = 150;

                dgvTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns[2].Width = 150;

                dgvTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvTestAppointments.Columns[3].Width = 150;
            }
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            int _localDrivingLicneseAppID = clsTestAppointment.Find((int)dgvTestAppointments.CurrentRow.Cells[0].Value).LocalDrivingLicenseApplicationID;
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseAppInfo(_localDrivingLicneseAppID);
            frmScheduleTest frm = new frmScheduleTest(_testTypeID,_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID, (int)dgvTestAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _FillInfo(_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
        }

        private void tsmiTakeTest_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest(_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID, _testTypeID, (int)dgvTestAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _FillInfo(_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
        }

        private void cmsOperations_Opening(object sender, CancelEventArgs e)
        {
            if ((bool)dgvTestAppointments.CurrentRow.Cells[3].Value)
            {
                tsmiTakeTest.Enabled = false;
            }
            else
            {
                tsmiTakeTest.Enabled = true;
            }
        }
    }
}
