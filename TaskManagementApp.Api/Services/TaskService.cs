using System;
using System.Collections.Generic;
using TaskManagementApp.Api.Models;
using TaskManagementApp.Api.Repositories;

namespace TaskManagementApp.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _taskRepository.GetAllTasks();
        }

        public Task GetTaskById(int id)
        {
            return _taskRepository.GetTaskById(id);
        }

        public Task AddTask(Task task)
        {
            // You can add additional business logic/validation here if needed
            return _taskRepository.AddTask(task);
        }

        public void UpdateTask(Task task)
        {
            // Check if task exists
            var existingTask = _taskRepository.GetTaskById(task.Id);
            if (existingTask == null)
            {
                throw new InvalidOperationException($"Task with id {task.Id} not found.");
            }
            _taskRepository.UpdateTask(task);
        }

        public void DeleteTask(int id)
        {
            // Check if task exists
            var existingTask = _taskRepository.GetTaskById(id);
            if (existingTask == null)
            {
                throw new InvalidOperationException($"Task with id {id} not found.");
            }
            _taskRepository.DeleteTask(id);
        }
    }
}
