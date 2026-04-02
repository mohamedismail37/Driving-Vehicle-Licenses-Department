using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;
using System.Security.Cryptography;

namespace DataAccessLayer
{
    public class clsTestAppointment
    {
        public static DataTable GetTestAppointments(int LocalDrivingLicenseApplicationID, int LicenseClassID, int TestTypeID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select TestAppointments.TestAppointmentID, TestAppointments.AppointmentDate,
                             TestTypes.TestTypeFees, TestAppointments.IsLocked
                             From TestAppointments
                             JOIN TestTypes
                             ON TestAppointments.TestTypeID = TestTypes.TestTypeID
                             JOIN LocalDrivingLicenseApplications
                             ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                             Where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND
                             LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID AND TestAppointments.TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsApplicationHasUnlockedAppointment(int LocalDrivingLicenseApplicationID, int LicenseClassID, int TestTypeID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select found = 1 From TestAppointments
                             JOIN TestTypes
                             ON TestAppointments.TestTypeID = TestTypes.TestTypeID
                             JOIN LocalDrivingLicenseApplications
                             ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                             Where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND
                             LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID AND TestAppointments.TestTypeID = @TestTypeID
                             AND TestAppointments.IsLocked = 0;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsApplicationPassedTheTest(int LocalDrivingLicenseApplicationID, int LicenseClassID, int TestTypeID)
        {
            bool isPassed = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select found = 1 From TestAppointments
                             JOIN TestTypes
                             ON TestAppointments.TestTypeID = TestTypes.TestTypeID
                             JOIN LocalDrivingLicenseApplications
                             ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                             JOIN Tests
                             ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             Where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND 
                             LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID AND TestAppointments.TestTypeID= @TestTypeID
                             AND Tests.TestResult= 1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isPassed = reader.HasRows;

                reader.Close();
            }
            catch
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return isPassed;
        }
        public static bool AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseAppID, DateTime AppointmentDate,
            float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int appointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees,
                                                       CreatedByUserID, IsLocked, RetakeTestApplicationID)
                             VALUES                   (@TestTypeID, @LocalDrivingLicenseAppID, @AppointmentDate, @PaidFees,
                                                       @CreatedByUserID, @IsLocked, @RetakeTestApplicationID)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID != -1)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) // Converting From Scalar to int
                {
                    appointmentID = insertedID;
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

            return (appointmentID != -1);
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, DateTime AppointmentDate)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE TestAppointments
                             SET    AppointmentDate = @AppointmentDate
                             WHERE  TestAppointmentID = @TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool GetTestAppointmentInfoByID(int TestAppointmentID,ref int TestTypeID,ref int LocalDrivingLicenseAppID,ref DateTime AppointmentDate,
           ref float PaidFees,ref int CreatedByUserID, ref bool IsLocked,ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseAppID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = float.Parse(reader["PaidFees"].ToString());
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    // Email: allows null in DB so we should handle null
                    RetakeTestApplicationID = (reader["RetakeTestApplicationID"] != DBNull.Value) ? (int)reader["RetakeTestApplicationID"] : -1;
                }
                else
                {
                    // The record was NOT found
                    isFound = false;
                }

                reader.Close();
            }
            catch
            {
                //
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool LockTestAppointment(int TestAppointmentID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE TestAppointments
                             SET    IsLocked = 1
                             WHERE  TestAppointmentID = @TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

    }
}
