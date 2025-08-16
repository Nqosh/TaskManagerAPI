using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.DTOs.TaskDtos;
using TaskManager.Application.Interfaces;
using TaskManager.Domain;
using TaskManager.Domain.Enums;
using TaskManager.Infrastructure.Persistence.DatabaseContext;

namespace TaskManager.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public TaskService(AppDbContext db, IMapper mapper)
        {
            _db = db;
           _mapper = mapper;
        }
        public async Task<IEnumerable<TaskDto>> GetTasksAsync(TaskQuery q)
        {
            var query  = _db.Tasks.AsQueryable();
            if(q.Status.HasValue)
                query = query.Where(t => t.Status == q.Status);
            if(q.Assignee.HasValue)
                query =  query.Where(t => t.AssignedId == q.Assignee);
            if(!string.IsNullOrEmpty(q.Search))
                query = query.Where(t => t.Title.Contains(q.Search!) || (t.Description ?? "").Contains(q.Search!));
            var list  = await query.OrderByDescending(t => t.CreateDate).ToListAsync();
            return list.Select(t => _mapper.Map<TaskDto>(t)).ToList();  
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto dto, Guid creatorId)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                AssignedId = dto.AssigneeId,
                CreatorId = creatorId,
                Status = Domain.Enums.TaskStatus.TODO,
                CreateDate = DateTime.UtcNow
            };

            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> UpdateAsync(Guid id, UpdateTaskDto dto, Guid userId)
        {
           var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id) ?? throw new KeyNotFoundException("task not found");

            if (dto.Status.HasValue && !IsValidTransition(task.Status, dto.Status.Value))
                throw new InvalidOperationException("Invalid status transition");
            if(dto.Status.HasValue)
                task.Status = dto.Status.Value;
            task.UpdateDate = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return _mapper.Map<TaskDto>(task);
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new KeyNotFoundException("Task not found");
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
        }

        private static bool IsValidTransition(Domain.Enums.TaskStatus from, Domain.Enums.TaskStatus to)
        {
            if(from == to) return true;
            return (from, to) switch
            {
                (Domain.Enums.TaskStatus.TODO, Domain.Enums.TaskStatus.IN_PROGRESS) => true,
                (Domain.Enums.TaskStatus.IN_PROGRESS, Domain.Enums.TaskStatus.DONE) => true,
                _ => false
            };
        }
    }
}
