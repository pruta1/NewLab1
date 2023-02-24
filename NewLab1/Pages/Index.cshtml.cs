using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewLab1.Pages.DB;
using System.ComponentModel.DataAnnotations;

namespace NewLab1.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; }
        
        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string PersonType { get; set; }

        [BindProperty]
        public string Phone { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
           if (PersonType=="Faculty")
            {
                DBClass.InsertFaculty(FirstName, LastName, Username, Email);
                DBClass.CreateHashedUser(Username, Password, PersonType);


            }
            else if (PersonType == "Student")
            {
                DBClass.InsertStudent(FirstName, LastName, Username, Email, Phone);
                DBClass.CreateHashedUser(Username, Password, PersonType);
            }

            DBClass.Lab3DBConnection.Close();
            // Perform actual logic to check if user was successfully
            //  added in your projects but for demo purposes we can say:

            ViewData["UserCreate"] = "User Successfully Created!";

            return RedirectToPage("/OfficeHourSch/HashedLogin");
        }
    }
}