using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;
using Task = TaskManagementAPI.Models.Task;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : Controller
    {
        private readonly IGenericRepository<Task> _repository;

        public TaskController(IGenericRepository<Task> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult GetTasks()
        {
            var result = _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetTaskById([FromRoute] int id)
        {
            var task = _repository.GetById(id);
            return task == null ? NotFound("Task not found") : Ok(task);
        }

        [HttpPost]
        public ActionResult AddTask([FromBody] Task task)
        {
            if (task == null)
            {
                return BadRequest("Task cannot be null");
            }
            else if (string.IsNullOrWhiteSpace(task.Title))
            {
                return BadRequest("Task must have a title.");
            }
            else
            {
                try
                {
                    _repository.Add(task);
                    return Ok(task);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id, [FromBody] Task task)
        {
            var taskExist = _repository.GetById(id);
            if (taskExist == null)
            {
                return NotFound("Task Not Found!!!!!!");
            }
            try
            {
                taskExist.Title = task.Title;
                taskExist.Description = task.Description;
                taskExist.CategoryId = task.CategoryId;
                taskExist.IsCompleted = task.IsCompleted;

                _repository.Update(taskExist);
                return Ok(taskExist);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask([FromRoute] int id)
        {
            var task = _repository.GetById(id);
            if (task == null)
            {
                return NotFound("Task not found!!!!!");
            }

            try
            {
                _repository.Delete(id);
                return Ok("Task Deleted Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
