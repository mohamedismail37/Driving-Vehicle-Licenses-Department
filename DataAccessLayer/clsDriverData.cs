using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Specialized;

namespace DataAccessLayer
{
    public class clsDriverData
    {
        public static int AddNewDriver( int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int DriverID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate )
                             VALUES              (@PersonID, @CreatedByUserID, @CreatedDate)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) // Converting From Scalar to int
                {
                    DriverID = insertedID;
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

            return DriverID;
        }

        public static bool IsDriverExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found =1 FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

            }
            catch (Exception ex)
            {
                // To logs later
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetDriverDataByPersonID(int PersonID, ref int DriverID, ref DateTime CreatedDate, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
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
        public static bool GetDriverDataByDriverID(ref int PersonID, int DriverID, ref DateTime CreatedDate, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
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
        public static DataTable GetDriversInfo()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select Distinct Drivers.DriverID, Drivers.PersonID, People.NationalNo,
                             People.FirstName + ' ' + People.SecondName + ' ' + IsNull(People.ThirdName, ' ') + ' ' + People.LastName as FullName,
                             Drivers.CreatedDate, (Select COUNT (*) From Licenses Where Licenses.IsActive = 1 AND Licenses.DriverID = Drivers.DriverID) AS ActiveLicense
                             From Drivers
                             JOIN People
                             ON Drivers.PersonID = People.PersonID
                             JOIN Licenses
                             ON Drivers.DriverID = Licenses.DriverID;";

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
