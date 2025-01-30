using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Data
{
    public class MeetingRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public MeetingRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public List<MeetingModel> GetMeetings()
        {
            var meetings = new List<MeetingModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Meeting_GetAllDetails", connection)
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

        public MeetingModel GetMeetingByID(int meetingID)
        {
            MeetingModel meeting = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Meeting_GetByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@MeetingID", meetingID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    meeting = new MeetingModel
                    {
                        MeetingID = Convert.ToInt32(reader["MeetingID"]),
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        FacultyID = Convert.ToInt32(reader["FacultyID"]),
                        ProjectID = Convert.ToInt32(reader["ProjectID"]),
                        Date = Convert.ToDateTime(reader["Date"]),
                        Discussion = reader["Discussion"].ToString(),
                        Remark = reader["Remark"].ToString()
                    };
                }
            }
            return meeting;
        }

        public bool InsertMeeting(MeetingModel meeting)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Meeting_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@StudentID", meeting.StudentID);
                command.Parameters.AddWithValue("@FacultyID", meeting.FacultyID);
                command.Parameters.AddWithValue("@ProjectID", meeting.ProjectID);
                command.Parameters.AddWithValue("@Discussion", meeting.Discussion);
                command.Parameters.AddWithValue("@Remark", meeting.Remark);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateMeeting(MeetingModel meeting)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Meeting_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@MeetingID", meeting.MeetingID);
                command.Parameters.AddWithValue("@StudentID", meeting.StudentID);
                command.Parameters.AddWithValue("@FacultyID", meeting.FacultyID);
                command.Parameters.AddWithValue("@ProjectID", meeting.ProjectID);
                command.Parameters.AddWithValue("@Date", meeting.Date);
                command.Parameters.AddWithValue("@Discussion", meeting.Discussion);
                command.Parameters.AddWithValue("@Remark", meeting.Remark);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteMeeting(int meetingID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Meeting_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@MeetingID", meetingID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public List<MeetingModel> GetMeetingByStudentID(int studentID)
        {
            var meetings = new List<MeetingModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Meeting_GetByStudentID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@StudentID",studentID);
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

        public async Task<StatisticsModel> GetMeetingsByDateRange(DateTime fromDate, DateTime toDate)
        {
            var statisticsData = new StatisticsModel
            {
                MeetingDetails = new List<MeetingModel>(),
                MeetingCount = new List<MeetingCountByFaculty>()
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("PR_Select_Date_wise_Meeting_With_Count", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@from", fromDate);
                    command.Parameters.AddWithValue("@to", toDate);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                statisticsData.MeetingDetails.Add(new MeetingModel
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

                            if (await reader.NextResultAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    statisticsData.MeetingCount.Add(new MeetingCountByFaculty
                                    {
                                        FacultyID = Convert.ToInt32(reader["FacultyID"]),
                                        FacultyName = reader["FacultyName"].ToString(),
                                        MeetingCount = Convert.ToInt32(reader["MeetingCount"])
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return statisticsData;
        }

    }
}
