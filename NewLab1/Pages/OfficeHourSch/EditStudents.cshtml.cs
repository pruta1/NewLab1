using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewLab1.Pages.DataClasses;
using NewLab1.Pages.DB;
using System.Data.SqlClient;

namespace NewLab1.Pages.OfficeHourSch
{
    public class EditStudentsModel : PageModel
    {

        [BindProperty]
        public OfficeHours EditView { get; set; }



        public EditStudentsModel()
        {

            EditView = new OfficeHours();

        }



        public void OnGet(int officehourid)
        {
            SqlDataReader singleProduct = DBClass.SingleOfficeReader(officehourid);

            while (singleProduct.Read())
            {
                EditView.OfficeHourID = officehourid;
                EditView.StudentName = singleProduct["studentName"].ToString();
            }
            DBClass.Lab3DBConnection.Close();


        }

        public IActionResult OnPost()
        {
            DBClass.UpdateOfficeHours(EditView);
            DBClass.Lab3DBConnection.Close();

            SqlDataReader tempReader = DBClass.SingleReader();
            int facultyID = 0;

            while (tempReader.Read())
            {
                facultyID = (int)tempReader["FacultyID"];
            }

            tempReader.Close();
            DBClass.Lab3DBConnection.Close();

            return RedirectToPage("/OfficeHourSch/OfficeHourPageTeacher", new { facultyid = facultyID });
        }
    }
}