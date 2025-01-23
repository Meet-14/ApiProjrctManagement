using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace WebProjrctManagement.Helper
{
    public class FileService
    {
        private readonly Cloudinary _cloudinary;

        public FileService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = "user_uploads"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.ToString();
        }
        public async Task<bool> DeleteFileAsync(string publicId)
        {
            try
            {
                var decodedPublicId = Uri.UnescapeDataString(publicId);

                var deletionParams = new DeletionParams(decodedPublicId)
                {
                    ResourceType = ResourceType.Raw
                };

                var deletionResult = _cloudinary.Destroy(deletionParams);

                if (deletionResult.Result == "ok")
                {
                    Console.WriteLine($"File with public ID {decodedPublicId} was successfully deleted.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to delete file with public ID {decodedPublicId}. Error: {deletionResult.Error?.Message}");
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
