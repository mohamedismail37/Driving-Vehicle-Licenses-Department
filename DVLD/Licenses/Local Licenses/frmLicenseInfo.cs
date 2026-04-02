using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmLicenseInfo: Form
    {
        // The best practise 
        private int _LicenseID = -1;
        public frmLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
            this.ctrlDriverLicenseInfo1.LoadDriverLicenseInfo(LicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {
            this.ctrlDriverLicenseInfo1.LoadDriverLicenseInfo(_LicenseID);
        }
    }
}
