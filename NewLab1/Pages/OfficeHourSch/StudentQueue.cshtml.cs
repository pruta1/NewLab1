using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewLab1.Pages.DataClasses;
using NewLab1.Pages.DB;
using System.Data.SqlClient;

namespace NewLab1.Pages.OfficeHourSch
{
    public class StudentQueueModel : PageModel
    {
        public List<Queue> QueueList { get; set; }

        public List<Faculty> FacultyList { get; set; }

        public List<Student> StudentList { get; set; }

        public StudentQueueModel()
        {

            QueueList = new List<Queue>();
            FacultyList = new List<Faculty>();
            StudentList = new List<Student>();
        }


        public void OnGet(int studentID)
        {
            SqlDataReader SingleReader = DBClass.QueueReader();
            while (SingleReader.Read())
            {
                int currentStudentID = studentID;
                if (currentStudentID == studentID)
                {
                    QueueList.Add(new Queue
                    {
                        QueueID = int.Parse(SingleReader["QueueID"].ToString()),
                        QueueOrder = int.Parse(SingleReader["QueueOrder"].ToString()),
                        StudentID = currentStudentID,
                        FacultyID = int.Parse(SingleReader["facultyID"].ToString()),

                    });
                }
            }
            DBClass.Lab3DBConnection.Close();
        }

    }
}