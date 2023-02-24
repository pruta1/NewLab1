using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewLab1.Pages.DataClasses;
using NewLab1.Pages.DB;
using System.Data.SqlClient;

namespace NewLab1.Pages.OfficeHourSch
{
    public class OfficeHourPageTeacherModel : PageModel
    {

        public List<OfficeHours> Hours { get; set; }

        public List<Faculty> FacultyList { get; set; }




        public OfficeHourPageTeacherModel()
        {
            Hours = new List<OfficeHours>();
            FacultyList = new List<Faculty>();

        }

        //OnGet used to retrieve information to place in table of office hour page  
        public void OnGet(int facultyID)
        {
            SqlDataReader SingleReader = DBClass.SingleReader();
            while (SingleReader.Read())
            {
                int currentFacultyID = int.Parse(SingleReader["facultyID"].ToString());
                if (currentFacultyID == facultyID)
                {
                    Hours.Add(new OfficeHours
                    {
                        OfficeHourID = int.Parse(SingleReader["officeHoursID"].ToString()),
                        OfficeNumber = int.Parse(SingleReader["office#"].ToString()),
                        Time = SingleReader["timeID"].ToString(),
                        Date = SingleReader["dateID"].ToString(),
                        FacultyID = currentFacultyID,
                        StudentName = SingleReader["studentName"].ToString(),
                    });
                }
            }
            DBClass.Lab3DBConnection.Close();
        }

    }
}

