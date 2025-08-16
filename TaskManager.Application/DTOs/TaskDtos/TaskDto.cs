using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager;

namespace TaskManager.Application.DTOs.TaskDtos
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Domain.Enums.TaskStatus? Status { get; set; }
        public Domain.Enums.TaskPriority? Priority { get; set; }
        public Guid? AssigneeId { get; set; }

        public Guid CreatorId { get; set; }
        DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}