using Amazon.Auth.AccessControlPolicy;
using MongoDB.Driver.Core.Configuration;
using NewLab1.Pages.DataClasses;
using System.Data;
using System.Data.SqlClient;
using static MongoDB.Driver.WriteConcern;

namespace NewLab1.Pages.DB
{
    public class DBClass
    {
        // Connection at the Class Level
        public static SqlConnection Lab3DBConnection = new SqlConnection();


        // Connection String 
        private static readonly String? Lab3DBConnString = "Server=Localhost;Database=Lab3;Trusted_Connection=True";
        private static readonly String? AuthConnString = "Server=Localhost;Database=AUTH;Trusted_Connection=True";


        public static SqlDataReader SingleOfficeReader(int OfficeHourID)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab3DBConnection;
            cmdProductRead.Connection.ConnectionString =
            Lab3DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM OfficeHours WHERE officeHoursID = " + OfficeHourID;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader SingleStudentReader(int StudentID)
        {
            SqlCommand cmdStudentRead = new SqlCommand();
            cmdStudentRead.Connection = Lab3DBConnection;
            cmdStudentRead.Connection.ConnectionString =
            Lab3DBConnString;
            cmdStudentRead.CommandText = "SELECT * FROM Student WHERE studentID = " + StudentID;
            cmdStudentRead.Connection.Open();
            SqlDataReader tempReader = cmdStudentRead.ExecuteReader();
            return tempReader;
        }
        public static SqlDataReader SingleFacultyReader(int FacultyID)
        {
            SqlCommand cmdFacultyRead = new SqlCommand();
            cmdFacultyRead.Connection = Lab3DBConnection;
            cmdFacultyRead.Connection.ConnectionString =
            Lab3DBConnString;
            cmdFacultyRead.CommandText = "SELECT * FROM Faculty WHERE facultyID = " + FacultyID;
            cmdFacultyRead.Connection.Open();
            SqlDataReader tempReader = cmdFacultyRead.ExecuteReader();
            return tempReader;
        }

        public static void UpdateStudent(Student s)
        {
            String sqlQuery = "UPDATE Student SET ";
            
            static SqlDataReader SingleStudentReader(int studentid)
            {
                throw new NotImplementedException();
            }

            static void UpdatePrduct(Student studentview)
            {
                throw new NotImplementedException();
            }
            sqlQuery = "UPDATE Student SET fName='" + s.FirstName + "', lName='" + s.LastName + "', studentEmail='" + s.StuEmail + "', phone='" + s.Phone + "', image='" + s.Image + "' WHERE studentID=" + s.StudentID;
            SqlCommand cmdStudentRead = new SqlCommand();
            cmdStudentRead.Connection = Lab3DBConnection;
            cmdStudentRead.Connection.ConnectionString =
            Lab3DBConnString;
            cmdStudentRead.CommandText = sqlQuery;
            cmdStudentRead.Connection.Open();
            cmdStudentRead.ExecuteNonQuery();
        }

        public static void UpdateFaculty(Faculty f)
        {
            String sqlQuery = "UPDATE Faculty SET ";

            static SqlDataReader SingleFacultyReader(int facultyid)
            {
                throw new NotImplementedException();
            }

            static void UpdatePrduct(Faculty facultyview)
            {
                throw new NotImplementedException();
            }
            sqlQuery = "UPDATE Faculty SET fName='" + f.FirstName + "', lName='" + f.LastName + "', facultyEmail='" + f.FacultyEmail + "', image='" + f.Image + "' WHERE facultyID=" + f.FacultyID;
            SqlCommand cmdFacultyRead = new SqlCommand();
            cmdFacultyRead.Connection = Lab3DBConnection;
            cmdFacultyRead.Connection.ConnectionString =
            Lab3DBConnString;
            cmdFacultyRead.CommandText = sqlQuery;
            cmdFacultyRead.Connection.Open();
            cmdFacultyRead.ExecuteNonQuery();
        }

        public static void UpdateOfficeHours(OfficeHours p)
        {
            String sqlQuery = "UPDATE OfficeHours SET ";

            static SqlDataReader SingleOfficeReader(int officehourid)
            {
                throw new NotImplementedException();
            }


            static void UpdateProduct(OfficeHours officehourview)
            {
                throw new NotImplementedException();
            }
            sqlQuery += "studentName='" + p.StudentName + "' WHERE officeHoursID= " + p.OfficeHourID;
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab3DBConnection;
            cmdProductRead.Connection.ConnectionString =
            Lab3DBConnString;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        //Sql Connection to get data from the faculty table 
        public static SqlDataReader FacultyReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab3DBConnection;
            cmdProductRead.Connection.ConnectionString = Lab3DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM Faculty";

            cmdProductRead.Connection.Open();

            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            // Do not close the connection here
            // Do not use a using statement

            return tempReader;
        }

        public static SqlDataReader QueueReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab3DBConnection;
            cmdProductRead.Connection.ConnectionString = Lab3DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM Queue";

            cmdProductRead.Connection.Open();

            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            // Do not close the connection here
            // Do not use a using statement

            return tempReader;
        }
        public static SqlDataReader StudentReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab3DBConnection;
            cmdProductRead.Connection.ConnectionString = Lab3DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM Student";

            cmdProductRead.Connection.Open();

            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }
       

       
        public static SqlDataReader GeneralReaderQuery(string sqlQuery)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab3DBConnection;
            cmdProductRead.Connection.ConnectionString = Lab3DBConnString;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }
        public static void InsertStudent(string Firstname, string Lastname, string Username, string Email, string Phone)
        {

            string loginQuery =
                "INSERT INTO Student (fName,lName, userName, studentEmail, phone) values (@fName, @lName, @userName, @studentEmail, @phone)";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab3DBConnection;
            cmdLogin.Connection.ConnectionString = Lab3DBConnString;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@fName", Firstname);
            cmdLogin.Parameters.AddWithValue("@lName", Lastname);
            cmdLogin.Parameters.AddWithValue("@userName", Username);
            cmdLogin.Parameters.AddWithValue("@studentEmail", Email);
            cmdLogin.Parameters.AddWithValue("@phone", Phone);

            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            cmdLogin.ExecuteNonQuery();


        }

        public static void InsertFaculty(string Firstname, string Lastname, string Username, string Email)
        {

            string loginQuery =
                "INSERT INTO Faculty (fName, lName, userName, facultyEmail) values (@fName, @lName, @userName, @facultyEmail)";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab3DBConnection;
            cmdLogin.Connection.ConnectionString = Lab3DBConnString;
            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@fName", Firstname);
            cmdLogin.Parameters.AddWithValue("@lName", Lastname);
            cmdLogin.Parameters.AddWithValue("@userName", Username);
            cmdLogin.Parameters.AddWithValue("@facultyEmail", Email);

            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            cmdLogin.ExecuteNonQuery();


        }

        public static void InsertOffice(int officenumber, string date, string time, int facultyID)
        {

            string loginQuery =
                "INSERT INTO OfficeHours (office#, dateID, timeID, facultyID) values (@office#, @dateID, @timeID, @facultyID)";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab3DBConnection;
            cmdLogin.Connection.ConnectionString = Lab3DBConnString;
            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@office#", officenumber);
            cmdLogin.Parameters.AddWithValue("@dateID", date);
            cmdLogin.Parameters.AddWithValue("@timeID", time);
            cmdLogin.Parameters.AddWithValue("@facultyID", facultyID);


            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            cmdLogin.ExecuteNonQuery();

        }

            public static SqlDataReader SelectFaculty(string username)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab3DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM Faculty WHERE userName = @username";
            cmdProductRead.Parameters.AddWithValue("@userName", username);
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader SelectStudent(string username)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = new SqlConnection();
            cmdProductRead.Connection.ConnectionString = Lab3DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM Student WHERE userName = @username";
            cmdProductRead.Parameters.AddWithValue("@username", username);
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        //SQL Connection for OfficeHour data 
        public static SqlDataReader SingleReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab3DBConnection;
            cmdProductRead.Connection.ConnectionString = Lab3DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM OfficeHours";

            cmdProductRead.Connection.Open();

            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }

        public static SqlDataReader SingleReader(int facultyID)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab3DBConnection;
            cmdProductRead.Connection.ConnectionString = Lab3DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM OfficeHours WHERE facultyID= @FacultyID";
            cmdProductRead.Parameters.AddWithValue("@FacultyID", facultyID);

            cmdProductRead.Connection.Open();

            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;
    }


        public static int LoginQuery(string loginQuery)
    {
        // This method expects to receive an SQL SELECT
        // query that uses the COUNT command.

            SqlCommand cmdLogin = new SqlCommand();
        cmdLogin.Connection = Lab3DBConnection;
        cmdLogin.Connection.ConnectionString = Lab3DBConnString;
        cmdLogin.CommandText = loginQuery;
        cmdLogin.Connection.Open();

        // ExecuteScalar() returns back data type Object
        // Use a typecast to convert this to an int.
        // Method returns first column of first row.
            int rowCount = (int)cmdLogin.ExecuteScalar();
        return rowCount;
    }


    public static int SecureLogin(string Username, string Password)
    {
        string loginQuery =
        "SELECT COUNT(*) FROM Credentials where Username = @Username and Password = @Password";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab3DBConnection;
            cmdLogin.Connection.ConnectionString = Lab3DBConnString;

            cmdLogin.CommandText = loginQuery;
            //replaces @username with the username from the method "SecureLogin" parameters 
            cmdLogin.Parameters.AddWithValue("@Username", Username);
            cmdLogin.Parameters.AddWithValue("@Password", Password);

            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            int rowCount = (int)cmdLogin.ExecuteScalar();

            return rowCount;
        }

        public static SqlDataReader GeneralStoredProcedureReader(string StoredProcedureName)
        {

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab3DBConnection;
            cmdProductRead.Connection.ConnectionString = Lab3DBConnString;
            cmdProductRead.CommandType = System.Data.CommandType.StoredProcedure;
            cmdProductRead.CommandText = StoredProcedureName;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }

        public static bool StoredProcedureLogin(string Username, string Password)
        {

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = Lab3DBConnection;
            cmdProductRead.Connection.ConnectionString = Lab3DBConnString;
            cmdProductRead.CommandType = System.Data.CommandType.StoredProcedure;
            cmdProductRead.Parameters.AddWithValue("@Username", Username);
            cmdProductRead.Parameters.AddWithValue("@Password", Password);
            cmdProductRead.CommandText = "sp_simpleLogin";
            cmdProductRead.Connection.Open();
            if (((int)cmdProductRead.ExecuteScalar()) > 0)
            {
                return true;
            }

            return false;
        }



        public static bool HashedParameterLogin(string Username, string Password, string PersonType)
        {


            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab3DBConnection;
            cmdLogin.Connection.ConnectionString = AuthConnString;

            cmdLogin.CommandType = CommandType.StoredProcedure;
            cmdLogin.CommandText = "sp_Lab3Login";
            cmdLogin.Parameters.AddWithValue("@Username", Username);
            cmdLogin.Parameters.AddWithValue("@Password", Password);
            //cmdLogin.Parameters.AddWithValue("@PersonType", PersonType);

            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            SqlDataReader hashReader = cmdLogin.ExecuteReader();
            if (hashReader.Read())
            {
                string correctHash = hashReader["Password"].ToString();

                if (PasswordHash.ValidatePassword(Password, correctHash))
                {
                    return true;
                }
            }

            return false;
        }


        public static void CreateHashedUser(string Username, string Password, string PersonType)
        {
            string loginQuery =
                "INSERT INTO HashedCredentials (Username,Password,PersonType) values (@Username, @Password, @PersonType)";

            using (SqlConnection connection = new SqlConnection(AuthConnString))
            using (SqlCommand cmdLogin = new SqlCommand(loginQuery, connection))
            {

                cmdLogin.CommandText = loginQuery;
                cmdLogin.Parameters.AddWithValue("@Username", Username);
                cmdLogin.Parameters.AddWithValue("@Password", PasswordHash.HashPassword(Password));
                cmdLogin.Parameters.AddWithValue("@PersonType", PersonType);

                cmdLogin.Connection.Open();

                // ExecuteScalar() returns back data type Object
                // Use a typecast to convert this to an int.
                // Method returns first column of first row.
                cmdLogin.ExecuteNonQuery();
            }

        }
        public static void InsertCredentials(string Username, string Password, string PersonType)
        {
            string loginQuery =
                "INSERT INTO Credentials (Username) values (@Username)";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab3DBConnection;
            cmdLogin.Connection.ConnectionString = Lab3DBConnString;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@Username", Username);

            cmdLogin.Connection.Open();

            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            cmdLogin.ExecuteNonQuery();




        }
    }
}

