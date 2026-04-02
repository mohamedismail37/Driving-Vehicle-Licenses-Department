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

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        clsLicense license = new clsLicense(); // also he created clsLicense and LicenseID both private
        clsPerson person = new clsPerson(); // Composition for driver in license -> and person in Driver /-> So, he don't need it
        // and her a public get function for licenseID & clsLicense
        // So, he make composotion for license in the form
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

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
            lblClass.Text = clsLicenseClass.Find(license.LicenseClassID).ClassName;
            lblName.Text = person.FullName;
            lblLicenseID.Text = license.LicenseID.ToString();
            lblNationalNo.Text = person.NationalNo;
            if (person.Gender == 0)
                lblGender.Text = "Male";
            else
                lblGender.Text = "Female";
            lblIssueDate.Text = license.IssueDate.ToShortDateString();
            lblIssueReason.Text = license.IssueReasonInText();
            if (string.IsNullOrEmpty(license.Notes))
                lblNotes.Text = "No Notes";
            else
                lblNotes.Text = license.Notes;

            if (license.IsActive == false)
                lblIsActive.Text = "No";
            else
                lblIsActive.Text = "Yes";
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = license.Driver.DriverID.ToString();
            lblExpirationDate.Text = license.ExpirationDate.ToShortDateString();
            if (clsDetainedLicense.IsDetainedLicense(license.LicenseID))
                lblIsDetained.Text = "Yes";
            else
                lblIsDetained.Text = "No";


            _LoadPersonImage();
        }

        public void LoadDriverLicenseInfo(int LicenseID)
        {
            license = clsLicense.FindByLicenseID(LicenseID);
            person = clsPerson.Find(license.Driver.PersonID);
            _FillDriverLicenseInfo();
        }

    }
}
