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
    public class Grade
    {
        private int grade_level_id;

        public int GradeLevelId
        {
            get { return grade_level_id; }
            set { grade_level_id = value; }
        }
        private string grade_level;

        public string GradeLevel
        {
            get { return grade_level; }
            set { grade_level = value; }
        }
    }

    public class Grades
    {
        private static CAESDO.DataOps dops = new CAESDO.DataOps();

        public static List<Grade> GetAllGrades()
        {
            List<Grade> gradeList = new List<Grade>();
            dops.ResetDops();
            dops.Sproc = "grade_level_GetAllGrades";

            ArrayList fields = new ArrayList();
            fields.Add("grade_level_id");
            fields.Add("grade_level");

            ArrayList results = dops.get_arrayList(fields);

            ArrayList result;

            int i = 0;
            while (i < results.Count)
            {
                result = (ArrayList)results[i];

                Grade grade = new Grade();
                grade.GradeLevelId = (int)result[0];
                grade.GradeLevel = (string)result[1];

                gradeList.Add(grade);

                i++;
            }
            return gradeList;
        }

        public static bool AddUserToGrade(string UserName, int GradeLevelId)
        {
            bool retval = true;

            dops.ResetDops();
            dops.Sproc = "grade_level_AddUserToGrade";
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
    }
}
