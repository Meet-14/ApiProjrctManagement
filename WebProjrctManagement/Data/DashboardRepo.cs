using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Data
{
    public class DashboardRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DashboardRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public List<MeetingModel> GetTop5Meetings()
        {
            var meetings = new List<MeetingModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Select_TopFive_Meeting", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    meetings.Add(new MeetingModel
                    {
                        MeetingID = Convert.ToInt32(reader["MeetingID"]),
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        StudentName = reader["StudentName"].ToString(),
                        FacultyID = Convert.ToInt32(reader["FacultyID"]),
                        FacultyName = reader["FacultyName"].ToString(),
                        ProjectID = Convert.ToInt32(reader["ProjectID"]),
                        ProjectDefinition = reader["ProjectDefinition"].ToString(),
                        Date = Convert.ToDateTime(reader["Date"]),
                        Discussion = reader["Discussion"].ToString(),
                        Remark = reader["Remark"].ToString()
                    });
                }
            }
            return meetings;
        }

        public List<StudentWorkModel> GetTop5StudentWorks()
        {
            var studentWorks = new List<StudentWorkModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Select_TopFive_Submit", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    studentWorks.Add(new StudentWorkModel
                    {
                        StudentWorkID = Convert.ToInt32(reader["StudentWorkID"]),
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        StudentName = reader["StudentName"].ToString(),
                        FileHeading = reader["FileHeading"].ToString(),
                        FilePath = reader["FilePath"].ToString(),
                        SubmittedDate = Convert.ToDateTime(reader["SubmittedDate"])
                    });
                }
            }
            return studentWorks;
        }

        public CountsModel GetCounts()
        {
            CountsModel counts = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Count", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    counts = new CountsModel
                    {
                        Meeting = Convert.ToInt32(reader["Meeting"]),
                        StudentProject = Convert.ToInt32(reader["StudentProject"]),
                        Students = Convert.ToInt32(reader["Students"]),
                        StudentWork = Convert.ToInt32(reader["StudentWork"])
                    };
                }
            }
            return counts;
        }
    }
}
