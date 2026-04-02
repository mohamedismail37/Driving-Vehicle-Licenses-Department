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
using DVLD.Global_Classes;

namespace DVLD.Licenses.Controls
{
    public partial class ctrlLicenseInfoWithFilter: UserControl
    {
        private int _LicenseID = -1;
        private clsLicense _License;
        public bool PermissionToIssue = false;
        public bool IsFound = false;
        public ctrlLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public event EventHandler OnLicenseSelected;

        public clsLicense License()
        {
            return _License;
        }

        public void SearchFilter(bool Status )
        {
            gbFilter.Enabled = Status;
            // Better than this you can make it as a public member in the head of the ctrl ^
            // and make this in the Load | fillInfo method
            /// and better than this too, is making it private then make {set & get} methods for it
        }

        private void _FillInfo()
        {
            ctrlDriverLicenseInfo1.LoadDriverLicenseInfo(_LicenseID);
        }

        private void btnLicenseSearch_Click(object sender, EventArgs e)
        {
            _LicenseID = int.Parse(txtLicenseID.Text);
            _License = clsLicense.FindByLicenseID(_LicenseID);
            
            if (_License == null)
            {
                MessageBox.Show("Error: No License with this ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PermissionToIssue = false;
            }
            else
            {
                _FillInfo();
                PermissionToIssue = true;
                IsFound = true;
            }

            OnLicenseSelected?.Invoke(this, EventArgs.Empty);
        }

        public void RefreshControl()
        {
            _FillInfo();
        }

        public void LoadLicense(int LicenseID)
        {
            _License = clsLicense.FindByLicenseID(LicenseID);
            _LicenseID = LicenseID;

            if (_License == null)
            {
                MessageBox.Show("Error: No License with this ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PermissionToIssue = false;
            }
            else
            {
                _FillInfo();
                PermissionToIssue = true;
                IsFound = true;
                this.txtLicenseID.Text = LicenseID.ToString();
            }

            SearchFilter(false);
            OnLicenseSelected?.Invoke(this, EventArgs.Empty);
        }

    }
}
