using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;

namespace DataAccessLayer
{
    public class clsLicenseData
    {
        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate,
            DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Licenses (ApplicationID, DriverID, LicenseClass, IssueDate,
                                                       ExpirationDate, Notes, PaidFees,IsActive ,IssueReason ,CreatedByUserID )
                             VALUES                   (@ApplicationID, @DriverID, @LicenseClass, @IssueDate,
                                                       @ExpirationDate, @Notes, @PaidFees,@IsActive , @IssueReason , @CreatedByUserID)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (Notes != "" && Notes != null)
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) // Converting From Scalar to int
                {
                    LicenseID = insertedID;
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

            return LicenseID;
        }

        public static bool GetLicenseInfoByLicenseID(int LicenseID, ref int ApplicationID,ref int DriverID, ref int LicenseClass, ref DateTime IssueDate,
          ref  DateTime ExpirationDate,ref string Notes, ref float PaidFees,ref bool IsActive, ref int IssueReason,ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    PaidFees = float.Parse(reader["PaidFees"].ToString());
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = int.Parse(reader["IssueReason"].ToString());

                    // Notes: allows null in DB so we should handle null
                    Notes = (reader["Notes"] != DBNull.Value) ? (string)reader["Notes"] : "";

                    CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static bool GetLicenseInfoByApplicationID(ref int LicenseID,int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate,
          ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    PaidFees = float.Parse(reader["PaidFees"].ToString());
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = int.Parse(reader["IssueReason"].ToString());

                    // Notes: allows null in DB so we should handle null
                    Notes = (reader["Notes"] != DBNull.Value) ? (string)reader["Notes"] : "";

                    CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static bool IsDriverHaveLicenseFromSpecificLicenseClass(int DriverID, int LicenseClass)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found =1 FROM Licenses WHERE DriverID = @DriverID AND LicenseClass = @LicenseClass";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("LicenseClass", LicenseClass);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

            }
            catch
            {   }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static DataTable GetLocalLicensesHistoryByPersonID(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select LicenseID, Licenses.ApplicationID, ClassName, IssueDate, ExpirationDate, IsActive
                             From Licenses
                             JOIN Drivers
                             ON Drivers.DriverID = Licenses.DriverID
                             JOIN LicenseClasses
                             ON LicenseClasses.LicenseClassID = Licenses.LicenseClass
                             Where Drivers.PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool DecativateLicense(int LicenseID)
         {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Licenses
                             SET IsActive = 0
                             WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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
        public static bool ActivateLicense(int LicenseID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Licenses
                             SET IsActive = 1
                             WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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
        // you can combine Decativate & Activate (License) in one method by adding one boolean parameter with the intstruction
    }
}
