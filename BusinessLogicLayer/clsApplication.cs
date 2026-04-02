using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsApplication
    {
        public enum enMode { AddNewApplication = 0, UpdateApplication = 1 };
        public enMode currentMode;
        public enum enApplicationStatus { New = 1, Canceled = 2 , Completed = 3}
        
        public int ApplicationID { set; get; }
        public int ApplicantPersonID { set; get; } // He did Composition for Person
        public DateTime ApplicationDate { set; get; }
        public int ApplicationTypeID { set; get; } // He created ApplicationType enum
        public enApplicationStatus ApplicationStatus { set; get; }
        public DateTime LastStatusDate { set; get; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }

        public clsApplication()
        {
            currentMode = enMode.AddNewApplication;

            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = -1;
            ApplicationStatus = enApplicationStatus.New;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
        }

        private clsApplication(int appID, int applicantPersonID, DateTime appDate, int appTypeId, enApplicationStatus appStatus, DateTime lastStatusDate,
            float paidFees, int userID)
        {
            this.ApplicationID = appID;
            this.ApplicantPersonID = applicantPersonID;
            this.ApplicationDate = appDate;
            this.ApplicationTypeID = appTypeId;
            this.ApplicationStatus = appStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = userID;

            this.currentMode = enMode.UpdateApplication;

        }

        private bool _UpdateApplicationStatus()
        {
            // You Updated the LastStatusDate through the DAL instead of BLL...
            return DataAccessLayer.clsApplication.UpdateApplicationStatus(this.ApplicationID,(int)this.ApplicationStatus);
        }
        private int _AddNewApplication()
        {
            ApplicationID = DataAccessLayer.clsApplication.AddNewApplication(ApplicantPersonID,ApplicationDate,ApplicationTypeID,
                (int)ApplicationStatus,LastStatusDate,PaidFees,CreatedByUserID);

            return ApplicationID;
        }

        public bool Save()
        {

            switch (currentMode)
            {
                case (enMode.AddNewApplication):
                    int appID = _AddNewApplication();
                    if (appID != -1)
                    {
                        currentMode = enMode.UpdateApplication;
                        this.ApplicationID = appID;
                        return true;
                    }
                     else
                     return false;
                case (enMode.UpdateApplication):
                    return _UpdateApplicationStatus();
            }
            return false;
        }

        public static int IsApplicantHaveInCompleteApplcation(int ApplicantID,int LicenseClassID)
        {
            // Also both function names are bad ^ \/
            return DataAccessLayer.clsApplication.IsApplicantHavePreviousApplicationFromSameType(ApplicantID, LicenseClassID);
        }

        public static clsApplication FindByApplicationID(int ApplicationID)
        {
            int applicantPersonID = 0, appTypeId = 0, userID = 0;
            DateTime appDate = DateTime.Now, lastStatusDate = DateTime.Now;
            enApplicationStatus appStatus = enApplicationStatus.New;
            float paidFees = 0;

            int ApplicationStatus = (int)appStatus;

            if (DataAccessLayer.clsApplication.GetApplicationInfoByApplicationID(ApplicationID, ref applicantPersonID, ref appDate, ref appTypeId,
                ref ApplicationStatus, ref lastStatusDate, ref paidFees, ref userID))
            {
                appStatus = (enApplicationStatus)ApplicationStatus;
                return new clsApplication(ApplicationID, applicantPersonID, appDate, appTypeId, appStatus, lastStatusDate, paidFees, userID);
            }
            else
                return null;
        }

        public static clsApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int applicationID = -1, applicantPersonID = 0, appTypeId = 0, userID = 0;
            DateTime appDate = DateTime.Now, lastStatusDate = DateTime.Now;
            enApplicationStatus appStatus = enApplicationStatus.New;
            float paidFees = 0;

            int ApplicationStatus = (int)appStatus;

            if (DataAccessLayer.clsApplication.GetApplicationInfoByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID,ref applicationID, ref applicantPersonID, ref appDate, ref appTypeId,
                ref ApplicationStatus, ref lastStatusDate, ref paidFees, ref userID))
            {
                return new clsApplication(applicationID, applicantPersonID, appDate, appTypeId, appStatus, lastStatusDate, paidFees, userID);
            }
            else
                return null;
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            return DataAccessLayer.clsApplication.DeleteApplication(ApplicationID);
        }

    }
}
