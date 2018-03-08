using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Collections.Generic;
using System.Security;
using System.Web.Security;
using CAESDO;

namespace EatFit.Data
{
    public class User
    {
        private string userId;

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private short age;

        public short Age
        {
            get { return age; }
            set { age = value; }
        }
        private long school_id;

        public long SchoolId
        {
            get { return school_id; }
            set { school_id = value; }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private int grade_level_id;

        public int GradeLevelId
        {
            get { return grade_level_id; }
            set { grade_level_id = value; }
        }
    }

    public class Users
    {
        private static CAESDO.DataOps dops = new CAESDO.DataOps();

        public static bool AddAgeToUser(string UserName, short age)
        {
            bool retval = true;

            dops.ResetDops();
            dops.Sproc = "user_details_AddAgeToUser";
            dops.SetParameter("@UserName", UserName, CAESDO.DataOps.DopsDirection.Input);
            dops.SetParameter("@age", age, CAESDO.DataOps.DopsDirection.Input);

            try
            {
                dops.Execute_Sql();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                retval = false;
            }

            return retval;
        }

        public static bool AddGenderToUser(string UserName, string gender)
        {
            bool retval = true;

            dops.ResetDops();
            dops.Sproc = "user_details_AddGenderToUser";
            dops.SetParameter("@UserName", UserName, CAESDO.DataOps.DopsDirection.Input);
            dops.SetParameter("@gender", gender, CAESDO.DataOps.DopsDirection.Input);

            try
            {
                dops.Execute_Sql();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                retval = false;
            }

            return retval;
        }

        public static User GetUser(string UserName)
        {
            User retval = null; ;

            dops.ResetDops();
            dops.Sproc = "user_details_GetUser";
            dops.SetParameter("@UserName", UserName, CAESDO.DataOps.DopsDirection.Input);

            ArrayList fields = new ArrayList();
            fields.Add("UserId");
            fields.Add("UserName");
            fields.Add("age");
            fields.Add("gender");
            fields.Add("school_id");
            fields.Add("grade_level_id");

            ArrayList results = dops.get_arrayList(fields);

            ArrayList result;

            if (results.Count == 1)
            {
                result = (ArrayList)results[0];

                User user = new User();
                user.UserId = result[0] != DBNull.Value ? ((Guid)result[0]).ToString(): String.Empty;
                user.UserName = result[1] as string ?? string.Empty;
                user.Age = (result[2] != DBNull.Value ? (short)result[2] : (short)0);
                user.Gender = result[3] as string ?? String.Empty;
                user.SchoolId = (result[4] != DBNull.Value ? (long)result[4] : 0L);
                user.GradeLevelId = (result[5] != DBNull.Value ? (int)result[5] : 0);
                retval = user;
            }

            return retval;
        }

        public static bool AddGradeLevelToUser(string UserName, int GradeLevelId)
        {
            bool retval = true;

            dops.ResetDops();
            dops.Sproc = "user_details_AddGradeLevelToUser";
            dops.SetParameter("@UserName", UserName, CAESDO.DataOps.DopsDirection.Input);
            dops.SetParameter("@grade_level_id", GradeLevelId, CAESDO.DataOps.DopsDirection.Input);

            try
            {
                dops.Execute_Sql();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retval = false;
                throw ex;

            }
            return retval;
        }

        public static bool AddSchoolToUser(string UserName, long SchoolId)
        {
            bool retval = true;
            Console.Out.WriteLine("UserName: [" + UserName + "]; SchoolId: [" + SchoolId + "]");
            dops.ResetDops();
            dops.Sproc = "user_details_AddSchoolToUser";
            dops.SetParameter("@UserName", UserName, CAESDO.DataOps.DopsDirection.Input);
            dops.SetParameter("@school_id", SchoolId, CAESDO.DataOps.DopsDirection.Input);

            try
            {
                dops.Execute_Sql();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retval = false;
                throw ex;
            }

            return retval;

        }
    }
}
