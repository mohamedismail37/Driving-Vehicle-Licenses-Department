using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsLicenseClass
    {
        public int LicenseClassID { set; get; }
        public string ClassName { set; get; }
        public string ClassDescription { set; get; }
        public int MinimumAllowedAge { set; get; }
        public int DefaultValidityLength { set; get; }
        public float ClassFees { set; get; }

        public clsLicenseClass()
        {
            LicenseClassID = -1;
            ClassName = "";
            ClassDescription = "";
            MinimumAllowedAge = 0;
            DefaultValidityLength = 0;
            ClassFees = 0;
        }

        private clsLicenseClass(int licenseClassID, string className, string classDescription, int minimumAllowedAge, int defualtValidityLength, float classFees)
        {
            LicenseClassID = licenseClassID;
            ClassName = className;
            ClassDescription = classDescription;
            MinimumAllowedAge = minimumAllowedAge;
            DefaultValidityLength = defualtValidityLength;
            ClassFees = classFees;
        }

        static public string[] ClassNames()
        {
            return DataAccessLayer.clsLicenseClasses.LicenseClassNames();
        }

        public static string GetClassNameByClassID(int ClassID)
        {
            return DataAccessLayer.clsLicenseClasses.LicenseClassName(ClassID);
        }

        public static clsLicenseClass Find(int LicenseClassID)
        {
            string className = "", classDescription = "";
            int minimumAllowedAge = 0, defaultValidityLength = 0;
            float classFees = 0;

            bool IsFound = DataAccessLayer.clsLicenseClasses.GetLicenseClassByID(LicenseClassID, ref className,ref classDescription,
                ref minimumAllowedAge,ref defaultValidityLength,ref classFees );

            if (IsFound)
            {
                return new clsLicenseClass(LicenseClassID, className, classDescription,minimumAllowedAge, defaultValidityLength, classFees);
            }
            else
                return null;
        }

    }
}
