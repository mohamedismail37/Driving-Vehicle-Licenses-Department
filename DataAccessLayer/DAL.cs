using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;

namespace DataAccessLayer
{
    public class DAL
    {
        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * From PeopleInfo";

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

            catch(Exception ex)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable FillInfoUpdatePerson(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * from People where PersonID  = @PersonID";

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
            catch(Exception ex)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsFound(string NationalNumber)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select Found = 1 from People where NationalNo = @NationalNumber";

            SqlCommand command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@NationalNumber",NationalNumber);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

            }
            catch(Exception ex)
            {
                // To logs later
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select CountryName From Countries;";

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

        private static string _SaveImage(string ImagePath)
        {
            string PeopleImagesFileName = "C:\\Users\\moham\\Desktop\\C19)Full Real Project\\DVLD\\DVLD-People-Images";

            string newDestinationFile = Path.Combine(PeopleImagesFileName, Guid.NewGuid().ToString() + ".JPEG");
            File.Copy(ImagePath, newDestinationFile);
            return newDestinationFile;
        }

        public static int SavePerson(string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo,
                DateTime DateOfBirth, bool Gender, string Phone, string Email, int CountryID, string Address, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People (FirstName, SecondName, ThirdName, LastName, NationalNo, DateOfBirth, Gendor,
Phone, Email,NationalityCountryID, Address, ImagePath)
VALUES (@FirstName, @SecondName, @ThirdName,
@LastName, @NationalNo, @DateOfBirth, @Gender, @Phone, @Email,@CountryID, 
@Address, @ImagePath) SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@Address", Address);

            if (ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", _SaveImage(ImagePath));
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            if (Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar(); // To execute any queries and return only one value (New ID)

                if (result != null && int.TryParse(result.ToString(), out int insertedID)) // Converting From Scalar to int
                {
                    PersonID = insertedID;
                }
            }
            catch(Exception ex)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        private static bool _IsTherePhotoInDB(int PersonID)
        {
            bool IsTherePhoto = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select Found = 1 from People Where PersonID = @PersonID and ImagePath is not NULL;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                IsTherePhoto = reader.HasRows;
            }
            catch
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return IsTherePhoto;
        }

        private static string _OldImagePath(int PersonID)
        {
            string OldImagePath = "";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select ImagePath from People Where PersonID = @PersonID ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    OldImagePath = result.ToString();
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
            return OldImagePath;

        }

        public static bool UpdatePerson(int PersonID,string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo,
                DateTime DateOfBirth, bool Gender, string Phone, string Email, int CountryID, string Address, string ImagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE People SET FirstName = @FirstName, SecondName = @SecondName, ThirdName = @ThirdName, LastName = @LastName,
NationalNo = @NationalNo, DateOfBirth = @DateOfBirth, Gendor = @Gender, Phone = @Phone, Email = @Email, NationalityCountryID = @CountryID, Address = @Address, ImagePath = @ImagePath
WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@Address", Address);

            if (!_IsTherePhotoInDB(PersonID))
            {
                if (ImagePath != "")
                    command.Parameters.AddWithValue("@ImagePath", _SaveImage(ImagePath));
                else
                    command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            }
            else
            {
                string OldImagePath = _OldImagePath(PersonID);
                if (OldImagePath != ImagePath)
                {
                    File.Delete(OldImagePath);
                    command.Parameters.AddWithValue("@ImagePath", _SaveImage(ImagePath));
                }
                else
                {
                    command.Parameters.AddWithValue("@ImagePath", ImagePath);
                }
            }

            if (Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            if (ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

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

        private static bool _IsUser(int PersonID)
        {
            bool IsUser = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select Found = 1 from Users where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                IsUser = reader.HasRows;

            }
            catch (Exception ex)
            {
                // To logs later
            }
            finally
            {
                connection.Close();
            }

            return IsUser;
        }

        private static bool _IsApplicant(int PersonID)
        {
            bool IsApplicant = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select Found = 1 from Applications where ApplicantPersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                IsApplicant = reader.HasRows;

            }
            catch (Exception ex)
            {
                // To logs later
            }
            finally
            {
                connection.Close();
            }

            return IsApplicant;
        }

        public static bool DeleteContact(int PersonID)
        {
            if (_IsUser(PersonID) || _IsApplicant(PersonID))
                return false;

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE People Where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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
