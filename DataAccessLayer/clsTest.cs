using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsTest
    {
        public static int GetNumberOfPassedTests(int LocalDrivingLicenseAppID)
        {
            int numberOfPassedTest = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"(SELECT COUNT(*) AS PassedTestsCount 
                             FROM Tests 
                             JOIN TestAppointments 
                             ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseAppID AND Tests.TestResult = 1)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseAppID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar(); // To execute any queries and return only one value (New ID)

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) // Converting From Scalar to int
                {
                    numberOfPassedTest = insertedID;
                }

            }
            catch 
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return numberOfPassedTest;
        }

        public static int NumberOfTrials(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            int numberOfTrials = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select COUNT(*) AS Trails
                             From Tests
                             JOIN TestAppointments
                             ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND
                             TestAppointments.TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar(); // To execute any queries and return only one value (New ID)

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) // Converting From Scalar to int
                {
                    numberOfTrials = insertedID;
                }

            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return numberOfTrials;
        }

        public static int TakeTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                             VALUES                   (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            if (!string.IsNullOrWhiteSpace(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) // Converting From Scalar to int
                {
                    TestID = insertedID;
                }
            }
            catch
            {
                //
            }
            finally
            {
                connection.Close();
            }


            return TestID;
        }

    }
}
