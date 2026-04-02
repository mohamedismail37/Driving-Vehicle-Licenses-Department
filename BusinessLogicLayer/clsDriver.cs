using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BusinessLogicLayer
{
    public class clsDriver
    {
        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { set; get; }

        public clsDriver()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;
        }

        private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;
        }

        private int _addNewDriver()
        {
            return DataAccessLayer.clsDriverData.AddNewDriver(this.PersonID,this.CreatedByUserID, this.CreatedDate);
        }
        public bool Save()
        {
            int driverID = _addNewDriver();
            if (driverID != -1)
            {
                DriverID = driverID;
                return true;
            }
            else
                return false;
        }

        public static bool IsDriverExistByPersonID(int PersonID)
        {
            return DataAccessLayer.clsDriverData.IsDriverExist(PersonID);
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int driverID = -1, createdByUserID = -1;
            DateTime createdDate = DateTime.Now;

            bool IsFound = clsDriverData.GetDriverDataByPersonID(PersonID, ref driverID, ref createdDate, ref createdByUserID);

            if (IsFound)
            {
                return new clsDriver(driverID, PersonID,createdByUserID,createdDate);
            }
            else
                return null;
        }

        public static clsDriver FindByDriverID(int DriverID)
        {
            int personID = -1, createdByUserID = -1;
            DateTime createdDate = DateTime.Now;

            bool IsFound = clsDriverData.GetDriverDataByDriverID(ref personID, DriverID, ref createdDate, ref createdByUserID);

            if (IsFound)
            {
                return new clsDriver(DriverID, personID, createdByUserID, createdDate);
            }
            else
                return null;
        }

        public static DataTable GetDriversData()
        {
            return DataAccessLayer.clsDriverData.GetDriversInfo();
        }

        // Instead of making in clsLicense a static method : dt GetLocalLicensesHistoryByLDLAppID()
        // He did here with non-static -> GoodIdea
        // and the same for the international licenses.

    }
}
