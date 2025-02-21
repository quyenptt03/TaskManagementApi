using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class TaskItem
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; }
        public int UserID { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Completed { get; set; }

        public TaskItem()
        {

        }

        public TaskItem(int id, string title, bool completed)
        {
            ID = id;
            Title = title;
            Completed = completed;
        }
    }
}
