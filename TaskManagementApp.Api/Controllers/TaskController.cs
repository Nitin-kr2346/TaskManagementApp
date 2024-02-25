using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TaskManagementApp.Api.Models;
using TaskManagementApp.Api.Services;

namespace TaskManagementApp.Api.Controllers
{
    /// <summary>
    /// Controller for managing Tasks
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        }

        /// <summary>
        /// Get all the tasks, GET: api/task
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Task>> GetTasks()
        {
            var tasks = _taskService.GetAllTasks();
            return Ok(tasks);
        }

        /// <summary>
        /// GET: api/task/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// POST: api/task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Task> CreateTask(Task task)
        {
            var createdTask = _taskService.AddTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        /// <summary>
        /// PUT: api/task/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="task"></param>
        /// <returns></returns>
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

        /// <summary>
        /// DELETE: api/task/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
