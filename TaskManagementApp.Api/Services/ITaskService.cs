using System.Collections.Generic;
using TaskManagementApp.Api.Models;

namespace TaskManagementApp.Api.Services
{
    public interface ITaskService
    {
        IEnumerable<Task> GetAllTasks();
        Task GetTaskById(int id);
        Task AddTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(int id);
    }
}
