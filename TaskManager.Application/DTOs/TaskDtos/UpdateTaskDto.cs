namespace TaskManager.Application.DTOs.TaskDtos
{
    public class UpdateTaskDto
    {
        public Domain.Enums.TaskStatus? Status { get; set; }
    }
}