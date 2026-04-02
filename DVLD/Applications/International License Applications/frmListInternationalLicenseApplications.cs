using BusinessLogicLayer;
using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.International_License_Applications
{
    public partial class frmListInternationalLicenseApplications: Form
    {
        // The Only question here,
        // Where is the inherited Form?!
        private static DataTable _dtInternationalLicensesApplications = clsInternationalLicense.GetInternationalLicenses();
        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();

            _RefreshInternationalDrivingLicenseApplicationsList();
        }

        private void _RefreshInternationalDrivingLicenseApplicationsList()
        {
            _dtInternationalLicensesApplications = clsInternationalLicense.GetInternationalLicenses();
            dgvAllLDLApplications.DataSource = _dtInternationalLicensesApplications;

            lblNumRecords.Text = dgvAllLDLApplications.RowCount.ToString();
        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _RefreshInternationalDrivingLicenseApplicationsList();

            cbFiltertion.SelectedIndex = 0;

            if (dgvAllLDLApplications.Rows.Count > 0)
            {
                dgvAllLDLApplications.Columns[0].HeaderText = "Int. License ID";
                dgvAllLDLApplications.Columns[0].Width = 150;

                dgvAllLDLApplications.Columns[1].HeaderText = "Appllication ID";
                dgvAllLDLApplications.Columns[1].Width = 150;

                dgvAllLDLApplications.Columns[2].HeaderText = "Driver ID";
                dgvAllLDLApplications.Columns[2].Width = 130;

                dgvAllLDLApplications.Columns[3].HeaderText = "L. License ID";
                dgvAllLDLApplications.Columns[3].Width = 150;

                dgvAllLDLApplications.Columns[4].HeaderText = "Issue Date";
                dgvAllLDLApplications.Columns[4].Width = 160;

                dgvAllLDLApplications.Columns[5].HeaderText = "Expiration Date";
                dgvAllLDLApplications.Columns[5].Width = 160;

                dgvAllLDLApplications.Columns[6].HeaderText = "Is Active";
                dgvAllLDLApplications.Columns[6].Width = 150;
            }
        }

        private void cbFiltertion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFiltertion.Text == "None")
            {
                cbStatusFilteration.Visible = false;
                txtFilteration.Visible = false;
            }
            else if (cbFiltertion.Text == "Is Active")
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
                case "Activated":
                    _dtInternationalLicensesApplications.DefaultView.RowFilter = string.Format("[IsActive] = 1");
                    break;
                case "Deactivated":
                    _dtInternationalLicensesApplications.DefaultView.RowFilter = string.Format("[IsActive] = 0");
                    break;
            }

            lblNumRecords.Text = dgvAllLDLApplications.Rows.Count.ToString();

        }

        private void txtFilteration_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFiltertion.Text)
            {
                case "Int. License ID":
                    filterColumn = "InternationalLicenseID";
                    break;
                case "Application ID.":
                    filterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    filterColumn = "DriverID";
                    break;
                case "L. License ID":
                    filterColumn = "IssuedUsingLocalLicenseID";
                    break;
                case "Issue Date":
                    filterColumn = "IssueDate";
                    break;
                case "Expiration Date":
                    filterColumn = "ExpirationDate";
                    break;
                case "Is Active":
                    filterColumn = "IsActive";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }

            if (txtFilteration.Text.Trim() == "" || filterColumn == "None")
            {
                _dtInternationalLicensesApplications.DefaultView.RowFilter = "";
                lblNumRecords.Text = dgvAllLDLApplications.Rows.Count.ToString();
                return;
            }

            _dtInternationalLicensesApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilteration.Text.Trim());

            lblNumRecords.Text = dgvAllLDLApplications.Rows.Count.ToString();
        }

        private void tsmiShowPersonDetails_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(clsDriver.FindByDriverID((int)dgvAllLDLApplications.CurrentRow.Cells[2].Value).PersonID);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalDriverInfo frm = new frmInternationalDriverInfo((int)dgvAllLDLApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(clsDriver.FindByDriverID((int)dgvAllLDLApplications.CurrentRow.Cells[2].Value).PersonID);
            frm.ShowDialog();
        }
    }
}
