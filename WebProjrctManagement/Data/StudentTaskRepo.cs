using Microsoft.Data.SqlClient;
using System.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Data
{
    public class StudentTaskRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public StudentTaskRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public List<StudentTaskModel> getStudentTask()
        {
            var studentTasks = new List<StudentTaskModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentTask_GetAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    studentTasks.Add(new StudentTaskModel
                    {
                        TaskID = Convert.ToInt32(reader["TaskID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        Status = reader["Status"].ToString(),
                        AssignDate = Convert.ToDateTime(reader["AssignDate"]),
                        StudentProjectID = Convert.ToInt32(reader["StudentProjectID"]),
                        ProjectID = Convert.ToInt32(reader["ProjectID"]),
                        ProjectDefinition = reader["ProjectDefinition"].ToString(),
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        StudentName = reader["StudentName"].ToString(),
                    });
                }

            }
            return studentTasks;
        }

        public StudentTaskModel getStudentTaskByID(int taskID)
        {
            StudentTaskModel studentTasks = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentTask_GetByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                command.Parameters.AddWithValue("@taskID", taskID);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    studentTasks = new StudentTaskModel
                    {
                        TaskID = Convert.ToInt32(reader["TaskID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        Status = reader["Status"].ToString(),
                        AssignDate = Convert.ToDateTime(reader["AssignDate"]),
                        StudentProjectID = Convert.ToInt32(reader["StudentProjectID"]),
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        StudentName = reader["StudentName"].ToString(),
                    };
                }

            }
            return studentTasks;
        }

        public bool InsertStudentTask(StudentTaskModel studentTask)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentTask_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@title", studentTask.Title);
                command.Parameters.AddWithValue("@Description", studentTask.Description);
                command.Parameters.AddWithValue("@StudentProjectID", studentTask.StudentProjectID);
                command.Parameters.AddWithValue("@Status", studentTask.Status);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateStudentTask(StudentTaskModel studentTask)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentTask_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@TaskID", studentTask.TaskID);
                command.Parameters.AddWithValue("@title", studentTask.Title);
                command.Parameters.AddWithValue("@Description", studentTask.Description);
                command.Parameters.AddWithValue("@StudentProjectID", studentTask.StudentProjectID);
                command.Parameters.AddWithValue("@Status", studentTask.Status);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteStudentTask(int TaskID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentTask_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@TaskID", TaskID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public List<StudentTaskModel> GetByStudentProjectID(int StudentProjectID)
        {
            var studentTasks = new List<StudentTaskModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentTask_GetByStudentProjectID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@StudentProjectID", StudentProjectID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    studentTasks.Add(new StudentTaskModel
                    {
                        TaskID = Convert.ToInt32(reader["TaskID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        Status = reader["Status"].ToString(),
                        AssignDate = Convert.ToDateTime(reader["AssignDate"]),
                        StudentProjectID = Convert.ToInt32(reader["StudentProjectID"]),
                        ProjectID = Convert.ToInt32(reader["ProjectID"]),
                        ProjectDefinition = reader["ProjectDefinition"].ToString(),
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        StudentName = reader["StudentName"].ToString(),
                    });
                }

            }
            return studentTasks;
        }
    }
}
