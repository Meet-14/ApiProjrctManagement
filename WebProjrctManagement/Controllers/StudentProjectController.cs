using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProjrctManagement.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentProjectController : ControllerBase
    {
        private readonly StudentProjectRepo _studentProjectRepo;

        public StudentProjectController(StudentProjectRepo studentProjectRepo)
        {
            _studentProjectRepo = studentProjectRepo;
        }

        // Get all student projects
        [HttpGet]
        public IActionResult GetAllStudentProjects()
        {
            var studentProjects = _studentProjectRepo.GetStudentProjects();
            return Ok(studentProjects);
        }

        // Get student project by ID
        [HttpGet("{id}")]
        public IActionResult GetStudentProjectByID(int id)
        {
            var studentProject = _studentProjectRepo.GetStudentProjectByID(id);
            if (studentProject == null)
            {
                return NotFound();
            }
            return Ok(studentProject);
        }

        // Insert a new student project
        [HttpPost]
        public IActionResult InsertStudentProject([FromBody] StudentProjectModel studentProject)
        {
            if (studentProject == null)
            {
                return BadRequest();
            }

            bool isInserted = _studentProjectRepo.InsertStudentProject(studentProject);
            if (isInserted)
            {
                return Ok(new { Message = "Student project inserted successfully!" });
            }

            return StatusCode(500, "An error occurred while inserting the student project.");
        }

        // Update an existing student project
        [HttpPut("{id}")]
        public IActionResult UpdateStudentProject(int id, [FromBody] StudentProjectModel studentProject)
        {
            if (studentProject == null || id != studentProject.StudentProjectID)
            {
                return BadRequest();
            }

            var isUpdated = _studentProjectRepo.UpdateStudentProject(studentProject);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // Delete a student project
        [HttpDelete("{id}")]
        public IActionResult DeleteStudentProject(int id)
        {
            var isDeleted = _studentProjectRepo.DeleteStudentProject(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
