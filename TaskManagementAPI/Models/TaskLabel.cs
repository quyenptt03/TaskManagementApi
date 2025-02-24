using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementAPI.Models
{
    public class TaskLabel
    {
        public int TaskId { get; set; }

        public int LabelId { get; set; }

        [ForeignKey("LabelId")]
        public virtual Label? Label { get; set; }
        [ForeignKey("TaskId")]
        public virtual Task? Task { get; set; }
    }
}
