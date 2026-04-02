using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BusinessLogicLayer
{
    public class clsLocalDrivingLicenseApplication // He inherited this class from : clsApplication /for me: composition will be better
    {
        /// Is making enMode in clsGlobalSettings better?
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew; 
        
        public int LocalDrivingLicenseApplicationID { set; get; }
        public int ApplicationID { set; get; }
        public int LicenseClassID { set; get; } // He made composition for LicensceClassInfo

        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            ApplicationID = -1;
            LicenseClassID = -1;
            Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int localDrivingLicenseAppID, int appID, int licenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseAppID;
            this.ApplicationID = appID;
            this.LicenseClassID = licenseClassID;
            Mode = enMode.Update;
        }
        private bool _AddNewLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID =  DataAccessLayer.clsLocalDrivingLicenseApplication.AddNewLocalDrivingLicenseApplication(ApplicationID, LicenseClassID);

            return (LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return DataAccessLayer.clsLocalDrivingLicenseApplication.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, LicenseClassID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();
            }
            return false;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return DataAccessLayer.clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicencseApplications();
        }

        public static clsLocalDrivingLicenseApplication FindLocalDrivingLicenseAppInfo(int LocalDrivingLicenseAppID)
        {
            int AppID = 0, LicenseClassID = 0;

            if (DataAccessLayer.clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseAppInfo(LocalDrivingLicenseAppID, ref AppID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseAppID, AppID, LicenseClassID);
            }
            else
                return null;
        }

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseAppID)
        {
            return DataAccessLayer.clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseAppID);
        }

    }
}
