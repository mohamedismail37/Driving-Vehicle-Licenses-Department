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
using System.Collections.Specialized;
using DVLD.Licenses.International_Licenses;

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _personID = -1; // He make here clsDriver, DriverID -> Because he already created composition for it
        private static DataTable _dtLocalLicenses = new DataTable();
        private static DataTable _dtInternationalLicenses = new DataTable();
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }
        // Also, he separated each fill of dgv in a specific method
        private void _filldgv()
        {
            // Local:
            _dtLocalLicenses = clsLicense.GetLocalLicensesHistoryByPersonID(_personID);
            dgvLocal.DataSource = _dtLocalLicenses;
            lblNumRecords.Text = dgvLocal.RowCount.ToString();

            // International:
            _dtInternationalLicenses = clsInternationalLicense.GetInternationalLicensesHistoryPersonID(_personID);
            dgvInternational.DataSource = _dtInternationalLicenses;
            lblNumRecordsInternational.Text = dgvInternational.RowCount.ToString();


        }
        public void LoadDriverLicenses(int PersonID)
        {
            _personID = PersonID;
            // You should handle Wrong personID sent \ Just as double check
            _filldgv();

            if (dgvLocal.Rows.Count > 0)
            {
                dgvLocal.Columns[0].HeaderText = "Lic. ID";
                dgvLocal.Columns[0].Width = 110;

                dgvLocal.Columns[1].HeaderText = "App. ID";
                dgvLocal.Columns[1].Width = 110;

                dgvLocal.Columns[2].HeaderText = "Class Name";
                dgvLocal.Columns[2].Width = 210;

                dgvLocal.Columns[3].HeaderText = "Issue Date";
                dgvLocal.Columns[3].Width = 110;

                dgvLocal.Columns[4].HeaderText = "Expiration Date";
                dgvLocal.Columns[4].Width = 110;

                dgvLocal.Columns[5].HeaderText = "Is Active";
                dgvLocal.Columns[5].Width = 110;
            }

            if (dgvInternational.Rows.Count > 0)
            {
                dgvInternational.Columns[0].HeaderText = "Int License ID";
                dgvInternational.Columns[0].Width = 110;

                dgvInternational.Columns[1].HeaderText = "Application ID";
                dgvInternational.Columns[1].Width = 110;

                dgvInternational.Columns[2].HeaderText = "L.License ID";
                dgvInternational.Columns[2].Width = 210;

                dgvInternational.Columns[3].HeaderText = "Issue Date";
                dgvInternational.Columns[3].Width = 110;

                dgvInternational.Columns[4].HeaderText = "Expiration Date";
                dgvInternational.Columns[4].Width = 110;

                dgvInternational.Columns[5].HeaderText = "Is Active";
                dgvInternational.Columns[5].Width = 110;
            }

        }
        // you can add the same load function, but with DriverID instead of PersonID as ++

        public void Clear()
        {
            _dtLocalLicenses.Clear();
            _dtInternationalLicenses.Clear();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo((int)dgvLocal.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmInternationalDriverInfo frm = new frmInternationalDriverInfo((int)dgvInternational.CurrentRow.Cells[0].Value);
            frm.Show();
        }

    }
}
