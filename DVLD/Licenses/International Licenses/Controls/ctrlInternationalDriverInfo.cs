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
using DVLD.Properties;
using System.IO;

namespace DVLD.Licenses.International_Licenses.Controls
{
    public partial class ctrlInternationalDriverInfo: UserControl
    {
        public ctrlInternationalDriverInfo()
        {
            InitializeComponent();
        }

        clsInternationalLicense license = new clsInternationalLicense();
        clsPerson person = new clsPerson(); 

        private void _LoadPersonImage()
        {
            string ImagePath = person.ImagePath;

            if (!string.IsNullOrEmpty(ImagePath))
            {
                if (File.Exists(ImagePath))
                {
                    pbPersonImage.ImageLocation = ImagePath;
                }
            }
            else
            {
                if (person.Gender == 0)
                    pbPersonImage.Image = Resources.Male_512;
                else
                    pbPersonImage.Image = Resources.Female_512;
            }
        }

        private void _FillDriverLicenseInfo()
        {
            lblIntLicenseID.Text = license.InternationalLicenseID.ToString();
            lblApplicationID.Text = license.ApplicationID.ToString();
            lblName.Text = person.FullName;
            lblLicenseID.Text = license.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = person.NationalNo;
            if (person.Gender == 0)
                lblGender.Text = "Male";
            else
                lblGender.Text = "Female";
            lblIssueDate.Text = license.IssueDate.ToShortDateString();

            if (license.IsActive == false)
                lblIsActive.Text = "No";
            else
                lblIsActive.Text = "Yes";
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = license.DriverID.ToString();
            lblExpirationDate.Text = license.ExpirationDate.ToShortDateString();

            _LoadPersonImage();
        }

        public void LoadDriverLicenseInfo(int InternationalLicenseID)
        {
            license = clsInternationalLicense.FindByInternationalLicenseID(InternationalLicenseID);
            person = clsPerson.Find(clsLicense.FindByLicenseID(license.IssuedUsingLocalLicenseID).Driver.PersonID);
            _FillDriverLicenseInfo();
        }

    }
}
