using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Properties;
using BusinessLogicLayer;
using System.Net;

namespace DVLD
{
    public partial class PersonInfo: UserControl
    {
        enum enModes {Empty = 0, NonEmpty =1 };
        enModes CurrentMode = enModes.Empty;
        int personID = -1;
        public PersonInfo()
        {
            InitializeComponent();
            llEditPersonInfo.Enabled = false;
            pbPersonDetails.Image = Resources.Male_512;
        }

        public PersonInfo(int PersonID)
        {
            InitializeComponent();
            CurrentMode = enModes.NonEmpty;
            personID = PersonID;
        }

        public void UpdatePersonInfo(int PersonID)
        {
            llEditPersonInfo.Enabled = true;

            this.PersonID.Text = PersonID.ToString();

            clsPerson _Person = clsPerson.Find(PersonID);


            NationalNo.Text = _Person.NationalNo;
            Name.Text = _Person.FullName;
            DateOfBirth.Text = _Person.DateOfBirth.ToString();
            Country.Text = _Person.CountryInfo.CountryName;

            if (_Person.Gender == 1)
                Gender.Text = "Female";
            else
                Gender.Text = "Male";

            Phone.Text = _Person.Phone;
            Email.Text = _Person.Email;
            Address.Text = _Person.Address;
            pbPersonDetails.ImageLocation = _Person.ImagePath;
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(int.Parse(PersonID.Text));
            frm.ShowDialog();
            UpdatePersonInfo(int.Parse(PersonID.Text));
        }

        private void PersonInfo_Load(object sender, EventArgs e)
        {
            if (CurrentMode == enModes.NonEmpty)
                UpdatePersonInfo(personID);
        }
    }
}
