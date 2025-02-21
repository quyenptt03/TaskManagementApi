using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IGenericRepository<User> _repository;

        public UserController(IGenericRepository<User> repository)
        {
            _repository = repository;

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

        [HttpPost]
        public ActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }
            else if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                return BadRequest("Username, email and password are required.");
            }
            else
            {
                try
                {
                    _repository.Add(user);
                    return Ok(user);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser([FromRoute] int id, [FromBody] User user)
        {
            var userExists = _repository.GetById(id);
            if (userExists == null || userExists.Id != user.Id)
            {
                return NotFound("User Not Found!!!!!!");
            }
            _repository.Update(user);
            return Ok(user);
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
