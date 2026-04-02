using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsDetainedLicense
    {
        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }
        public float FineFees { set; get; }
        public int CreatedByUserID { set; get; } // Composition for the User, but without removing this member,
                                                 // bec. it will benifit us in the private constructor in searching for
                                                 // the clsUser.Find(CreatedByUserID)
        public bool IsReleased { set; get; }
        public DateTime ReleaseDate { set; get; }
        public int ReleasedByUserID { set; get; } // Composition for the User
        public int ReleaseApplicationID { set; get; }

        public clsDetainedLicense()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID = -1;
            IsReleased = false;
            ReleaseDate = DateTime.Now;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;
        }

        private clsDetainedLicense(int detainID, int licenseID, DateTime detainDate, float fineFees, int createdByUserID, 
            bool isReleased, DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
        }

        public static bool IsDetainedLicense(int LicenseID)
        {
            return DataAccessLayer.clsDetainedLicenseData.IsDetainedLicense(LicenseID);
        }

        public static int DetainLicense(int LicenseID, float FineFees, int CreatedByUserID)
        {
            return DataAccessLayer.clsDetainedLicenseData.DetainLicense(LicenseID,FineFees,CreatedByUserID);
        }

        public static clsDetainedLicense Find(int LicenseID)
        {
            int detainID = -1, createdByUserID = -1, releasedByUserID = -1, releaseApplicationID = -1;
            DateTime detainDate = DateTime.Now, releaseDate = DateTime.Now;
            float fineFees = 0;
            bool isRelased = false;

            bool IsFound = DataAccessLayer.clsDetainedLicenseData.GetDetainLicenseInfoByLicenseID( ref detainID,LicenseID, ref detainDate,ref fineFees,
                ref createdByUserID, ref isRelased, ref releaseDate, ref releasedByUserID, ref releaseApplicationID);

            if (IsFound)
            {
                return new clsDetainedLicense(detainID, LicenseID, detainDate, fineFees, createdByUserID, isRelased, releaseDate, releasedByUserID, releaseApplicationID);
            }
            else
                return null;
        }
        public static bool ReleaseDetainLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID,int LicenseID)
        {
            return (DataAccessLayer.clsDetainedLicenseData.ReleaseDetainLicense(DetainID, ReleasedByUserID, ReleaseApplicationID) && clsLicense.ActivateLicense(LicenseID));
        }

        public static DataTable GetDetainedLicesesData()
        {
            return DataAccessLayer.clsDetainedLicenseData.GetDetainLicensesData();
        }

    }
}
