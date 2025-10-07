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

            DataTable dt = BLL.FillInfoUpdatePerson(PersonID);

            DataRow row = dt.Rows[0];

            NationalNo.Text = row["NationalNo"].ToString();
            Name.Text = row["FirstName"].ToString() + ' ' + row["SecondName"].ToString() + ' ' + row["ThirdName"].ToString() + ' ' + row["LastName"].ToString();
            DateOfBirth.Text = row["DateOfBirth"].ToString();
            Country.Text = (row["NationalityCountryID"].ToString());
            if (row["Gendor"].ToString() == "1")
                Gender.Text = "Female";
            else
                Gender.Text = "Male";

            Phone.Text = row["Phone"].ToString();
            Email.Text = row["Email"].ToString();
            Address.Text = row["Address"].ToString();
            pbPersonDetails.ImageLocation = row["ImagePath"].ToString();
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
