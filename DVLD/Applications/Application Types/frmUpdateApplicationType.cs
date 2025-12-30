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
    public partial class frmUpdateApplicationType: Form
    {
        private int _ApplicationTypeID = -1;
        private clsApplicationType _ApplicationType;
        public frmUpdateApplicationType(int ApplicationID)
        {
            InitializeComponent();

            _ApplicationTypeID = ApplicationID;
        }
        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            lblID.Text = _ApplicationTypeID.ToString() ;

            if (_ApplicationType != null)
            {
                txtApplicationTitle.Text = _ApplicationType.ApplicationTypeTitle;
                txtApplicationFees.Text = ((int)_ApplicationType.ApplicationFees).ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            /// Validate here..

            if (txtApplicationTitle.Text != _ApplicationType.ApplicationTypeTitle || txtApplicationFees.Text != _ApplicationType.ApplicationFees.ToString())
            {
                _ApplicationType.ApplicationTypeTitle = txtApplicationTitle.Text;
                _ApplicationType.ApplicationFees = decimal.Parse(txtApplicationFees.Text); 

                if (_ApplicationType.Save())
                {
                    MessageBox.Show("Data Saved Successfully!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmUpdateApplicationType_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Data Did NOT Saved!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
