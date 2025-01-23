using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Data
{
    public class FacultyRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public FacultyRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        public List<FacultyModel> GetFaculties()
        {
            var faculties = new List<FacultyModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Faculty_GetAllDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    faculties.Add(new FacultyModel
                    {
                        FacultyID = Convert.ToInt32(reader["FacultyID"]),
                        FacultyName = reader["FacultyName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString(),
                        Password = reader["Password"]?.ToString()
                    });
                }
            }
            return faculties;
        }

        public FacultyModel GetFacultyByID(int facultyID)
        {
            FacultyModel faculty = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Faculty_GetByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@FacultyID", facultyID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    faculty = new FacultyModel
                    {
                        FacultyID = Convert.ToInt32(reader["FacultyID"]),
                        FacultyName = reader["FacultyName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString(),
                        Password = reader["Password"]?.ToString()
                    };
                }
            }
            return faculty;
        }

        public bool InsertFaculty(FacultyModel faculty)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Faculty_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@FacultyName", faculty.FacultyName);
                command.Parameters.AddWithValue("@Email", faculty.Email);
                command.Parameters.AddWithValue("@PhoneNo", faculty.PhoneNo);
                command.Parameters.AddWithValue("@Password", faculty.Password);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateFaculty(FacultyModel faculty)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Faculty_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@FacultyID", faculty.FacultyID);
                command.Parameters.AddWithValue("@FacultyName", faculty.FacultyName);
                command.Parameters.AddWithValue("@Email", faculty.Email);
                command.Parameters.AddWithValue("@PhoneNo", faculty.PhoneNo);
                command.Parameters.AddWithValue("@Password", faculty.Password);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteFaculty(int facultyID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Faculty_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@FacultyID", facultyID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public FacultyModel FacultyLogIN(string email, string password)
        {
            FacultyModel faculty = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Faculty_LogIN", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    faculty = new FacultyModel
                    {
                        FacultyID = Convert.ToInt32(reader["FacultyID"]),
                        FacultyName = reader["FacultyName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString(),
                        Password = reader["Password"]?.ToString()
                    };
                }
            }
            return faculty;
        }

        public List<FacultyDropDownModel> FacultyDropDown()
        {
            var projects = new List<FacultyDropDownModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Faculty_DropDown", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    projects.Add(new FacultyDropDownModel
                    {
                        FacultyID = Convert.ToInt32(reader["FacultyID"]),
                        FacultyName = reader["FacultyName"].ToString()
                    });
                }
            }
            return projects;
        }
    }
}
