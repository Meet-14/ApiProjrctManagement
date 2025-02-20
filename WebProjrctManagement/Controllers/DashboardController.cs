using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjrctManagement.Data;

namespace WebProjrctManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        public readonly DashboardRepo _dashboardRepo;

        public DashboardController(DashboardRepo dashboardRepo)
        {
            _dashboardRepo = dashboardRepo;
        }

        [HttpGet("Meetings")]
        public IActionResult GetTop5Meetings()
        {
            var meetings = _dashboardRepo.GetTop5Meetings();
            return Ok(meetings);
        }

        [HttpGet("StudentWork")]
        public IActionResult GetTop5StudentWork()
        {
            var work = _dashboardRepo.GetTop5StudentWorks();
            return Ok(work);
        }

        [HttpGet("Counts")]
        public IActionResult GetCounts()
        {
            var count = _dashboardRepo.GetCounts();
            return Ok(count);
        }
    }
}
