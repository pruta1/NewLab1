using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewLab1.Pages.DataClasses;
using NewLab1.Pages.DB;
using System.Data.SqlClient;

namespace NewLab1.Pages.OfficeHourSch
{
    public class HashedLoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string PersonType { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string username, string password, string personType)
        {
            if (DBClass.HashedParameterLogin(username, password, personType))
            {
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetString("personType", personType);

                SqlDataReader tempReader = null;

                if (personType == "Faculty")
                {
                    tempReader = DBClass.SelectFaculty(username);
                }
                else if (personType == "Student")
                {
                    tempReader = DBClass.SelectStudent(username);
                }

                while (tempReader.Read())
                {             

                    // Check the user's ID and redirect accordingly
                    if (personType == "Faculty")
                    {
                        int facultyID = (int)tempReader["FacultyID"];
                        DBClass.Lab3DBConnection.Close();
                        return RedirectToPage("/OfficeHourSch/OfficeHourPageTeacher", new { facultyid = facultyID });
                    }
                    else if (personType == "Student")
                    {
                        int studentID = (int)tempReader["StudentID"];
                        DBClass.Lab3DBConnection.Close();
                        return RedirectToPage("/OfficeHourSch/ChooseFaculty", new { studentid = studentID});
                    }
                }

                tempReader.Close();
                DBClass.Lab3DBConnection.Close();
            }
            else
            {
                ViewData["LoginMessage"] = "Username, password, or selected type is incorrect.";
                DBClass.Lab3DBConnection.Close();
                return Page();
            }

            // Default redirect
            return RedirectToPage("/Index");
        }



        //Used to populate valid information in the login boxes
    }

}

