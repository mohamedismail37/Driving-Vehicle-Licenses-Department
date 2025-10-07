using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;
using System.IO;

namespace BusinessLogicLayer
{
    public class BLL
    {

        public static DataTable GetAllPeople()
        {
            return DAL.GetAllPeople();
        }

        public static bool IsFound(string NationalNumber)
        {
            return DAL.IsFound(NationalNumber);
        }

        public static DataTable GetAllCountries()
        {
            return DAL.GetAllCountries();
        }

        public static int SavePerson(string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo,
                DateTime DateOfBirth,bool Gender, string Phone, string Email,int CountryID, string Address, string ImagePath)
        {
            return DataAccessLayer.DAL.SavePerson(FirstName, SecondName, ThirdName, LastName, NationalNo,
                DateOfBirth, Gender, Phone, Email, CountryID, Address, ImagePath);
        }

        public static bool UpdatePerson(int PersonID,string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo,
                DateTime DateOfBirth, bool Gender, string Phone, string Email, int CountryID, string Address, string ImagePath)
        {
            return DataAccessLayer.DAL.UpdatePerson(PersonID,FirstName, SecondName, ThirdName, LastName, NationalNo,
    DateOfBirth, Gender, Phone, Email, CountryID, Address, ImagePath);
        }

        public static DataTable FillInfoUpdatePerson(int PersonID)
        {
            return DAL.FillInfoUpdatePerson(PersonID);
        }

        public static bool DeletePerson(int PersonID)
        {
            return DAL.DeleteContact(PersonID);
        }
    }
}
