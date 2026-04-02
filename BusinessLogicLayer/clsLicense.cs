using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsLicense
    {
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public clsDriver Driver { set; get; } // Composition
        public int LicenseClassID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public float PaidFees { set; get; }
        public bool IsActive { set; get; }
        public int IssueReason { set; get; } // Enumerable
        public int CreatedByUserID { set; get; }

        // Detained Info, also Composition

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            Driver = new clsDriver();
            LicenseClassID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = 0;
            IsActive = false;
            IssueReason = -1;
            CreatedByUserID = -1;
        }

        private clsLicense(int licenseID, int appID, int driverID, int licenseClass, DateTime issueDate,
            DateTime expirationDate, string notes, float paidFees, bool isActive, int issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = appID;
            Driver = clsDriver.FindByDriverID(driverID);
            LicenseClassID = licenseClass;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
        }

        private int _addNewLicense()
        {
            return DataAccessLayer.clsLicenseData.AddNewLicense(this.ApplicationID, this.Driver.DriverID, this.LicenseClassID,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
        }

        public bool Save()
        {
            int licenseID = _addNewLicense();
            if ( licenseID != -1)
            {
                LicenseID = licenseID;
                return true;
            }
            else
                return false;
        }

        public static clsLicense FindByLicenseID(int LicenseID)
        {
            int appID = -1, driverID = -1, licenseClassID = -1;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now;
            string notes = "";
            float paidFees = 0;
            bool isActive = false;
            int issueReason = -1, createdByUserID = -1;

            bool IsFound = DataAccessLayer.clsLicenseData.GetLicenseInfoByLicenseID(LicenseID, ref appID, ref driverID, ref licenseClassID,
                ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID);

            if (IsFound)
            {
                return new clsLicense(LicenseID, appID, driverID, licenseClassID,
                 issueDate, expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsLicense FindByApplicationID(int ApplicationID)
        {
            int licenseID = -1, driverID = -1, licenseClassID = -1;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now;
            string notes = "";
            float paidFees = 0;
            bool isActive = false;
            int issueReason = -1, createdByUserID = -1;

            bool IsFound = DataAccessLayer.clsLicenseData.GetLicenseInfoByApplicationID(ref licenseID, ApplicationID , ref driverID, ref licenseClassID,
                ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID);

            if (IsFound)
            {
                return new clsLicense(licenseID, ApplicationID, driverID, licenseClassID,
                 issueDate, expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public string IssueReasonInText()
        {
            // Enum was better
            switch (IssueReason)
            {
                case 1:
                    return "First Time";
                case 2:
                    return "Renew";
                case 3:
                    return "Replacement for lost License";
                case 4:
                    return "Replacement for damaged License";
                case 5:
                    return "Release detained Driving License";
                case 6:
                    return "New International License";
                default:
                    return "";
            }
        }

        public static bool IsDriverHaveThisLicenseClass(int DriverID, int LicenseClass)
        {
            return DataAccessLayer.clsLicenseData.IsDriverHaveLicenseFromSpecificLicenseClass(DriverID, LicenseClass);
        }

        public static DataTable GetLocalLicensesHistoryByPersonID(int PersonID)
        {
            return DataAccessLayer.clsLicenseData.GetLocalLicensesHistoryByPersonID(PersonID);
        }

        public static bool DecativatedLicense(int LicenseID)
        {
            return DataAccessLayer.clsLicenseData.DecativateLicense(LicenseID);
        }

        public static bool ActivateLicense(int LicenseID)
        {
            return DataAccessLayer.clsLicenseData.ActivateLicense(LicenseID);
        }

    }
}
