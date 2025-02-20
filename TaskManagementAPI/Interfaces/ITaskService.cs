using System.Text.Json;
using System.Xml.Linq;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Interfaces
{
    public interface ITaskService
    {
        List<TaskItem> GetAllTasks();
        TaskItem? GetTaskById(int id);
        void AddTask(TaskItem task);
        void UpdateTask(TaskItem task);
        bool DeleteTask(int id);

    }
}
