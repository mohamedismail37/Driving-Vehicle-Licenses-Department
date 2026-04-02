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
using DVLD.Applications.Local_Driving_License;
using DVLD.Licenses;
using DVLD.Tests.Test_Appointments;

namespace DVLD.Applications
{
    public partial class frmLocalDrivingLicenseApplicationsList: Form
    {
        private static DataTable _dtAllLocalDrivingLicenseApplications = BusinessLogicLayer.clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

        // Select from the _dtAllLocalDrivingLicenseApplications the needed columns & Modify on this DataTable to show in the dgv
        private DataTable _dtLocalDrivingLicenseApplications =  _dtAllLocalDrivingLicenseApplications.DefaultView.ToTable(false, "LocalDrivingLicenseApplicationID", "ClassName", "NationalNo", "FullName", "ApplicationDate", "PassedTests", "Status");
        public frmLocalDrivingLicenseApplicationsList()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplicationsList();
        }
        private void _RefreshLocalDrivingLicenseApplicationsList()
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            _dtLocalDrivingLicenseApplications = _dtAllLocalDrivingLicenseApplications.DefaultView.ToTable(false, "LocalDrivingLicenseApplicationID", "ClassName", "NationalNo", "FullName", "ApplicationDate", "PassedTests", "Status");
            dgvAllLDLApplications.DataSource = _dtLocalDrivingLicenseApplications;

            lblNumRecords.Text = dgvAllLDLApplications.RowCount.ToString();
        }
        private void frmLocalDrivingLicenseApplicationsList_Load(object sender, EventArgs e)
        {
            _RefreshLocalDrivingLicenseApplicationsList();

            cbFiltertion.SelectedIndex = 0;

            if (dgvAllLDLApplications.Rows.Count > 0)
            {
                dgvAllLDLApplications.Columns[0].HeaderText = "L.D.L AppID";
                dgvAllLDLApplications.Columns[0].Width = 90;

                dgvAllLDLApplications.Columns[1].HeaderText = "Driving Class";
                dgvAllLDLApplications.Columns[1].Width = 270;

                dgvAllLDLApplications.Columns[2].HeaderText = "National No.";
                dgvAllLDLApplications.Columns[2].Width = 90;

                dgvAllLDLApplications.Columns[3].HeaderText = "Full Name";
                dgvAllLDLApplications.Columns[3].Width = 310;

                dgvAllLDLApplications.Columns[4].HeaderText = "Application Date";
                dgvAllLDLApplications.Columns[4].Width = 110;

                dgvAllLDLApplications.Columns[5].HeaderText = "Passed Tests";
                dgvAllLDLApplications.Columns[5].Width = 90;

                dgvAllLDLApplications.Columns[6].HeaderText = "Status";
                dgvAllLDLApplications.Columns[6].Width = 110;
            }
        }

        private void cbFiltertion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFiltertion.Text == "None")
            {
                cbStatusFilteration.Visible = false;
                txtFilteration.Visible = false;
            }
            else if (cbFiltertion.Text == "Status")
            {
                cbStatusFilteration.Visible = true;
                txtFilteration.Visible = false;
                cbStatusFilteration.SelectedIndex = 0;
            }
            else
            {
                cbStatusFilteration.Visible = false;
                txtFilteration.Visible = true;
            }

            lblNumRecords.Text = dgvAllLDLApplications.Rows.Count.ToString();
        }

        private void cbStatusFilteration_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbStatusFilteration.Text)
            {
                case "Cancelled":
                    _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[Status] LIKE 'Cancelled'");
                    break;
                case "Completed":
                    _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[Status] LIKE 'Completed'");
                    break;
                case "New":
                    _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[Status] LIKE 'New'");
                    break;

            }

            lblNumRecords.Text = dgvAllLDLApplications.Rows.Count.ToString();
        }

        private void txtFilteration_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFiltertion.Text)
            {
                case "L.D.L AppID":
                    filterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    filterColumn = "NationalNo";
                    break;
                case "Full Name":
                    filterColumn = "FullName";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }

            if (txtFilteration.Text.Trim() == "" || filterColumn == "None")
            {
                _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblNumRecords.Text = dgvAllLDLApplications.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "LocalDrivingLicenseApplicationID")
            {
                _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilteration.Text.Trim());
            }
            else
            {
                _dtLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, txtFilteration.Text.Trim());
            }

            lblNumRecords.Text = dgvAllLDLApplications.Rows.Count.ToString();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to cancel this Application", "Cancel App", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clsApplication application = clsApplication.FindByLocalDrivingLicenseApplicationID((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value);
                application.ApplicationStatus = clsApplication.enApplicationStatus.Canceled;
                application.currentMode = clsApplication.enMode.UpdateApplication;

                // He resolved it as in my old Sol.CancelMethod()

                if (application.Save())
                {
                    MessageBox.Show("Application Canceled Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshLocalDrivingLicenseApplicationsList();
                }
                else
                    MessageBox.Show("Application didn't Canceled", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                return;
        }

        private void cmsOperations_Opening(object sender, CancelEventArgs e)
        {
            //
            if ((string)dgvAllLDLApplications.CurrentRow.Cells[6].Value == "Completed")
            {
                cancelApplicationToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = true;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
                tsmiEdit.Enabled = false;
                tsmiDelete.Enabled = false;
            }
            else if ((string)dgvAllLDLApplications.CurrentRow.Cells[6].Value == "Cancelled")
            {
                cancelApplicationToolStripMenuItem.Enabled = false;
                scheduleTestsToolStripMenuItem.Enabled = false;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
            }
            else
            {
                cancelApplicationToolStripMenuItem.Enabled = true;
                showLicenseToolStripMenuItem.Enabled = false;
                issueDrivingLicenseToolStripMenuItem.Enabled = true;
            }

            //
            if (int.Parse(dgvAllLDLApplications.CurrentRow.Cells[5].Value.ToString()) == 0)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = true;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
                tsmiEdit.Enabled = false;
            }
            else if (int.Parse(dgvAllLDLApplications.CurrentRow.Cells[5].Value.ToString()) == 1)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = true;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
            }
            else if (int.Parse(dgvAllLDLApplications.CurrentRow.Cells[5].Value.ToString()) == 2)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = true;
            }
            else
            {
                scheduleTestsToolStripMenuItem.Enabled = false;
            }

            //
            if (int.Parse(dgvAllLDLApplications.CurrentRow.Cells[5].Value.ToString()) == 3 && (string)dgvAllLDLApplications.CurrentRow.Cells[6].Value == "New")
            {
                issueDrivingLicenseToolStripMenuItem.Enabled = true;
            }
            else
            {
                issueDrivingLicenseToolStripMenuItem.Enabled = false ;
            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointment frm = new frmTestAppointment((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value,1);
            frm.ShowDialog();
            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointment frm = new frmTestAppointment((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value, 2);
            frm.ShowDialog();
            _RefreshLocalDrivingLicenseApplicationsList();

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointment frm = new frmTestAppointment((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value, 3);
            frm.ShowDialog();
            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void issueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicenseForFirstTime frm = new frmIssueDriverLicenseForFirstTime((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // So this funny line is result of Don't using composition and inheritance in BLL
            frmLicenseInfo frm = new frmLicenseInfo(clsLicense.FindByApplicationID(clsApplication.FindByLocalDrivingLicenseApplicationID((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value).ApplicationID).LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(clsPerson.Find((string)dgvAllLDLApplications.CurrentRow.Cells[2].Value).PersonID);
            frm.ShowDialog();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseAppID = (int)dgvAllLDLApplications.CurrentRow.Cells[0].Value;
            int applicationID = clsApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseAppID).ApplicationID;

            if (MessageBox.Show("Are you sure do you want to delete Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(localDrivingLicenseAppID))
                {
                    if (clsApplication.DeleteApplication(applicationID))
                    {
                        MessageBox.Show("Application Deleted Successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        _RefreshLocalDrivingLicenseApplicationsList();
                        return;
                    }
                }
                MessageBox.Show("Application Did NOT Deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                return;
        }

        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            frmLDLApplicationInfo frm = new frmLDLApplicationInfo((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplicationsList();
        }
    }
}
