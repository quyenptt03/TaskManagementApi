using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Helpers;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IConfiguration _configuration;

        public UserController(IGenericRepository<User> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            var result = (List<User>)_repository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetCourseById([FromRoute] int id)
        {
            User user = _repository.GetById(id);
            return user == null ? NotFound("User not found") : Ok(user);
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] Auth registerUser)
        {
            if (registerUser.Username == null || registerUser.Email == null || registerUser.Password == null)
            {
                return BadRequest("Username, email and password are required.");
            }
            if (_repository.Any(u => u.Username == registerUser.Username))
            {
                return Conflict("Username already exists");
            }
            if (_repository.Any(u => u.Email == registerUser.Email))
            {
                return Conflict("Email already exists");
            }
            try
            {
                User user = new User
                {
                    Username = registerUser.Username,
                    Email = registerUser.Email,
                    PasswordHash = SecurePassword.Hash(registerUser.Password)
                };
                _repository.Add(user);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] Auth loginUser)
        {
            if (loginUser.Email == null || loginUser.Password == null)
            {
                return BadRequest("Email and password are required.");
            }
            var user = _repository.GetAll().FirstOrDefault(u => u.Email == loginUser.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }
            bool isValid = SecurePassword.Verify(loginUser.Password, user.PasswordHash);
            if (!isValid)
            {
                return Unauthorized("Invalid username or password.");
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );
            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = accessToken});
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCourse([FromRoute] int id)
        {
            var user = _repository.GetById(id);
            if (user == null)
            {
                return NotFound("User not found!!!!!");
            }

            try
            {
                _repository.Delete(id);
                return Ok("User Deleted Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
    }
}
