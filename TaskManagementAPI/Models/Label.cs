using System;
using System.Collections.Generic;

namespace TaskManagementAPI.Models;

public partial class Label
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TaskLabel> TaskLabels { get; set; } = new List<TaskLabel>();
}
