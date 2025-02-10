using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjrctManagement.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentTaskController : ControllerBase
    {
        public readonly StudentTaskRepo _studentTaskRepo;

        public StudentTaskController(StudentTaskRepo studentTaskRepo)
        {
            _studentTaskRepo = studentTaskRepo;
        }

        [HttpGet]
        public IActionResult GetAllTask()
        {
            var task = _studentTaskRepo.getStudentTask();
            return Ok(task);
        }

        [HttpGet("getByID/{id}")]
        public IActionResult GetAllTaskByID(int id)
        {
            var task = _studentTaskRepo.getStudentTaskByID(id);
            return Ok(task);
        }

        [HttpPost]
        public IActionResult InsertTask([FromBody] StudentTaskModel studentTask)
        {
            if (studentTask == null)
            {
                return BadRequest();
            }

            bool isInserted = _studentTaskRepo.InsertStudentTask(studentTask);
            if (isInserted)
            {
                return Ok(new { Message = "Task inserted successfully!" });
            }

            return StatusCode(500, "An error occurred while inserting the Task.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] StudentTaskModel studentTask)
        {
            if (studentTask == null || id != studentTask.TaskID)
            {
                return BadRequest();
            }

            var isUpdated = _studentTaskRepo.UpdateStudentTask(studentTask);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var isDeleted = _studentTaskRepo.DeleteStudentTask(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{StudentProjectID}")]
        public IActionResult GetByStudentProjectID(int StudentProjectID)
        {
            var task = _studentTaskRepo.GetByStudentProjectID(StudentProjectID);
            return Ok(task);
        }
    }
}
