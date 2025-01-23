/*using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjectManagement.Helper;
using WebProjrctManagement.Helper;

namespace WebProjrctManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileService _fileService;

        public FilesController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            var fileUrl = await _fileService.UploadFileAsync(file);
            return Ok(new { FileUrl = fileUrl });
        }

        [HttpDelete("api/files/{publicId}")]
        public async Task<IActionResult> DeleteFile(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
            {
                return BadRequest("Public ID is required.");
            }

            // Decode the public ID to handle URL encoding (e.g., %2F -> /)
            var decodedPublicId = Uri.UnescapeDataString(publicId);

            var isDeleted = await _fileService.DeleteFileAsync(decodedPublicId);

            if (isDeleted)
            {
                return NoContent(); // Success, return 204 No Content status
            }
            else
            {
                return StatusCode(500, new { message = "An error occurred while deleting the file." });
            }
        }

    }
}
*/