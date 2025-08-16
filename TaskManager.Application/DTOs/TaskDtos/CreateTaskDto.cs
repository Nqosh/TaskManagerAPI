namespace TaskManager.Application.DTOs.TaskDtos
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Domain.Enums.TaskPriority Priority { get; set; }
        public Guid? AssigneeId { get; set; }
    }
}