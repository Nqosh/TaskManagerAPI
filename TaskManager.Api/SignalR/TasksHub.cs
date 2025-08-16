using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TaskManager.Api.SignalR;

[Authorize]
public class TasksHub : Hub {}
