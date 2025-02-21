using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/taskitems")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskItemController(ITaskService taskService)
        {
            _taskService = taskService;
            
        }

        [HttpGet]
        public ActionResult GetTasks()
        {
            List<TaskItem> result = _taskService.GetAllTasks();
            return Ok(result);
        }

        [HttpGet("{taskId}")]
        public ActionResult GetCourseById([FromRoute] int taskId)
        {
            TaskItem task = _taskService.GetTaskById(taskId);
            return task == null ? NotFound("Task not found") : Ok(task);
        }

        [HttpPost]
        public ActionResult AddTask([FromBody] TaskItem task)
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
                    _taskService.AddTask(task);
                    return Ok(task);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        [HttpPut("{taskId}")]
        public ActionResult UpdateTask(int taskId, [FromBody] TaskItem task)
        {
            var taskExist = _taskService.GetTaskById(taskId);
            if (taskExist == null || taskExist.ID != task.ID)
            {
                return NotFound("Task Not Found!!!!!!");
            }
            _taskService.UpdateTask(task);
            return Ok(task);
        }

        [HttpDelete("{taskId}")]
        public ActionResult DeleteCourse([FromRoute] int taskId)
        {
            bool isDeleted = _taskService.DeleteTask(taskId);
            if (!isDeleted)
            {
                return NotFound("Task Not Found!!!!!!");
            }
            return Ok("Task Deleted Successfully");
        }


    }
}
