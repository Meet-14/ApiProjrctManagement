using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjrctManagement.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        public readonly MeetingRepo _meetingRepo;
        public MeetingController(MeetingRepo meetingRepo)
        {
            _meetingRepo = meetingRepo;
        }
        [HttpGet]
        public IActionResult GetAllMeeting()
        {
            var Meeting = _meetingRepo.GetMeetings();
            return Ok(Meeting);
        }

        [HttpGet("{id}")]
        public IActionResult GetMeetingByID(int id)
        {
            var Meeting = _meetingRepo.GetMeetingByID(id);
            if (Meeting == null)
            {
                return NotFound();
            }
            return Ok(Meeting);
        }

        [HttpPost]
        public IActionResult InsertMeeting([FromBody] MeetingModel project)
        {
            if (project == null)
            {
                return BadRequest();
            }

            bool isInserted = _meetingRepo.InsertMeeting(project);
            if (isInserted)
            {
                return Ok(new { Message = "project inserted successfully!" });
            }

            return StatusCode(500, "An error occurred while inserting the project.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMeeting(int id, [FromBody] MeetingModel project)
        {
            if (project == null || id != project.MeetingID)
            {
                return BadRequest();
            }

            var isUpdated = _meetingRepo.UpdateMeeting(project);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMeeting(int id)
        {
            var isDeleted = _meetingRepo.DeleteMeeting(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("gatByStudentID/{id}")]
        public IActionResult GetMeetingByStudentID(int id)
        {
            var Meeting = _meetingRepo.GetMeetingByStudentID(id);
            if (Meeting == null)
            {
                return NotFound();
            }
            return Ok(Meeting);
        }

        [HttpGet("GetMeetingsByDateRange")]
        public async Task<IActionResult> GetMeetingsByDateRange(DateTime fromDate, DateTime toDate)
        {
            var result = await _meetingRepo.GetMeetingsByDateRange(fromDate, toDate);
            return Ok(result);
        }
    }
}
