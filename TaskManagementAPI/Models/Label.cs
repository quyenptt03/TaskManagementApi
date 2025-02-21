﻿using System;
using System.Collections.Generic;

namespace TaskManagementAPI.Models;

public partial class Label
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
