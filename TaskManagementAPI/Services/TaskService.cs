using System.Text.Json;
using System.Xml.Linq;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class TaskService:ITaskService
    {
        private List<TaskItem> _tasks;
        private readonly string _filePath = "tasks.json";
        public TaskService()
        {
            _tasks = LoadTasks();
        }

        private List<TaskItem> LoadTasks()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
                return new List<TaskItem>();
            }

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        private void SaveTasks()
        {
            string json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<TaskItem> GetAllTasks()
        {
            return _tasks;
        }

        public TaskItem? GetTaskById(int id)
        {

            var task = _tasks.FirstOrDefault(task => task.ID == id);
            return task;
        }

        public void AddTask(TaskItem task)
        {
            if (task != null)
            {
                task.ID = _tasks.Count > 0 ? _tasks.Max(t => t.ID) + 1 : 1;
                _tasks.Add(task);
                SaveTasks();
            }
        }

        public void UpdateTask(TaskItem task)
        {
            var existTask = GetTaskById(task.ID);

            if (existTask != null)
            {
                existTask.Title = task.Title;
                existTask.Completed = task.Completed;
                SaveTasks();
            }
        }

        public bool DeleteTask(int id)
        {
            var task = GetTaskById(id);

            if (task == null)
            {
                return false;
            }

            _tasks.Remove(task);
            SaveTasks();
            return true;
        }
    }
}
