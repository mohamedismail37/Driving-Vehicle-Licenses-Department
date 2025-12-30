using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Data;
using System.Linq;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class clsTestType
    {

        // Instead of making TestTypeID as integer
        // You can make it as an Enum (As an other Sol.)
        /// public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3};
   
        public int TestTypeID { set; get; }
        public string TestTypeTitle { set; get; }
        public string TestTypeDescription { set; get; }
        public float TestTypeFees { set; get; }

        public clsTestType()
        {
            TestTypeID = -1;
            TestTypeDescription = "";
            TestTypeTitle = "";
            TestTypeFees = 0;
        }

        private clsTestType(int testTypeID, string testTypeTitle, string testTypeDescription, float testTypeFees)
        {
            TestTypeID = testTypeID;
            TestTypeTitle = testTypeTitle;
            TestTypeDescription = testTypeDescription;
            TestTypeFees = testTypeFees;
        }

        public static DataTable GetAllTestTypes()
        {
            return DataAccessLayer.clsTestType.GetAllTestTypes();
        }

        public static clsTestType GetTestTypeData(int TestTypeID)
        {
            string testTypeTitle = "", testTypeDescription = "";
            float testTypeFees = 0;

            bool isFound = DataAccessLayer.clsTestType.GetTestTypeInfo(TestTypeID,ref testTypeTitle,ref testTypeDescription,ref testTypeFees);

            if (isFound)
            {
                return new clsTestType(TestTypeID, testTypeTitle, testTypeDescription, testTypeFees);
            }
            else
                return null;

        }
        
        public bool Save()
        {
            return DataAccessLayer.clsTestType.EditTestTypeInfo(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
        }
    }
}
