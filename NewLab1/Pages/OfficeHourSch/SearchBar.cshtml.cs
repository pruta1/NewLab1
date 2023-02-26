using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Core.Configuration;
using NewLab1.Pages.DataClasses;
using System.Configuration;
using System.Data.SqlClient;

namespace NewLab1.Pages.OfficeHourSch
{
    public class SearchBarModel : PageModel
    {

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        public int? FacultyID { get; set; }

        public void OnGet()
        {
            // Do nothing
        }

        public IActionResult OnPost(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                // Invalid input
                return Page();
            }

            int facultyID;
            using (SqlConnection connection = new SqlConnection("Server=Localhost;Database=Lab3;Trusted_Connection=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Faculty WHERE fName = @firstName AND lName = @lastName", connection))
                {
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Found the faculty member
                        facultyID = Int32.Parse(reader["facultyID"].ToString());
                    }
                    else
                    {
                        // Faculty member not found
                        ModelState.AddModelError("", "Faculty member not found.");
                        return Page();
                    }
                }
            }

            // Redirect to OfficeHourPage with the FacultyID
           return RedirectToPage("OfficeHourPage", new { FacultyID = facultyID });
        }
    }
}