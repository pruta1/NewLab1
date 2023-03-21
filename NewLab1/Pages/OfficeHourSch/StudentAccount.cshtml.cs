using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewLab1.Pages.DataClasses;
using NewLab1.Pages.DB;
using System.Data.SqlClient;

namespace NewLab1.Pages.OfficeHourSch
{
    public class StudentAccountModel : PageModel
    {
        public List<Student> StudentAccount { get; set; }

        public StudentAccountModel()
        {
            StudentAccount = new List<Student>();  

        }


        public void OnGet(int studentID)
        {
            SqlDataReader SingleReader = DBClass.StudentReader();
            while (SingleReader.Read())
            {
                int currentStudentID = int.Parse(SingleReader["studentID"].ToString());
                if (currentStudentID == studentID)
                {
                    StudentAccount.Add(new Student
                    {
                        FirstName = SingleReader["fName"].ToString(),
                        LastName =  SingleReader["lName"].ToString(),
                        StuEmail = SingleReader["studentEmail"].ToString(),
                        Phone = SingleReader["phone"].ToString(),
                    });
                }
            }
            DBClass.Lab3DBConnection.Close();
        }

    }
}