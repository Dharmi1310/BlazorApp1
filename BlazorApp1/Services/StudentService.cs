using BlazorApp1.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace BlazorApp1.Services
{

    public class StudentService
    {
        private readonly string _conn;

        public StudentService(IConfiguration config)
        {
            _conn = config.GetConnectionString("MySqlConnection");
        }

        public void AddStudent(Student st)
        {
            using var con = new MySqlConnection(_conn);
            con.Open();
            
            string query = "INSERT INTO student(Student_Id,Student_Name,Student_Class,userid,phone_number,email_id) VALUES(@id,@name,@class,@name,'9989505220','xyz@email.com')";
            using var cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", st.Student_Id);
            cmd.Parameters.AddWithValue("@name", st.Student_Name);
            cmd.Parameters.AddWithValue("@class", st.Student_Class);

            cmd.ExecuteNonQuery();
        }

        public void UpdateStudent(Student st)
        {
            using var con = new MySqlConnection(_conn);
            con.Open();

            string query = @"UPDATE student 
                     SET Student_Name = @name,
                         Student_Class = @class
                     WHERE Student_Id = @id";

            using var cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", st.Student_Id);
            cmd.Parameters.AddWithValue("@name", st.Student_Name);
            cmd.Parameters.AddWithValue("@class", st.Student_Class);

            cmd.ExecuteNonQuery();
        }

        public void DeleteStudent(int id)
        {
            using var con = new MySqlConnection(_conn);
            con.Open();

            string query = "DELETE FROM student WHERE Student_Id=@id";
            using var cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
      
        public List<Student> GetAll()
        {
            var list = new List<Student>();

            using var con = new MySqlConnection(_conn);
            con.Open();

            string query = "SELECT * FROM student order by student_id";
            using var cmd = new MySqlCommand(query, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Student
                {
                    
                    Student_Id = reader.GetInt32("Student_Id"),
                    Student_Name = reader.GetString("Student_Name"),
                    Student_Class = reader.GetInt32("Student_Class"),
                    Phone_number = reader.GetString("Phone_number"),
                    email_id=reader.GetString("email_id")
                });
            }
            return list;
        }

        public bool ValidateUser(string userid, string password)
        {
            using var con = new MySqlConnection(_conn);
            con.Open();

            string query = "SELECT COUNT(*) FROM userdetails WHERE userid=@userid AND password=@password";
            using var cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@password", password);
            
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        public List<Courses> getcourses()
        {
            var list = new List<Courses>();

            using var con = new MySqlConnection(_conn);
            con.Open();

            string query = "SELECT * FROM courses ";
            using var cmd = new MySqlCommand(query, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Courses
                {

                    S_no = reader.GetInt32("S_no"),
                    course_name = reader.GetString("course_name"),
                    status = reader.GetString("status")
                });
            }
            return list;

        }
        public void AddUser(Userdetails ud)
        {
            using var con = new MySqlConnection(_conn);
            con.Open();

            string query = "INSERT INTO student(userid,password) VALUES(@userid,@password)";
            using var cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@userid", ud.userid);
            cmd.Parameters.AddWithValue("@password", ud.password);

            cmd.ExecuteNonQuery();
        }

    }
}
