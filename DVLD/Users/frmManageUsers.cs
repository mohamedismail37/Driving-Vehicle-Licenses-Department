using BusinessLogicLayer;
using DVLD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmManageUsers: Form
    {
        private static DataTable _dtAllUsers = clsUser.GetAllUsers();

        // Select from the _dtAllUsers the needed columns & Modify on this DataTable to show in the dgv
        private DataTable _dtUsers = _dtAllUsers.DefaultView.ToTable(false, "UserID", "PersonID", "Full Name", "UserName", "IsActive");
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            _dtUsers = _dtAllUsers.DefaultView.ToTable(false, "UserID", "PersonID", "Full Name", "UserName", "IsActive");
            dgvAllUsers.DataSource = _dtUsers;

            lblNumRecords.Text = dgvAllUsers.RowCount.ToString();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            cbFiltertion.SelectedIndex = 0;

            if (dgvAllUsers.Rows.Count > 0)
            {
                dgvAllUsers.Columns[0].HeaderText = "User ID";
                dgvAllUsers.Columns[0].Width = 110;

                dgvAllUsers.Columns[1].HeaderText = "Person ID";
                dgvAllUsers.Columns[1].Width = 110;

                dgvAllUsers.Columns[2].HeaderText = "Full Name";
                dgvAllUsers.Columns[2].Width = 310;

                dgvAllUsers.Columns[3].HeaderText = "User Name";
                dgvAllUsers.Columns[3].Width = 110;

                dgvAllUsers.Columns[4].HeaderText = "Is Active";
                dgvAllUsers.Columns[4].Width = 110;
            }

        }

        private void btnAddEditPerson_Click(object sender, EventArgs e)
        {

            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void cbFiltertion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFiltertion.Text == "None")
            {
                cbIsActiveFilteration.Visible = false;
                txtFilteration.Visible = false;
            }
            else if (cbFiltertion.Text == "Is Active")
            {
                cbIsActiveFilteration.Visible = true;
                txtFilteration.Visible = false;
                cbIsActiveFilteration.SelectedIndex = 0;
            }
            else
            {
                cbIsActiveFilteration.Visible = false;
                txtFilteration.Visible = true;
            }
        }

        private void txtFilteration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFiltertion.Text == "User ID" || cbFiltertion.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cbIsActiveFilteration_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbIsActiveFilteration.Text)
            {
                case "All":
                    _dtUsers.DefaultView.RowFilter = "";
                    break;
                case "Yes":
                    _dtUsers.DefaultView.RowFilter = string.Format("[IsActive] = 1");
                    break;
                case "No":
                    _dtUsers.DefaultView.RowFilter = string.Format("[IsActive] = 0");
                    break;
            }

            lblNumRecords.Text = _dtUsers.Rows.Count.ToString();
        }

        private void txtFilteration_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFiltertion.Text)
            {
                case "User ID":
                    filterColumn = "UserID";
                    break;
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "Full Name":
                    filterColumn = "Full Name";
                    break;
                case "User Name":
                    filterColumn = "UserName";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }

            if (txtFilteration.Text.Trim() == "" || filterColumn == "None")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblNumRecords.Text = _dtUsers.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "PersonID" || filterColumn == "UserID")
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilteration.Text.Trim());
            }
            else
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, txtFilteration.Text.Trim());
            }

            lblNumRecords.Text = _dtUsers.Rows.Count.ToString();

        }

        private void tsmiAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.Show();

            _RefreshUsersList();
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {

            frmAddUpdateUser frm = new frmAddUpdateUser((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to delete this user [" + dgvAllUsers.CurrentRow.Cells[0].Value.ToString() + "]", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsUser.DeleteUser((int)dgvAllUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshUsersList();
                }
                else
                {
                    MessageBox.Show("User didn't deleted", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshUsersList();

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshUsersList();
        }
    }
}
