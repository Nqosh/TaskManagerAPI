using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.DTOs.TaskDtos;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetTasksAsync(TaskQuery query);
        Task<TaskDto> CreateAsync(CreateTaskDto dto, Guid creatorId);
        Task<TaskDto> UpdateAsync(Guid Id,UpdateTaskDto dto, Guid creatorId);
        Task  DeleteAsync(Guid id);
    }
}
