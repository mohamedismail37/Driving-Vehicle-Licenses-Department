using BusinessLogicLayer;
using DVLD.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DVLD.Applications
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication: Form
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode currentMode;

        private int _localDrivingLicenseAppID = -1;
        clsApplication newApplication = new clsApplication();
        clsLocalDrivingLicenseApplication newLocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            currentMode = enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            currentMode = enMode.Update;
            _localDrivingLicenseAppID = LocalDrivingLicenseApplicationID;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillcbLicenseClassNames()
        {
            string[] arr = clsLicenseClass.ClassNames();

            for (int i = 0; i < arr.Length; i++)
            {
                cbLicenseClasses.Items.Add(arr[i]);
            }

            cbLicenseClasses.SelectedIndex = 2;
        }

        private void _LoadData()
        {
            this.Text = "Update Local Driving License Application";
            lblTitle.Text = "Update Local Driving License Application";
            newApplication = clsApplication.FindByLocalDrivingLicenseApplicationID(_localDrivingLicenseAppID);
            newLocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseAppInfo(_localDrivingLicenseAppID);
            this.ctrlPersonCardWithFilter1.LoadPersonInfo(newApplication.ApplicantPersonID);
            ctrlPersonCardWithFilter1.Enabled = false;
            cbLicenseClasses.SelectedIndex = newLocalDrivingLicenseApplication.LicenseClassID - 1;
        }
        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillcbLicenseClassNames();

            if (currentMode == enMode.Update)
                _LoadData();
            
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = clsApplicationType.Find(1).ApplicationFees.ToString(); // Instead of (1), he did it as Enum (enApplicationTypes)
            lblCreatedBy.Text = clsGlobalSettings.LoggedInUser.UserName;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentMode == enMode.Update)
            {
                newLocalDrivingLicenseApplication.Mode = clsLocalDrivingLicenseApplication.enMode.Update;
                newLocalDrivingLicenseApplication.LicenseClassID = cbLicenseClasses.SelectedIndex + 1;
                newApplication.currentMode = clsApplication.enMode.UpdateApplication;
            }

            int appID = clsApplication.IsApplicantHaveInCompleteApplcation(ctrlPersonCardWithFilter1.PersonID, cbLicenseClasses.SelectedIndex + 1); // For License Class He created find method but with (ClassName)

            if (appID != -1)
            {
                MessageBox.Show("Choose another License Class, The selected person already have an active application for the selected class with ID = " + appID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsDriver.IsDriverExistByPersonID(ctrlPersonCardWithFilter1.PersonID))
            {
                if (clsLicense.IsDriverHaveThisLicenseClass(clsDriver.FindByPersonID(ctrlPersonCardWithFilter1.PersonID).DriverID, cbLicenseClasses.SelectedIndex + 1))
                {
                    MessageBox.Show("Person already have a license with the same applied driving class, Choose different driving class.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            newApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            newApplication.ApplicationDate = DateTime.Now;
            newApplication.ApplicationTypeID = 1; // By defualt = 1, because we in the frmNewLocalDrivingLicenseApplcation 
            newApplication.ApplicationStatus = (clsApplication.enApplicationStatus)1;
            newApplication.LastStatusDate = DateTime.Now;
            newApplication.PaidFees = float.Parse(lblApplicationFees.Text);
            newApplication.CreatedByUserID = clsGlobalSettings.LoggedInUser.UserID;

            if (newApplication.Save())
            {
                newLocalDrivingLicenseApplication.ApplicationID = newApplication.ApplicationID;
                newLocalDrivingLicenseApplication.LicenseClassID = cbLicenseClasses.SelectedIndex + 1;

                if (newLocalDrivingLicenseApplication.Save())
                {
                    lblLocalDrivingLicenseApplicationID.Text = newLocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    currentMode = enMode.Update;
                    this.frmNewLocalDrivingLicenseApplication_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Data did NOT Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Data did NOT Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        // On ActivatedMethod: FilterFocus() -> Nice Idea to know 
    }
}
