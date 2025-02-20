using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using WebProjectManagement.Model;
using WebProjrctManagement.Data;
using WebProjrctManagement.Model;

namespace WebProjrctManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly StudentsRepo _studentsRepo;
        private readonly FacultyRepo _facultyRepo;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(StudentsRepo studentsRepo, FacultyRepo facultyRepo, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _studentsRepo = studentsRepo;
            _facultyRepo = facultyRepo;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LogInModel userLoginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid login data." });
                }

                object user = null;
                string role = "";
                string token = "";

                if (userLoginModel.UserType?.ToLower() == "faculty")
                {
                    user = _facultyRepo.FacultyLogIN(userLoginModel);
                    role = "Faculty";
                }
                else if (userLoginModel.UserType?.ToLower() == "student")
                {
                    user = _studentsRepo.StudentLogIN(userLoginModel);
                    role = "Student";
                }
                else
                {
                    return BadRequest(new { message = "Invalid user type." });
                }

                if (user != null)
                {
                    if (role == "Faculty")
                    {
                        var faculty = (FacultyModel)user;
                        token = GenerateJwtToken(faculty.FacultyID.ToString(), faculty.Email, role, faculty.FacultyName);
                        return Ok(new
                        {
                            userId = faculty.FacultyID,
                            username = faculty.FacultyName,
                            EmailAddress = faculty.Email,
                            role,
                            token,
                            message = "Login successful"
                        });
                    }
                    else if (role == "Student")
                    {
                        var student = (StudentsModel)user;
                        token = GenerateJwtToken(student.StudentID.ToString(), student.Email, role, student.StudentName);
                        return Ok(new
                        {
                            userId = student.StudentID,
                            username = student.StudentName,
                            EmailAddress = student.Email,
                            role,
                            token,
                            message = "Login successful"
                        });
                    }
                }

                return Unauthorized(new { message = "Invalid email or password." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred during login.");
                return StatusCode(500, new { message = "Unexpected error occurred. Please try again." });
            }
        }


        private string GenerateJwtToken(string userId, string email, string role, string username)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? "");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, username)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}