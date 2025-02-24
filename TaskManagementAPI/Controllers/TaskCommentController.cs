using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/task-comments")]
    public class TaskCommentController : Controller
    {
        private readonly IGenericRepository<TaskComment> _repository;

        public TaskCommentController(IGenericRepository<TaskComment> repository)
        {
            _repository = repository;

        }

        [HttpGet]
        public ActionResult GetTaskComments()
        {
            var result = (List<TaskComment>)_repository.GetAll();
            return Ok(result);
        }


        [HttpPost]
        public ActionResult AddCommentToTask([FromBody] TaskComment comment)
        {
            if (comment == null)
            {
                return BadRequest("Task comment cannot be null");
            }
            else
            {
                try
                {
                    _repository.Add(comment);
                    return Ok(comment);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteComment([FromRoute] int id)
        {
            var comment = _repository.GetById(id);
            if (comment == null)
            {
                return NotFound("Comment not found!!!!!");
            }

            try
            {
                _repository.Delete(id);
                return Ok("Commet Deleted Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
    }
}
