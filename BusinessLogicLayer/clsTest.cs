using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class clsTest
    {
        public int TestID { set; get; }
        public int TestAppointmentID { set; get; } // He did Composition for clsTestAppointment
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }

        public clsTest()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = "";
            CreatedByUserID = -1;
        }

        private clsTest(int testID, int testAppointmentID, bool testResult, string notes, int createdByUserID)
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserID;
        }

        public static int GetNumberOfPassedTests(int LocalDrivingLicenseAppID)
        {
            return DataAccessLayer.clsTest.GetNumberOfPassedTests(LocalDrivingLicenseAppID);
        }

        public static int NumberOfTrails(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            return DataAccessLayer.clsTest.NumberOfTrials(LocalDrivingLicenseApplicationID,TestTypeID);
        }
        
        public static int Save(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            // I won't make _add & _update inside the save method, Because the Exam UnModified.
            return DataAccessLayer.clsTest.TakeTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);
            // THIS IS WRONG SOL.
            // Save(method) CAN'T BE STATIC!!!!!!!!!!!!

            // Since that I don't need to get the test data
            // so i I didn't create Find method in BLL | GetInfo in DAL
            
            // So, I know that that's not the best solution
            /// But it's OK
        }
    }
}
