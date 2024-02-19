using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TaskManagementApp.Api.Models;
using TaskManagementApp.Api.Services;

namespace TaskManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        }

        // GET: api/task
        [HttpGet]
        public ActionResult<IEnumerable<Task>> GetTasks()
        {
            var tasks = _taskService.GetAllTasks();
            return Ok(tasks);
        }

        // GET: api/task/5
        [HttpGet("{id}")]
        public ActionResult<Task> GetTask(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST: api/task
        [HttpPost]
        public ActionResult<Task> CreateTask(Task task)
        {
            var createdTask = _taskService.AddTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        // PUT: api/task/5
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }
            try
            {
                _taskService.UpdateTask(task);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/task/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                _taskService.DeleteTask(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
