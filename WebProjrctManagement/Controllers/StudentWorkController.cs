using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjrctManagement.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentWorkController : ControllerBase
    {
        private readonly StudentWorkRepo _studentWorkRepo;

        public StudentWorkController(StudentWorkRepo studentWorkRepo)
        {
            _studentWorkRepo = studentWorkRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllStudentWorks()
        {
            var studentWorks = _studentWorkRepo.GetStudentWorks();
            return Ok(studentWorks);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetStudentWorkByID(int id)
        {
            var studentWork = _studentWorkRepo.GetStudentWorkByID(id);
            if (studentWork == null)
            {
                return NotFound();
            }
            return Ok(studentWork);
        }

        [HttpPost]
        public async Task<IActionResult> InsertStudentWork([FromForm] StudentWorkModel studentWork)
        {
            if (studentWork == null || studentWork.formFile == null || studentWork.formFile.Length == 0)
            {
                return BadRequest(new { Message = "Invalid input. Please provide valid student work data and a file." });
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Model validation failed.", Errors = errors });
            }

            try
            {
                bool isInserted = await _studentWorkRepo.InsertStudentWork(studentWork);
                if (isInserted)
                {
                    return Ok(new { Message = "Student work inserted successfully!" });
                }

                return StatusCode(500, "An error occurred while inserting the student work.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudentWork(int id, [FromBody] StudentWorkModel studentWork)
        {
            if (studentWork == null || id != studentWork.StudentWorkID)
            {
                return BadRequest();
            }

            var isUpdated = _studentWorkRepo.UpdateStudentWork(studentWork);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteStudentWorkAsync(int id)
        {
            try
            {
                // Attempt to delete the student work
                var isDeleted = await _studentWorkRepo.DeleteStudentWork(id);
                if (!isDeleted)
                {
                    return NotFound(new { Message = "Student work not found or could not be deleted." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the student work.", Error = ex.Message });
            }
        }

        [HttpGet("DownloadFile/{id}")]
        [Authorize]
        public IActionResult DownloadFile(int id)
        {
            var studentWork = _studentWorkRepo.GetStudentWorkByID(id);
            if (studentWork == null)
            {
                return NotFound();
            }

            string fileUrl = studentWork.FilePath;
            if (string.IsNullOrEmpty(fileUrl))
            {
                return BadRequest("File URL is required.");
            }

            try
            {
                using (var client = new HttpClient())
                {
                    fileUrl = Uri.UnescapeDataString(fileUrl);
                    Console.WriteLine(fileUrl);
                    var fileUri = new Uri(fileUrl);
                    Console.WriteLine($"Requesting file from URL: {fileUri}");

                    var response = client.GetAsync(fileUri).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        return NotFound("Unable to download the file.");
                    }

                    var fileBytes = response.Content.ReadAsByteArrayAsync().Result;
                    var fileName = fileUrl.Substring(fileUrl.LastIndexOf('/') + 1);

                    // Set the Content-Disposition header to prompt file download
                    Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");

                    return File(fileBytes, "application/octet-stream");
                }
            }
            catch (Exception ex)
            {
                // Log the error or handle the exception
                return StatusCode(500, new { Message = "An error occurred while downloading the file.", Error = ex.Message });
            }
        }

        [HttpGet("Student/{id}")]
        [Authorize]
        public IActionResult GetStudentWorkByStudentID(int id)
        {
            var studentWork = _studentWorkRepo.GetStudentWorkByStudentID(id);
            if (studentWork == null)
            {
                return NotFound();
            }
            return Ok(studentWork);
        }
    }
}
