using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Month3AssessmentCode.Models;
using Month3AssessmentCode.services;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Month3AssessmentCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/<TaskController>
        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask(TaskModel taskModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _taskService.Createtask(userId,taskModel);
            return Ok(task);
        }
        [HttpPut("Updatetask")]
        
        public async Task<IActionResult> Updatetask(int taskId,[FromBody]TaskModel taskModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _taskService.UpdateTask(userId,taskId, taskModel);
            if(task ==null)
                return Unauthorized();
            return Ok(task);
        }

        [HttpGet("getTasks")]
        
        public async Task<IActionResult> getTasks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var task = await _taskService.GetAllTasks();
            if (task == null)
                return Unauthorized();
            return Ok(task);
        }


        
    }
}
