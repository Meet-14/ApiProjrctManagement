using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjrctManagement.Helper;

namespace WebProjrctManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly string _imageDirectory =
            Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "Images");

        public ImageController()
        {
            if (!Directory.Exists(_imageDirectory))
                Directory.CreateDirectory(_imageDirectory);
        }

        [HttpPost("upload")]
        public IActionResult UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No image uploaded.");

            string fileName = ImageHelper.SaveImageToFile(imageFile, _imageDirectory);

            if (string.IsNullOrEmpty(fileName))
                return BadRequest("Failed to upload image.");

            string fileUrl = $"{Request.Scheme}://{Request.Host}/api/image/get/{fileName}";

            return Ok(new { imagePath = fileUrl });
        }

        [HttpGet("get/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            string filePath = Path.Combine(_imageDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Image not found.");

            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
            string mimeType = GetMimeType(filePath);

            return File(imageBytes, mimeType);
        }

        private string GetMimeType(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            return extension switch
            {
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                _ => "application/octet-stream"
            };
        }
    }

}