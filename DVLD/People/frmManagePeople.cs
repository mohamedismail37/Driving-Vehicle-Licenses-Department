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
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleList()
        {
            dgvAllPeople.DataSource = clsPerson.GetAllPeople();
            lblNumRecords.Text = dgvAllPeople.RowCount.ToString();

        }

        private void _FillcbFilteration()
        {
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
            _RefreshPeopleList();
            _FillcbFilteration();
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
            string filterText = tbFilteration.Text.Trim();
            DataView dv = clsPerson.GetAllPeople().DefaultView;
            if (filterText.Length > 0)
            {
                switch (cbFiltartion.SelectedIndex)
                {
                    case 1:
                            dv.RowFilter = string.Format("[Person ID] = {0}", filterText);
                            break;

                    case 2:
                                dv.RowFilter = string.Format("[National No.] LIKE '%{0}%'", filterText);
                                break;
                    case 3:
                                dv.RowFilter = string.Format("[First Name] LIKE '%{0}%'", filterText);
                                break;
                            case 4:
                                dv.RowFilter = string.Format("[Second Name] LIKE '%{0}%'", filterText);
                                break;
                            case 5:
                                dv.RowFilter = string.Format("[Third Name] LIKE '%{0}%'", filterText);
                                break;
                            case 6:
                                dv.RowFilter = string.Format("[Last Name] LIKE '%{0}%'", filterText);
                                break;
                            case 7:
                                dv.RowFilter = string.Format("[Nationality] LIKE '%{0}%'", filterText);
                                break;
                            case 8:
                                dv.RowFilter = string.Format("[Gender] LIKE '%{0}%'", filterText);
                                break;
                            case 9:
                                dv.RowFilter = string.Format("[Phone] LIKE '%{0}%'", filterText);
                                break;
                            case 10:
                                dv.RowFilter = string.Format("[Email] LIKE '%{0}%'", filterText);
                                break;
                            }
                        }
                        else
                        {
                            dgvAllPeople.DataSource = clsPerson.GetAllPeople();
                        }
                        dgvAllPeople.DataSource = dv;

                } 

        private void cbFiltartion_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilteration.Visible = true;
        }

        private void tbFilteration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFiltartion.SelectedIndex == 1)
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
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
            if (MessageBox.Show("Are you sure do you want to delete this person?", "Delete Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson((int)dgvAllPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }
                else
                    MessageBox.Show("Person Not Deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();
        }
    }
}
