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
    public class clsDetainedLicenseData
    {
        public static bool IsDetainedLicense(int LicenseID)
        {
            bool isDetained = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found =1 FROM DetainedLicenses WHERE LicenseID = @LicenseID and IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isDetained = reader.HasRows;

            }
            catch
            { }
            finally
            {
                connection.Close();
            }

            return isDetained;
        }

        public static int DetainLicense(int LicenseID, float FineFees, int CreatedByUserID)
        {
            int DetainedLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses (LicenseID, DetainDate, FineFees, CreatedByUserID,
                             IsReleased,ReleaseDate, ReleasedByUserID, ReleaseApplicationID )
                             VALUES           (@LicenseID, CURRENT_TIMESTAMP, @FineFees, @CreatedByUserID,
                             0, NULL ,NULL,NULL)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) // Converting From Scalar to int
                {
                    DetainedLicenseID = insertedID;
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

            return DetainedLicenseID;
        }
        public static bool GetDetainLicenseInfoByLicenseID(ref int DetainID,  int LicenseID, ref DateTime DetainDate, ref float FineFees,
   ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" SELECT TOP 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID
                              ORDER by DetainID desc;";

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

                    DetainID = (int)reader["DetainID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = float.Parse(reader["FineFees"].ToString());
                    IsReleased = (bool)reader["IsReleased"];

                    ReleaseDate = (reader["ReleaseDate"] != DBNull.Value) ? (DateTime)reader["ReleaseDate"] : DateTime.Now;
                    ReleasedByUserID = (reader["ReleasedByUserID"] != DBNull.Value) ? (int)reader["ReleasedByUserID"] : -1;
                    ReleaseApplicationID = (reader["ReleaseApplicationID"] != DBNull.Value) ? (int)reader["ReleaseApplicationID"] : -1;

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

        public static bool ReleaseDetainLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses
                             SET IsReleased = 1, 
                                 ReleaseDate = CURRENT_TIMESTAMP, 
                                 ReleasedByUserID = @ReleasedByUserID, 
                                 ReleaseApplicationID = @ReleaseApplicationID

                           WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);


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

        public static DataTable GetDetainLicensesData()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select DetainedLicenses.DetainID,DetainedLicenses.LicenseID,
                             DetainedLicenses.DetainDate,DetainedLicenses.IsReleased,
                             DetainedLicenses.FineFees,DetainedLicenses.ReleaseDate,
                             People.NationalNo, 
                             People.FirstName + ' ' + People.SecondName + ' ' + IsNull(People.ThirdName, ' ') + ' ' + People.LastName as FullName,
                             DetainedLicenses.ReleaseApplicationID
                             From DetainedLicenses
                             JOIN Licenses
                             ON DetainedLicenses.LicenseID = Licenses.LicenseID
                             JOIN Drivers
                             ON Drivers.DriverID = Licenses.DriverID
                             JOIN People
                             ON People.PersonID = Drivers.PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

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

            catch (Exception ex)
            {
                //
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
    }
}
