using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Security.Policy;

namespace DataAccessLayer
{
    public class clsLicenseClasses
    {
        static public string[] LicenseClassNames()
        {
            string[] arr = new string[7] ;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT ClassName FROM LicenseClasses";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {
                    // the index of reader[here] is points to the column not the row.
                    arr[i] = reader[0].ToString();
                    i++;
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

            return arr;
        }

        public static string LicenseClassName(int LicenseClassID)
        {
            string licenseClassName = "";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT ClassName FROM LicenseClasses Where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    licenseClassName = reader["ClassName"].ToString();
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

            return licenseClassName;
        }

        public static bool GetLicenseClassByID(int LicenseClassID,ref string ClassName,ref string ClassDescription,ref int MinimumAllowedAge,
            ref int DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = int.Parse(reader["MinimumAllowedAge"].ToString());
                    DefaultValidityLength = int.Parse(reader["DefaultValidityLength"].ToString());
                    ClassFees = float.Parse(reader["ClassFees"].ToString());
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

    }
}
