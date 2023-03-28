using NewLab1.Pages.DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using NewLab1.Pages.DB;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection;
using MongoDB.Driver.Core.Configuration;

namespace NewLab1.Pages.OfficeHourSch
{
    public class ChooseFacultyModel : PageModel
    {

        [BindProperty]
        [Required]
        public string SelectMessage { get; set; }

        [BindProperty]
        [Required]
        public string SelectedTeacher { get; set; }

        public List<OfficeHours> OfficeHours { get; set; }

        public List<Faculty> FacultyList { get; set; }


        //Declaring OfficeHour and Faculty 
        public ChooseFacultyModel()
        {

            OfficeHours = new List<OfficeHours>();
            FacultyList = new List<Faculty>();
        }




        public IActionResult OnPost(string StudentAccount, int studentid)
        {
            if (studentid != 0)
            {
                return RedirectToPage("StudentAccount", new { studentid = studentid });
            }
            return Page();
        }

        //OnGet to retrieve faculty first and last names for dropdown menu
        public void OnGet()
        {
            SqlDataReader facultyReader = DBClass.FacultyReader();

            while (facultyReader.Read())
            {
                FacultyList.Add(new Faculty
                {
                    FirstName = facultyReader["fName"].ToString(),
                    LastName = facultyReader["lName"].ToString(),
                    FacultyID = Int32.Parse(facultyReader["facultyID"].ToString())
                });
            }

            // Close the reader and connection after you have finished using them
            facultyReader.Close();
            DBClass.Lab3DBConnection.Close();
        }


        public IActionResult OnPostSingleSelect(int facultyID)
        {
            return RedirectToPage("OfficeHourPage", new { facultyID = SelectedTeacher });
        }

    }
}
