using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewLab1.Pages.DataClasses;
using NewLab1.Pages.DB;
using System.Data.SqlClient;

namespace NewLab1.Pages.OfficeHourSch
{
    public class FacultyAccountModel : PageModel
    {

        [BindProperty]
        public Faculty EditView { get; set; }


        [BindProperty]
        public IFormFile Picture { get; set; }



        public FacultyAccountModel()
        {

            EditView = new Faculty();

        }


        public void OnGet(int facultyid)
        {
            SqlDataReader SingleReader = DBClass.SingleFacultyReader(facultyid);
            while (SingleReader.Read())
            {
                EditView.FacultyID = facultyid;


                EditView.FirstName = SingleReader["fName"].ToString();
                EditView.LastName = SingleReader["lName"].ToString();
                EditView.FacultyEmail = SingleReader["facultyEmail"].ToString();
                EditView.Image = SingleReader["image"].ToString(); // add this line
            }
            DBClass.Lab3DBConnection.Close();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Picture != null && Picture.Length > 0)
            {
                // Save the uploaded picture
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Picture.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await Picture.CopyToAsync(stream);
                }
                EditView.Image = fileName;
            }

            DBClass.UpdateFaculty(EditView);
            DBClass.Lab3DBConnection.Close();

            SqlDataReader tempReader = DBClass.FacultyReader();
            int facultyID = 0;

            while (tempReader.Read())
            {
                facultyID = (int)tempReader["FacultyID"];
            }

            tempReader.Close();
            DBClass.Lab3DBConnection.Close();

            return RedirectToPage("/OfficeHourSch/FacultyAccount", new { facultyid = facultyID });
        }

    }
}

