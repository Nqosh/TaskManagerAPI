using Microsoft.EntityFrameworkCore;
using TaskManager.Application.DTOs;
using TaskManager.Infrastructure.Persistence;
using TaskManager.Infrastructure.Services;
using AutoMapper;
using TaskManager.Application.Mapping;
using Xunit;
using TaskManager.Application.DTOs.TaskDtos;
using TaskManager.Domain.Enums;
using TaskManager.Domain;
using TaskManager.Infrastructure.Persistence.DatabaseContext;
public class TaskServiceTests
{
    private (AppDbContext db, TaskService service) Make()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var db = new AppDbContext(opts);
        var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper();
        var svc = new TaskService(db, mapper);
        return (db, svc);
    }
    [Fact]
    public async Task CreateTask_SetsDefaultsAndPersists()
    {
        var (db, svc) = Make();
        var dto = new CreateTaskDto { Title = "Test", Description =  "Desc", Priority = TaskPriority.MEDIUM, AssigneeId = null };
        var result = await svc.CreateAsync(dto, Guid.NewGuid());
        Assert.Equal("Test", result.Title);
        Assert.Equal( TaskManager.Domain.Enums.TaskStatus.TODO, result.Status);
        Assert.NotEqual(Guid.Empty, result.Id);
    }
    [Fact]
    public async Task UpdateTask_InvalidTransition_Throws()
    {
        var (db, svc) = Make();
        var t = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = "A",
            CreatorId = Guid.NewGuid(),
            Status = TaskManager.Domain.Enums.TaskStatus.DONE
        };
        db.Tasks.Add(t);
        await db.SaveChangesAsync();
        await Assert.ThrowsAsync<InvalidOperationException>(() => svc.UpdateAsync(t.Id, new UpdateTaskDto { Status = TaskManager.Domain.Enums.TaskStatus.IN_PROGRESS }, Guid.NewGuid()));
    }
}
