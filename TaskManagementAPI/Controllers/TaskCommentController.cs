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

        [HttpGet("{id}")]
        public ActionResult GetTaskCommentById([FromRoute] int id)
        {
            TaskComment TaskComment = _repository.GetById(id);
            return TaskComment == null ? NotFound("TaskComment not found") : Ok(TaskComment);
        }

        [HttpPost]
        public ActionResult AddTaskComment([FromBody] TaskComment taskComment)
        {
            if (taskComment == null)
            {
                return BadRequest("TaskComment cannot be null");
            }
            else
            {
                try
                {
                    _repository.Add(taskComment);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTaskComment([FromRoute] int id, [FromBody] TaskComment taskComment)
        {
            var taskCommentExists = _repository.GetById(id);
            if (taskCommentExists == null || taskCommentExists.Id != taskComment.Id)
            {
                return NotFound("Task Comment Not Found!!!!!!");
            }
            _repository.Update(taskComment);
            return Ok(taskComment);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCourse([FromRoute] int id)
        {
            var TaskComment = _repository.GetById(id);
            if (TaskComment == null)
            {
                return NotFound("TaskComment not found!!!!!");
            }

            try
            {
                _repository.Delete(id);
                return Ok("TaskComment Deleted Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
    }
}
