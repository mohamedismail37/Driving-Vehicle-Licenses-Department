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
using DVLD.Licenses;

namespace DVLD.Drivers
{
    public partial class frmListDrivers: Form
    {
        private DataTable _dtDrivers;

        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshDriversList()
        {
            _dtDrivers = clsDriver.GetDriversData();

            dgvDrivers.DataSource = _dtDrivers;
            lblNumRecords.Text = dgvDrivers.RowCount.ToString();
        }

        private void _FillcbFilteration()
        {
            cbFiltartion.Items.Add("None");
            cbFiltartion.Items.Add("Driver ID");
            cbFiltartion.Items.Add("Person ID.");
            cbFiltartion.Items.Add("National");
            cbFiltartion.Items.Add("Full Name");

            cbFiltartion.SelectedIndex = 0;

            tbFilteration.Visible = false;
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _RefreshDriversList();
            _FillcbFilteration();

            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 80;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 80;

                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 80;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 200;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 120;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 80;
            }
        }

        private void cbFiltartion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFiltartion.Text == "None")
                tbFilteration.Visible = false;
            else
                tbFilteration.Visible = true;
        }

        private void tbFilteration_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFiltartion.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Date":
                    FilterColumn = "Created Date";
                    break;
                case "Active Licenses":
                    FilterColumn = "ActiveLicense";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            // Reset the filters in case nothing selected or filter value contains nothing

            if (tbFilteration.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblNumRecords.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID" || FilterColumn == "DriverID")
            {
                // In this casse we deal with integer not String.

                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, tbFilteration.Text.Trim());
            }
            else
            {
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, tbFilteration.Text.Trim());
            }


            lblNumRecords.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

            _RefreshDriversList();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }
    }
}