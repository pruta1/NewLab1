using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewLab1.Pages.DataClasses;
using NewLab1.Pages.DB;
using System.Buffers.Text;
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewLab1.Pages.DataClasses;
using NewLab1.Pages.DB;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace NewLab1.Pages.OfficeHourSch
{
    public class StudentAccountModel : PageModel
    {

        [BindProperty]
        public Student EditView { get; set; }

        [BindProperty]
        public IFormFile Picture { get; set; }

        public StudentAccountModel()
        {

            EditView = new Student();

        }

        public void OnGet(int studentid)
        {
            SqlDataReader SingleReader = DBClass.SingleStudentReader(studentid);
            while (SingleReader.Read())
            {
                EditView.StudentID = studentid;
                EditView.FirstName = SingleReader["fName"].ToString();
                EditView.LastName = SingleReader["lName"].ToString();
                EditView.StuEmail = SingleReader["studentEmail"].ToString();
                EditView.Phone = SingleReader["phone"].ToString();
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

            DBClass.UpdateStudent(EditView);
            DBClass.Lab3DBConnection.Close();

            SqlDataReader tempReader = DBClass.StudentReader();
            int studentID = 0;

            while (tempReader.Read())
            {
                studentID = (int)tempReader["StudentID"];
            }

            tempReader.Close();
            DBClass.Lab3DBConnection.Close();

            return RedirectToPage("/OfficeHourSch/StudentAccount", new { studentid = studentID });
        }
    }
}