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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Licenses.Detained_Licenses
{
    public partial class frmListDetainedLicenses: Form
    {
        private static DataTable _dtDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();

            _RefreshDetainedLicensesList();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();

            _RefreshDetainedLicensesList();
        }

        private void _RefreshDetainedLicensesList()
        {
            _dtDetainedLicenses = BusinessLogicLayer.clsDetainedLicense.GetDetainedLicesesData();
            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;

            lblNumRecords.Text = dgvDetainedLicenses.RowCount.ToString();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _RefreshDetainedLicensesList();

            cbFiltertion.SelectedIndex = 0;

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D. ID";
                dgvDetainedLicenses.Columns[0].Width = 110;

                dgvDetainedLicenses.Columns[1].HeaderText = "L. ID";
                dgvDetainedLicenses.Columns[1].Width = 110;

                dgvDetainedLicenses.Columns[2].HeaderText = "D. Date";
                dgvDetainedLicenses.Columns[2].Width = 130;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 130;

                dgvDetainedLicenses.Columns[6].HeaderText = "N. No";
                dgvDetainedLicenses.Columns[6].Width = 110;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 230;

                dgvDetainedLicenses.Columns[8].HeaderText = "Release App ID";
                dgvDetainedLicenses.Columns[8].Width = 110;
            }
        }

        private void cbFiltertion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFiltertion.Text == "None")
            {
                cbStatusFilteration.Visible = false;
                txtFilteration.Visible = false;
            }
            else if (cbFiltertion.Text == "Is Released")
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

            lblNumRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void cbStatusFilteration_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbStatusFilteration.Text)
            {
                case "Is Released":
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[IsReleased] = 1");
                    break;
                case "Non-Released":
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[IsReleased] = 0");
                    break;
            }
            lblNumRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void txtFilteration_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFiltertion.Text)
            {
                case "D. ID":
                    filterColumn = "DetainID";
                    break;
                case "L. ID":
                    filterColumn = "LicenseID";
                    break;
                case "N. No":
                    filterColumn = "NationalNo";
                    break;
                case "Full Name":
                    filterColumn = "FullName";
                    break;
                case "Release App. ID":
                    filterColumn = "ReleaseApplicationID";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }

            if (txtFilteration.Text.Trim() == "" || filterColumn == "None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblNumRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                return;
            }

            if (!(filterColumn == "NationalNo" || filterColumn == "FullName" ))
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilteration.Text.Trim());
            }
            else
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, txtFilteration.Text.Trim());
            }

            lblNumRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void cmsOperations_Opening(object sender, CancelEventArgs e)
        {
            if (bool.Parse(dgvDetainedLicenses.CurrentRow.Cells[3].Value.ToString()) == true )
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            }
            else
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;
            }

        }

        private void tsmiPersonDetails_Click(object sender, EventArgs e)
        {
            int PersonID = clsPerson.Find(dgvDetainedLicenses.CurrentRow.Cells[6].Value.ToString()).PersonID;
            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
            _RefreshDetainedLicensesList();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(int.Parse(dgvDetainedLicenses.CurrentRow.Cells[1].Value.ToString()));
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsPerson.Find(dgvDetainedLicenses.CurrentRow.Cells[6].Value.ToString()).PersonID;
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(dgvDetainedLicenses.CurrentRow.Cells[1].Value.ToString());

            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(LicenseID);
            frm.Show();

        }
    }
}
