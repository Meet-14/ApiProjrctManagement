using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Data
{
    public class StudentWorkRepo
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly Cloudinary _cloudinary;

        public StudentWorkRepo(IConfiguration configuration, Cloudinary cloudinary)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString");
            _cloudinary = cloudinary;
        }
        public List<StudentWorkModel> GetStudentWorks()
        {
            var studentWorks = new List<StudentWorkModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentWork_GetAll", connection)
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
        public StudentWorkModel GetStudentWorkByID(int studentWorkID)
        {
            StudentWorkModel studentWork = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentWork_GetByID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@StudentWorkID", studentWorkID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    studentWork = new StudentWorkModel
                    {
                        StudentWorkID = Convert.ToInt32(reader["StudentWorkID"]),
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        FileHeading = reader["FileHeading"].ToString(),
                        FilePath = reader["FilePath"].ToString(),
                        SubmittedDate = Convert.ToDateTime(reader["SubmittedDate"])
                    };
                }
            }
            return studentWork;
        }

        public async Task<bool> InsertStudentWork(StudentWorkModel studentWork)
        {
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription((studentWork.formFile).FileName, (studentWork.formFile).OpenReadStream()),
                Folder = "user_uploads"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            studentWork.FilePath = uploadResult.SecureUrl.ToString();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentWork_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@StudentID", studentWork.StudentID);
                command.Parameters.AddWithValue("@FileHeading", studentWork.FileHeading);
                command.Parameters.AddWithValue("@FilePath", studentWork.FilePath);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool UpdateStudentWork(StudentWorkModel studentWork)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentWork_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@StudentWorkID", studentWork.StudentWorkID);
                command.Parameters.AddWithValue("@StudentID", studentWork.StudentID);
                command.Parameters.AddWithValue("@FileHeading", studentWork.FileHeading);
                command.Parameters.AddWithValue("@FilePath", studentWork.FilePath);
                command.Parameters.AddWithValue("@SubmittedDate", studentWork.SubmittedDate);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public async Task<bool> DeleteStudentWork(int studentWorkID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Step 1: Fetch the file URL from the database
                    SqlCommand getFilePathCommand = new SqlCommand("PR_GetStudentWorkFilePath", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    getFilePathCommand.Parameters.AddWithValue("@StudentWorkID", studentWorkID);

                    await connection.OpenAsync();
                    string filePath = (string)await getFilePathCommand.ExecuteScalarAsync();
                    connection.Close();
                    if (string.IsNullOrEmpty(filePath))
                    {
                        return false;
                    }

                    // Step 2: delete file from _cloudinary
                    string publicId = GetPublicIdFromUrl(filePath);
                    var deletionParams = new DeletionParams(publicId)
                    {
                        ResourceType = ResourceType.Raw
                    };

                    var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
                    if (deletionResult.Result != "ok")
                    {
                        throw new Exception($"Failed to delete the file from Cloudinary. Details: {deletionResult.Error?.Message}");
                    }

                    // Step 3: Delete the record from the database
                    SqlCommand deleteCommand = new SqlCommand("PR_StudentWork_Delete", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    deleteCommand.Parameters.AddWithValue("@StudentWorkID", studentWorkID);

                    await connection.OpenAsync();
                    int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();
                    connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (or rethrow it if necessary)
                throw new Exception("Error while deleting student work: " + ex.Message, ex);
            }
        }
        private string GetPublicIdFromUrl(string fileUrl)
        {
            var uri = new Uri(fileUrl);
            string[] segments = uri.AbsolutePath.Split('/');

            // Combine the folder and file name with extension
            return string.Join("/", segments.Skip(segments.Length - 2));
        }

        public List<StudentWorkModel> GetStudentWorkByStudentID(int StudentID)
        {
            var studentWorks = new List<StudentWorkModel>();    
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_StudentWork_GetByStudentID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@StudentID", StudentID);
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

    }
}
