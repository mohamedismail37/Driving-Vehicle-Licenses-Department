using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.ApplicationTypes
{
    public partial class frmListApplicationTypes: Form
    {
        private static DataTable _dtAllApplications = clsApplicationType.GetAllApplicationTypes();

        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshUsersList()
        {
            _dtAllApplications = clsApplicationType.GetAllApplicationTypes();
            dgvAllApplicationTypes.DataSource = _dtAllApplications;

            lblNumRecords.Text = dgvAllApplicationTypes.RowCount.ToString();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();

            if (dgvAllApplicationTypes.Rows.Count > 0)
            {
                dgvAllApplicationTypes.Columns[0].HeaderText = "ID";
                dgvAllApplicationTypes.Columns[0].Width = 110;

                dgvAllApplicationTypes.Columns[1].HeaderText = "Title";
                dgvAllApplicationTypes.Columns[1].Width = 410;

                dgvAllApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvAllApplicationTypes.Columns[2].Width = 110;
            }

        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frm = new frmUpdateApplicationType((int)dgvAllApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshUsersList();
        }
    }
}
