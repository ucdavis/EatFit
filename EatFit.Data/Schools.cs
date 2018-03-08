using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Collections.Generic;
using System.Security;
using System.Web.Security;

namespace EatFit.Data
{
    public class Schools
    {
        private static CAESDO.DataOps dops = new CAESDO.DataOps();

        public static List<School> GetAllSchools()
        {
            List<School> schoolList = new List<School>();
            dops.ResetDops();
            dops.Sproc = "school_GetAllSchools";

            ArrayList fields = new ArrayList();
            fields.Add("school_id");
            fields.Add("name");
            fields.Add("city");
            fields.Add("state");

            ArrayList results = dops.get_arrayList(fields);
           
                ArrayList result;

                int i = 0;
                while (i < results.Count)
                {
                    result = (ArrayList)results[i];

                    School school = new School();
                    school.SchoolId = (long)result[0];  // This will ALWAYS have a value!
                    school.Name = result[1] as string ?? "no name";
                    school.City = result[2] as string ?? string.Empty; 
                    school.State = result[3] as string ?? string.Empty;
                    schoolList.Add(school);

                    i++;
                }
            
            return schoolList;
        }

        public static bool CreateSchool(string name, string city, string state)
        {
            bool retval = true;
            dops.ResetDops();
            dops.Sproc = "school_CreateSchool";
            dops.SetParameter("@name", name, CAESDO.DataOps.DopsDirection.Input);
            dops.SetParameter("@city", city, CAESDO.DataOps.DopsDirection.Input);
            dops.SetParameter("@state", state, CAESDO.DataOps.DopsDirection.Input);
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

        public static bool SchoolExists(string name, string city, string state)
        {
            dops.ResetDops();
            dops.Sproc = "school_SchoolExists";
            dops.SetParameter("@name", name, CAESDO.DataOps.DopsDirection.Input);
            dops.SetParameter("@city", city, CAESDO.DataOps.DopsDirection.Input);
            dops.SetParameter("@state", state, CAESDO.DataOps.DopsDirection.Input);
            dops.SetParameter("RETURN_VALUE", -1, CAESDO.DataOps.DopsDirection.Return);
            dops.Execute_Sql();
            return ((int)(dops.GetOutputVariable("RETURN_VALUE"))) == 1 ? true : false;
        }

        public static bool DeleteSchool(long school)
        {
            bool retval = true;
            dops.ResetDops();
            dops.Sproc = "school_DeleteSchool";
            dops.SetParameter("@school_id", school, CAESDO.DataOps.DopsDirection.Input);
            
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

        public static bool AddUserToSchool(string UserName, long SchoolId)
        {
            bool retval = true;
            
            dops.ResetDops();
            dops.Sproc = "school_AddUserToSchool";
            dops.SetParameter("@UserName", UserName, CAESDO.DataOps.DopsDirection.Input);
            dops.SetParameter("@school_id", SchoolId, CAESDO.DataOps.DopsDirection.Input);

            try
            {
                dops.Execute_Sql();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                retval = false;
                //throw ex;
            }
           
            return retval;
          
        }

        public static List<User> GetUsersInSchool(long school)
        {
            List<User> userList = new List<User>();

            dops.ResetDops();
            dops.Sproc = "school_GetUsersInSchool";
            dops.SetParameter("@SchoolId", school, CAESDO.DataOps.DopsDirection.Input);

            ArrayList fields = new ArrayList();
            fields.Add("UserId");
            fields.Add("UserName");

            ArrayList results = dops.get_arrayList(fields);

            ArrayList result;

            int i = 0;
            while (i < results.Count)
            {
                result = (ArrayList)results[i];

                User user = new User();
                user.UserId = result[0].ToString();  // this will ALWAYS be set.
                user.UserName = result[1] as string ?? "no username";
                userList.Add(user);

                i++;
            }
            return userList;
        }
    }

}
