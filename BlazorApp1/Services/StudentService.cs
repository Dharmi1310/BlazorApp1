
using BlazorApp1.Models;
using MySql.Data.MySqlClient;
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

            string query = "INSERT INTO student(Student_Id,Student_Name,Student_Class) VALUES(@id,@name,@class)";
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

            string query = "SELECT * FROM student order by Student_Id ";
            using var cmd = new MySqlCommand(query, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Student
                {
                    Student_Id = reader.GetInt32("Student_Id"),
                    Student_Name = reader.GetString("Student_Name"),
                    Student_Class = reader.GetInt32("Student_Class")
                });
            }
            return list;
        }

        public bool ValidateUser(string userid, string password)
        {
            using var con = new MySqlConnection(_conn);
            con.Open();

            string query = "SELECT COUNT(*) FROM userdetails WHERE userid=@u AND password=@p";
            using var cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@u", userid);
            cmd.Parameters.AddWithValue("@p", password);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }
}
