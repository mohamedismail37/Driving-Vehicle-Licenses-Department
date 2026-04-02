using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class clsInternationalLicense
    {
        public int InternationalLicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; } //Composition...
        public int IssuedUsingLocalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }
        public int CreatedByUserID { set; get; }

        public clsInternationalLicense()
        {
            InternationalLicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            IssuedUsingLocalLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = false;
            CreatedByUserID = -1;
        }

        private clsInternationalLicense(int internationalLicenseID, int applicationID, int driverID, int issuedUsingLocalLicenseID,
            DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {
            InternationalLicenseID = internationalLicenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedByUserID = createdByUserID;
        }

        public static clsInternationalLicense FindByInternationalLicenseID(int InternationalLicenseID)
        {
            int appID = -1, driverID = -1, issuedUsingLocalLicenseID = -1;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now;
            bool isActive = false;
            int createdByUserID = -1;

            bool IsFound = DataAccessLayer.clsInternationalLicenseData.GetInternationalLicenseInfoByInternationalLicenseID(InternationalLicenseID, ref appID, ref driverID, ref issuedUsingLocalLicenseID,
                ref issueDate, ref expirationDate, ref isActive, ref createdByUserID);

            if (IsFound)
            {
                return new clsInternationalLicense(InternationalLicenseID, appID, driverID, issuedUsingLocalLicenseID,
                 issueDate, expirationDate, isActive, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsInternationalLicense FindByLocalLicenseID(int IssuedUsingLocalLicenseID)
        {
            int appID = -1, driverID = -1, internationalLicenseID = -1;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now;
            bool isActive = false;
            int createdByUserID = -1;

            bool IsFound = DataAccessLayer.clsInternationalLicenseData.GetInternationalLicenseInfoByLicenseID(ref internationalLicenseID, ref appID, ref driverID, IssuedUsingLocalLicenseID,
                ref issueDate, ref expirationDate, ref isActive, ref createdByUserID);

            if (IsFound)
            {
                return new clsInternationalLicense(internationalLicenseID, appID, driverID, IssuedUsingLocalLicenseID,
                 issueDate, expirationDate, isActive, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetInternationalLicensesHistoryPersonID(int PersonID)
        {
            return DataAccessLayer.clsInternationalLicenseData.GetInternationalLicensesHistoryPersonID(PersonID);
        }

        public static bool IsHavePreviousActiveInternationalLicense(int DriverID)
        {
            return clsInternationalLicenseData.IsHavePreviousActiveInternationalLicense(DriverID);
        }

        private int _addNewLicense()
        {
            return DataAccessLayer.clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
                this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }

        public bool Save()
        {
            int internationalLicenseID = _addNewLicense();
            if (internationalLicenseID != -1)
            {
                InternationalLicenseID = internationalLicenseID;
                return true;
            }
            else
                return false;
        }

        public static DataTable GetInternationalLicenses()
        {
            return DataAccessLayer.clsInternationalLicenseData.GetInternationalLicensesInfo();
        }
    }
}
