using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebProjectManagement.Model;
using WebProjrctManagement.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebProjrctManagement.Data
{
    public class StudentsRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public StudentsRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public List<StudentsModel> GetStudents()
        {
            var students = new List<StudentsModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Student_GetAllDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new StudentsModel
                    {
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        StudentName = reader["StudentName"].ToString(),
                        Enr_No = reader["Enr_No"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString(),
                        Password = reader["Password"].ToString()
                    });

                }
            }
            return students;
        }

        public StudentsModel getStudentByID(int studentID)
        {
            StudentsModel students = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Student_GetByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@StudentID", studentID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    students = new StudentsModel
                    {
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        StudentName = reader["StudentName"].ToString(),
                        Enr_No = reader["Enr_No"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString(),
                        Password = reader["Password"].ToString()
                    };
                }
            }
            return students;
        }

        public bool InsertStudent(StudentsModel student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Student_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@StudentName", student.StudentName);
                command.Parameters.AddWithValue("@Enr_No", student.Enr_No);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@PhoneNo", student.PhoneNo);
                command.Parameters.AddWithValue("@Password", student.Password);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;

            }
        }

        public bool UpdateStudent(StudentsModel student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Student_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@StudentID", student.StudentID);
                command.Parameters.AddWithValue("@StudentName", student.StudentName);
                command.Parameters.AddWithValue("@Enr_No", student.Enr_No);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@PhoneNo", student.PhoneNo);
                command.Parameters.AddWithValue("@Password", student.Password);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;

            }
        }

        public bool DeleteStudent(int studentID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Student_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@StudentID", studentID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public StudentsModel StudentLogIN(string email, string password)
        {
            StudentsModel students = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Student_LogIN", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    students = new StudentsModel
                    {
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        StudentName = reader["StudentName"].ToString(),
                        Enr_No = reader["Enr_No"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString(),
                        Password = reader["Password"].ToString()
                    };
                }
            }
            return students;
        }

        public List<StudentDropDownModel> StudentDropDown()
        {
            var students = new List<StudentDropDownModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Student_DropDown", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new StudentDropDownModel
                    {
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        StudentName = reader["StudentName"].ToString()
                    });
                }
            }
            return students;
        }

        public List<StudentInfoModel> GetStudentInfo(int studentID)
        {
            var studentInfo = new List<StudentInfoModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Get_Student_Project_Info", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@StudentID", studentID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    studentInfo.Add(new StudentInfoModel
                    {
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        StudentName = reader["StudentName"].ToString(),
                        FacultyID = Convert.ToInt32(reader["FacultyID"]),
                        FacultyName = reader["FacultyName"].ToString(),
                        ProjectID = Convert.ToInt32(reader["ProjectID"]),
                        ProjectDefinition = reader["ProjectDefinition"].ToString(),
                    });

                }
            }
            return studentInfo;
        }
    }
}
