using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewLab1.Pages.DataClasses;
using NewLab1.Pages.DB;
using System.Data.SqlClient;

namespace NewLab1.Pages.OfficeHourSch
{
    public class JoinStudentsModel : PageModel
    {

        [BindProperty]
        public OfficeHours OfficeHourView { get; set; }



        public JoinStudentsModel()
        {

            OfficeHourView = new OfficeHours();

        }



        public void OnGet(int officehourid)
        {
            SqlDataReader singleProduct = DBClass.SingleOfficeReader(officehourid);

            while (singleProduct.Read())
            {
                OfficeHourView.OfficeHourID = officehourid;
                OfficeHourView.StudentName = singleProduct["studentName"].ToString();
            }
            DBClass.Lab3DBConnection.Close();


        }

        public IActionResult OnPost(int facultyID)
        {
            DBClass.UpdateOfficeHours(OfficeHourView);

            DBClass.Lab3DBConnection.Close();

            return RedirectToPage("/OfficeHourSch/ChooseFaculty", new {facultyID = facultyID});
        }

    }
}
