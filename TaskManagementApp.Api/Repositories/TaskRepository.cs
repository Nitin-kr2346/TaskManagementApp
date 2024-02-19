using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagementApp.Api.Models;

namespace TaskManagementApp.Api.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<Task> _tasks;
        private int _nextId = 2;

        public TaskRepository()
        {
            // Initialize with some dummy data
            _tasks = new List<Task>
            {
                new Task { Id = 1, Name = "Task 1", Description = "Description for Task 1", Deadline = DateTime.Now.AddDays(7) },
                new Task { Id = 2, Name = "Task 2", Description = "Description for Task 2", Deadline = DateTime.Now.AddDays(14) }
            };
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _tasks;
        }

        public Task GetTaskById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public Task AddTask(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            task.Id = _nextId++;
            _tasks.Add(task);
            return task;
        }

        public void UpdateTask(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask == null)
            {
                throw new InvalidOperationException($"Task with id {task.Id} not found.");
            }
            // Update existing task properties
            existingTask.Name = task.Name;
            existingTask.Description = task.Description;
            existingTask.Deadline = task.Deadline;
        }

        public void DeleteTask(int id)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask != null)
            {
                _tasks.Remove(existingTask);
            }
        }
    }
}
