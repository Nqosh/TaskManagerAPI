using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs.TaskDtos;
using TaskManager.Application.Interfaces;
using TaskManager.Api.Utils;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("tasksList")]
        public async Task<IActionResult> GetTasks([FromQuery] TaskQuery query)
        {
            var result = await _taskService.GetTasksAsync(query);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost("createtask")]
        public async Task<IActionResult> Post([FromBody] CreateTaskDto dto)
        {
            var userId = User.GetUserId(); 
            var createdTask = await _taskService.CreateAsync(dto, userId);
            return Ok(createdTask);
        }

        // PUT api/<TasksController>/5
        [HttpPut("updatetask")]
        public async Task<IActionResult> UpdateTask([FromQuery] Guid id, [FromBody] UpdateTaskDto dto)
        {
            var userId = User.GetUserId();
            var updated = await _taskService.UpdateAsync(id,dto, userId);
            return Ok(updated);
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("deleteTask")]
        public async Task<IActionResult> DeleteTask([FromQuery] Guid id)   
        {
            await _taskService.DeleteAsync(id);
            return Ok();
        }
    }
}
