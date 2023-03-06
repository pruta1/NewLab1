using NewLab1.Pages.DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using NewLab1.Pages.DB;

namespace NewLab1.Pages.OfficeHourSch
{
    public class OfficeHourPageModel : PageModel
    {



        public List<OfficeHours> Hours { get; set; }

        public List<Faculty> FacultyList { get; set; }

        public List<Queue> QueueList { get; set; }  



        public OfficeHourPageModel()
        {

            Hours = new List<OfficeHours>();
            FacultyList = new List<Faculty>();
            QueueList = new List<Queue>();

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
        public IActionResult OnPostStudentQueue()
        {
            return RedirectToPage("/OfficeHourSch/StudentQueue");
        }

    }
}

