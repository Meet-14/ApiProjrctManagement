using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Data
{
    public class StudentProjectRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public StudentProjectRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public List<StudentProjectModel> GetStudentProjects()
        {
            var studentProjects = new List<StudentProjectModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentProject_GetDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    studentProjects.Add(new StudentProjectModel
                    {
                        StudentProjectID = Convert.ToInt32(reader["StudentProjectID"]),
                        ProjectID = Convert.ToInt32(reader["ProjectID"]),
                        ProjectDefinition = reader["ProjectDefinition"].ToString(),
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        StudentName = reader["StudentName"].ToString(),
                        FacultyID = Convert.ToInt32(reader["FacultyID"]),
                        FacultyName = reader["FacultyName"].ToString(),
                        AcademicYear = reader["AcademicYear"].ToString(),
                        StartingDate = Convert.ToDateTime(reader["StartingDate"]),
                        MeetingsConducted = Convert.ToInt32(reader["MeetingsConducted"])
                    });
                }
            }
            return studentProjects;
        }

        public StudentProjectModel GetStudentProjectByID(int studentProjectID)
        {
            StudentProjectModel studentProject = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentProject_GetByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@StudentProjectID", studentProjectID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    studentProject = new StudentProjectModel
                    {
                        StudentProjectID = Convert.ToInt32(reader["StudentProjectID"]),
                        ProjectID = Convert.ToInt32(reader["ProjectID"]),
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        FacultyID = Convert.ToInt32(reader["FacultyID"]),
                        AcademicYear = reader["AcademicYear"].ToString(),
                        StartingDate = Convert.ToDateTime(reader["StartingDate"]),
                        MeetingsConducted = Convert.ToInt32(reader["MeetingsConducted"])
                    };
                }
            }
            return studentProject;
        }

        public bool InsertStudentProject(StudentProjectModel studentProject)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentProject_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ProjectID", studentProject.ProjectID);
                command.Parameters.AddWithValue("@StudentID", studentProject.StudentID);
                command.Parameters.AddWithValue("@FacultyID", studentProject.FacultyID);
                command.Parameters.AddWithValue("@AcademicYear", studentProject.AcademicYear);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateStudentProject(StudentProjectModel studentProject)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentProject_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@StudentProjectID", studentProject.StudentProjectID);
                command.Parameters.AddWithValue("@ProjectID", studentProject.ProjectID);
                command.Parameters.AddWithValue("@StudentID", studentProject.StudentID);
                command.Parameters.AddWithValue("@FacultyID", studentProject.FacultyID);
                command.Parameters.AddWithValue("@AcademicYear", studentProject.AcademicYear);
                command.Parameters.AddWithValue("@StartingDate", studentProject.StartingDate);
                command.Parameters.AddWithValue("@MeetingsConducted", studentProject.MeetingsConducted);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteStudentProject(int studentProjectID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentProject_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@StudentProjectID", studentProjectID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
