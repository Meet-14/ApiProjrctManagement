using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.IO;
using System.Threading.Tasks;
namespace WebProjectManagement.Helper
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var cloudName = configuration["CloudinarySettings:CloudName"];
            var apiKey = configuration["CloudinarySettings:ApiKey"];
            var apiSecret = configuration["CloudinarySettings:ApiSecret"];

            _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret))
            {
                Api = { Secure = true }
            };
        }

        public async Task<string> UploadFileAsync(string localFilePath)
        {
            try
            {
                if (string.IsNullOrEmpty(localFilePath) || !File.Exists(localFilePath))
                    throw new ArgumentException("Invalid file path.");

                // Use RawUploadParams for resource_type:auto
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(localFilePath)
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                // Delete the local file after upload
                File.Delete(localFilePath);

                // Return the secure URL of the uploaded file
                return uploadResult.SecureUrl?.ToString();
            }
            catch (Exception ex)
            {
                // Handle error (e.g., log it)
                Console.WriteLine($"Upload failed: {ex.Message}");

                // Delete the local file if upload fails
                if (File.Exists(localFilePath))
                    File.Delete(localFilePath);

                return null; // Return null if the upload fails
            }
        }

        public async Task<bool> DeleteFileAsync(string publicId)
        {
            try
            {
                // Decode the public ID to remove any URL encoding
                var decodedPublicId = Uri.UnescapeDataString(publicId);

                // Create DeletionParams with the decoded public ID
                var deletionParams = new DeletionParams(decodedPublicId);

                // Perform the deletion synchronously (cloudinary does not support DestroyAsync)
                var deletionResult = _cloudinary.Destroy(deletionParams);

                // Log full result for debugging
                if (deletionResult.Result == "ok")
                {
                    Console.WriteLine($"File with public ID {decodedPublicId} was successfully deleted.");
                    return true;
                }
                else
                {
                    // Detailed error logging
                    Console.WriteLine($"Failed to delete file with public ID {decodedPublicId}. Error: {deletionResult.Error?.Message}");
                    if (deletionResult.Error != null)
                    {
                        Console.WriteLine($"Error details: {deletionResult.Error}");
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the file: {ex.Message}");
                return false;
            }
        }


    }


}
