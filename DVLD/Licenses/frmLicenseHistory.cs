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

namespace DVLD.Licenses
{
    public partial class frmLicenseHistory: Form
    {
        private int _PersonID = -1;
        public frmLicenseHistory(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
        }
        public frmLicenseHistory()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                this.ctrlDriverLicenses1.LoadDriverLicenses(_PersonID);
                this.ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                this.ctrlPersonCardWithFilter1.FilterEnabled = false;
            }
            else
            {
                ctrlPersonCardWithFilter1.Enabled = true;
            }

        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
            if (_PersonID == -1)
            {
                ctrlDriverLicenses1.Clear();
            }
            else
            {
                ctrlDriverLicenses1.LoadDriverLicenses(_PersonID);
            }
        }
    }
}
