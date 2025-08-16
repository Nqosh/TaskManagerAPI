namespace TaskManager.Application.DTOs.TaskDtos
{
    public class TaskQuery
    {
        public Guid Id { get; set; }
        public Domain.Enums.TaskStatus? Status { get; set; }
        public Guid? Assignee { get; set; }
        public string? Search { get; set; }
    }
}