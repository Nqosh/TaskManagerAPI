using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.TODO;
        public TaskPriority Priority { get; set; } = TaskPriority.MEDIUM;
        public Guid? AssignedId { get; set; }
        public User? Assigneee { get; set; }
        public Guid? CreatorId  { get; set; }
        public User? Creator { get; set; }  
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateDate { get; set; }    

    }
}
