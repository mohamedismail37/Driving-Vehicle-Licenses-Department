using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;
using System.Runtime.CompilerServices;
using System.CodeDom;

namespace BusinessLogicLayer
{
    public class clsTestAppointment
    {
        public int TestAppointmentID { set; get; }
        public int TestTypeID { set; get; }
        public int LocalDrivingLicenseApplicationID { set; get; }
        public DateTime AppointmentDate { set; get; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }
        public int RetakeTestApplicationID { set; get; }
        public enum enMode { AddNew = 0, Update =1 };
        public enMode mode;
        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;

            mode = enMode.AddNew;
        }

        private clsTestAppointment(int testAppointmentID, int testTypeID, int localDrivingLicenseAppID, DateTime appointmentDate, float paidFees, 
            int createdByUserID, bool isLocked, int retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseAppID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;

            mode = enMode.Update;
        }

        public bool Save()
        {
            switch (mode)
            {
                case enMode.AddNew:
                    if (_addNewAppointment())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateAppointment();
            }
            return false;

        }
        public static DataTable GetTestAppointments(int LocalDrivingLicenseApplicationID, int LicenseClassID, int TestTypeID)
        {
            return DataAccessLayer.clsTestAppointment.GetTestAppointments(LocalDrivingLicenseApplicationID, LicenseClassID, TestTypeID);
        }

        public static bool IsApplicationHasUnlockedAppointment(int LocalDrivingLicenseApplicationID, int LicenseClassID, int TestTypeID)
        {
            return DataAccessLayer.clsTestAppointment.IsApplicationHasUnlockedAppointment(LocalDrivingLicenseApplicationID, LicenseClassID, TestTypeID);
        }

        public static bool IsApplicationPassedTheTest(int LocalDrivingLicenseApplicationID, int LicenseClassID, int TestTypeID)
        {
            return DataAccessLayer.clsTestAppointment.IsApplicationPassedTheTest(LocalDrivingLicenseApplicationID, LicenseClassID, TestTypeID);
        }

        private bool _addNewAppointment()
        {
            return DataAccessLayer.clsTestAppointment.AddNewTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID,
                this.IsLocked, this.RetakeTestApplicationID);
        }
        private bool _UpdateAppointment()
        {
            return DataAccessLayer.clsTestAppointment.UpdateTestAppointment(this.TestAppointmentID, this.AppointmentDate);
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {

            int testTypeID = -1, localDrivingLicenseAppID = -1, createdByUserID = -1, retakeTestAppID = -1;
            DateTime appointmentDate = DateTime.Now;
            float paidFees = 0;
            bool isLocked = false;

            bool isFound = DataAccessLayer.clsTestAppointment.GetTestAppointmentInfoByID(TestAppointmentID,ref testTypeID, ref localDrivingLicenseAppID,
               ref appointmentDate,ref paidFees,ref createdByUserID,ref isLocked,ref retakeTestAppID);

            if (isFound)
            {
                return new clsTestAppointment(TestAppointmentID, testTypeID, localDrivingLicenseAppID, appointmentDate, paidFees,
                    createdByUserID, isLocked,retakeTestAppID);
            }
            else
                return null;
        }

        public static bool LockTestAppointment(int TestAppointment)
        {
            return DataAccessLayer.clsTestAppointment.LockTestAppointment(TestAppointment);
        }
    }
}
