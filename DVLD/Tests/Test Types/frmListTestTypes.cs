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

namespace DVLD.Tests
{
    public partial class frmListTestTypes: Form
    {

        private DataTable _dtAllTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshTestTypesList()
        {
            _dtAllTestTypes = clsTestType.GetAllTestTypes();
            dgvAllTestTypes.DataSource = _dtAllTestTypes;
            lblNumRecords.Text = dgvAllTestTypes.RowCount.ToString();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();

            if (dgvAllTestTypes.Rows.Count > 0)
            {
                dgvAllTestTypes.Columns[0].HeaderText = "ID";
                dgvAllTestTypes.Columns[0].Width = 110;

                dgvAllTestTypes.Columns[1].HeaderText = "Title";
                dgvAllTestTypes.Columns[1].Width = 150;

                dgvAllTestTypes.Columns[2].HeaderText = "Description";
                dgvAllTestTypes.Columns[2].Width = 310;

                dgvAllTestTypes.Columns[3].HeaderText = "Fees";
                dgvAllTestTypes.Columns[3].Width = 80;
            }
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frm = new frmUpdateTestType((int)dgvAllTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshTestTypesList();
        }
    }
}
