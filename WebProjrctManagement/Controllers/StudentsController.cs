using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjectManagement.Model;
using WebProjrctManagement.Data;

namespace WebProjrctManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public readonly StudentsRepo _studentsRepo;

        public StudentsController(StudentsRepo studentsRepo)
        {
            _studentsRepo = studentsRepo;
        }
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentsRepo.GetStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentByID(int id)
        {
            var students = _studentsRepo.getStudentByID(id);
            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpPost]
        public IActionResult InsertStudent([FromBody] StudentsModel student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            bool isInserted = _studentsRepo.InsertStudent(student);
            if (isInserted)
            {
                return Ok(new { Message = "Student inserted successfully!" });
            }

            return StatusCode(500, "An error occurred while inserting the student.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] StudentsModel student)
        {
            if (student == null || id != student.StudentID)
            {
                return BadRequest();
            }

            var isUpdated = _studentsRepo.UpdateStudent(student);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var isDeleted = _studentsRepo.DeleteStudent(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("login/{email}/{password}")]
        public IActionResult StudentLogIN(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Email and Password are required." });
            }

            var student = _studentsRepo.StudentLogIN(email, password);

            if (student == null)
            {
                return Unauthorized(new { message = "Invalid Email or Password." });
            }

            return Ok(student);
        }

        [HttpGet("dropdown")]
        public IActionResult FacultyDropDown()
        {
            var student = _studentsRepo.StudentDropDown();
            return Ok(student);
        }

        [HttpGet("studentInfo/{id}")]
        public IActionResult GetStudentInfo(int id)
        {
            var students = _studentsRepo.GetStudentInfo(id);
            return Ok(students);
        }
    }
}