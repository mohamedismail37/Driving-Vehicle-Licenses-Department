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
    public partial class frmUpdateTestType: Form
    {
        private int _TestTypeID;
        private clsTestType _TestType;
        public frmUpdateTestType(int TestTypeID)
        {
            InitializeComponent();

            _TestTypeID = TestTypeID;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.GetTestTypeData(_TestTypeID);

            if (_TestType != null)
            {
                lblID.Text = _TestTypeID.ToString();
                txtTestTitle.Text = _TestType.TestTypeTitle;
                txtDescription.Text = _TestType.TestTypeDescription;
                txtTestFees.Text = _TestType.TestTypeFees.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            // Can add here some validation for check that fees is a number && not null;

            _TestType.TestTypeTitle = txtTestTitle.Text;
            _TestType.TestTypeDescription = txtDescription.Text;
            _TestType.TestTypeFees = float.Parse(txtTestFees.Text);

            if (_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmUpdateTestType_Load(null, null);
            }
            else
            {
                MessageBox.Show("Data Did NOT Saved!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
