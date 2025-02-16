using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebProjrctManagement.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        public readonly FacultyRepo _FacultyRepo;

        public FacultyController(FacultyRepo FacultyRepo)
        {
            _FacultyRepo = FacultyRepo;
        }
        [HttpGet]
        public IActionResult GetAllFaculty()
        {
            var Faculty = _FacultyRepo.GetFaculties();
            return Ok(Faculty);
        }

        [HttpGet("{id}")]
        public IActionResult GetFacultyByID(int id)
        {
            var Faculty = _FacultyRepo.GetFacultyByID(id);
            if (Faculty == null)
            {
                return NotFound();
            }
            return Ok(Faculty);
        }

        [HttpPost]
        public IActionResult InsertFaculty([FromBody] FacultyModel faculty)
        {
            if (faculty == null)
            {
                return BadRequest();
            }

            bool isInserted = _FacultyRepo.InsertFaculty(faculty);
            if (isInserted)
            {
                return Ok(new { Message = "Faculty inserted successfully!" });
            }

            return StatusCode(500, "An error occurred while inserting the faculty.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFaculty(int id, [FromBody] FacultyModel faculty)
        {
            if (faculty == null || id != faculty.FacultyID)
            {
                return BadRequest();
            }

            var isUpdated = _FacultyRepo.UpdateFaculty(faculty);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFaculty(int id)
        {
            var isDeleted = _FacultyRepo.DeleteFaculty(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult FacultyLogIN([FromBody] LogInModel logIn)
        {
            if (logIn == null)
            {
                return BadRequest(new { message = "Email and Password are required." });
            }
            var faculty = _FacultyRepo.FacultyLogIN(logIn);

            if (faculty == null)
            {
                return Unauthorized(new { message = "Invalid Email or Password." });
            }

            return Ok(faculty);
        }

        [HttpGet("dropdown")]
        public IActionResult FacultyDropDown()
        {
            var faculty = _FacultyRepo.FacultyDropDown();
            return Ok(faculty);
        }
    }
}
