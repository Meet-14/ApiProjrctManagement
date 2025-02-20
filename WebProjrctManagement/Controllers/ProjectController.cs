using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjrctManagement.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        public readonly ProjectRepo _ProjectRepo;
        public ProjectController(ProjectRepo projectRepo) { 
            _ProjectRepo = projectRepo;
        }
        [HttpGet]
        public IActionResult GetAllProject()
        {
            var Project = _ProjectRepo.GetProjects();
            return Ok(Project);
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectByID(int id)
        {
            var Project = _ProjectRepo.GetProjectByID(id);
            if (Project == null)
            {
                return NotFound();
            }
            return Ok(Project);
        }

        [HttpPost]
        public IActionResult InsertProject([FromBody] ProjectModel project)
        {
            if (project == null)
            {
                return BadRequest();
            }

            bool isInserted = _ProjectRepo.InsertProject(project);
            if (isInserted)
            {
                return Ok(new { Message = "project inserted successfully!" });
            }

            return StatusCode(500, "An error occurred while inserting the project.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] ProjectModel project)
        {
            if (project == null || id != project.ProjectID)
            {
                return BadRequest();
            }

            var isUpdated = _ProjectRepo.UpdateProject(project);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var isDeleted = _ProjectRepo.DeleteProject(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            
            return NoContent();
        }

    }
}
