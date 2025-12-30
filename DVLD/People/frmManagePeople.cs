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

namespace DVLD
{
    public partial class frmManagePeople: Form
    {
        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        // Only select the columns that you want to show in the GRID
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false,"PersonID","NationalNo",
                                                                       "FirstName","SecondName","ThirdName","LastName",
                                                                       "GenderCaption","DateOfBirth","CountryName",
                                                                       "Phone","Email");

        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GenderCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dgvAllPeople.DataSource = _dtPeople;
            lblNumRecords.Text = dgvAllPeople.RowCount.ToString();
        }

        private void _FillcbFilteration()
        {
            // You Can Fill them using Designer
            cbFiltartion.Items.Add("None");
            cbFiltartion.Items.Add("Person ID");
            cbFiltartion.Items.Add("National No.");
            cbFiltartion.Items.Add("First Name");
            cbFiltartion.Items.Add("Second Name");
            cbFiltartion.Items.Add("Third Name");
            cbFiltartion.Items.Add("Last Name");
            cbFiltartion.Items.Add("Nationality");
            cbFiltartion.Items.Add("Gendor");
            cbFiltartion.Items.Add("Phone");
            cbFiltartion.Items.Add("Email");

            cbFiltartion.SelectedIndex = 0;

            tbFilteration.Visible = false;
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();//
            _FillcbFilteration();//

            if (dgvAllPeople.Rows.Count > 0)
            {
                dgvAllPeople.Columns[0].HeaderText = "Person ID";
                dgvAllPeople.Columns[0].Width = 110;

                dgvAllPeople.Columns[1].HeaderText = "National No.";
                dgvAllPeople.Columns[1].Width = 120;

                dgvAllPeople.Columns[2].HeaderText = "First Name";
                dgvAllPeople.Columns[2].Width = 120;

                dgvAllPeople.Columns[3].HeaderText = "Second Name";
                dgvAllPeople.Columns[3].Width = 140;

                dgvAllPeople.Columns[4].HeaderText = "Third Name";
                dgvAllPeople.Columns[4].Width = 120;

                dgvAllPeople.Columns[5].HeaderText = "Last Name";
                dgvAllPeople.Columns[5].Width = 120;

                dgvAllPeople.Columns[6].HeaderText = "Gender";
                dgvAllPeople.Columns[6].Width = 120;

                dgvAllPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvAllPeople.Columns[7].Width = 120;

                dgvAllPeople.Columns[8].HeaderText = "Nationality";
                dgvAllPeople.Columns[8].Width = 120;

                dgvAllPeople.Columns[9].HeaderText = "Phone";
                dgvAllPeople.Columns[9].Width = 120;

                dgvAllPeople.Columns[10].HeaderText = "Email";
                dgvAllPeople.Columns[10].Width = 170;

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddEditPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void tbFilteration_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            // Map Selected filter to real Column name

            switch (cbFiltartion.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "Nationality":
                    FilterColumn = "CountryName";
                    break;
                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            // Reset the filters in case nothing selected or filter value contains nothing

            if (tbFilteration.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblNumRecords.Text = dgvAllPeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
            {
                // In this casse we deal with integer not String.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, tbFilteration.Text.Trim());
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, tbFilteration.Text.Trim());
            }


            lblNumRecords.Text = dgvAllPeople.Rows.Count.ToString();

        }
        private void cbFiltartion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFiltartion.Text == "None")
                tbFilteration.Visible = false;
            else
                tbFilteration.Visible = true;
        }

        private void tbFilteration_KeyPress(object sender, KeyPressEventArgs e)
        {
            // We Force Numbers only Incase PersonID is Selected
            if (cbFiltartion.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tsmiAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void tsmiSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature not implemented yet","Not Ready!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void tsmiPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature not implemented yet", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to delete this person [" + dgvAllPeople.CurrentRow.Cells[0].Value.ToString() + "]", "Delete Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson((int)dgvAllPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }
                else
                    MessageBox.Show("Person was Not Deleted because it has data linked to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();//
        }

        private void dgvAllPeople_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new frmPersonDetails((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();//
        }
    }
}
