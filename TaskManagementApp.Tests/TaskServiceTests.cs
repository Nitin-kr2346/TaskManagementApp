using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TaskManagementApp.Api.Models;
using TaskManagementApp.Api.Repositories;
using TaskManagementApp.Api.Services;

namespace TaskManagementApp.Tests
{
    public class TaskServiceTests
    {
        private ITaskService _taskService;
        private Mock<ITaskRepository> _mockTaskRepository;

        [SetUp]
        public void Setup()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _taskService = new TaskService(_mockTaskRepository.Object);
        }

        [Test]
        public void GetAllTasks_ReturnsTasks()
        {
            // Arrange
            var tasks = new List<Task>
            {
                new Task { Id = 1, Name = "Task 1", Description = "Description for Task 1", Deadline = DateTime.Now.AddDays(7) },
                new Task { Id = 2, Name = "Task 2", Description = "Description for Task 2", Deadline = DateTime.Now.AddDays(14) }
            };
            _mockTaskRepository.Setup(repo => repo.GetAllTasks()).Returns(tasks);

            // Act
            var result = _taskService.GetAllTasks();

            // Assert
            Assert.AreEqual(tasks, result);
        }

        [Test]
        public void GetTaskById_ValidId_ReturnsTask()
        {
            // Arrange
            int taskId = 1;
            var task = new Task { Id = taskId, Name = "Task 1", Description = "Description for Task 1", Deadline = DateTime.Now.AddDays(7) };
            _mockTaskRepository.Setup(repo => repo.GetTaskById(taskId)).Returns(task);

            // Act
            var result = _taskService.GetTaskById(taskId);

            // Assert
            Assert.AreEqual(task, result);
        }

        [Test]
        public void AddTask_ValidTask_ReturnsAddedTask()
        {
            // Arrange
            var taskToAdd = new Task { Name = "New Task", Description = "Description for New Task", Deadline = DateTime.Now.AddDays(5) };
            var addedTask = new Task { Id = 1, Name = "New Task", Description = "Description for New Task", Deadline = DateTime.Now.AddDays(5) };
            _mockTaskRepository.Setup(repo => repo.AddTask(taskToAdd)).Returns(addedTask);

            // Act
            var result = _taskService.AddTask(taskToAdd);

            // Assert
            Assert.AreEqual(addedTask, result);
        }

        [Test]
        public void UpdateTask_ValidTask_CallsRepositoryUpdateTask()
        {
            // Arrange
            var taskToUpdate = new Task { Id = 1, Name = "Task 1", Description = "Updated Description for Task 1", Deadline = DateTime.Now.AddDays(7) }; 
            var task = new Task { Id = 1, Name = "Task 1", Description = "Description for Task 1", Deadline = DateTime.Now.AddDays(7) };
            _mockTaskRepository.Setup(repo => repo.GetTaskById(1)).Returns(task);

            // Act
            _taskService.UpdateTask(taskToUpdate);

            // Assert
            _mockTaskRepository.Verify(repo => repo.UpdateTask(taskToUpdate), Times.Once);
        }

        [Test]
        public void DeleteTask_ValidId_CallsRepositoryDeleteTask()
        {
            // Arrange
            int taskIdToDelete = 1;
            var task = new Task { Id = taskIdToDelete, Name = "Task 1", Description = "Description for Task 1", Deadline = DateTime.Now.AddDays(7) };
            _mockTaskRepository.Setup(repo => repo.GetTaskById(taskIdToDelete)).Returns(task);

            // Act
            _taskService.DeleteTask(taskIdToDelete);

            // Assert
            _mockTaskRepository.Verify(repo => repo.DeleteTask(taskIdToDelete), Times.Once);
        }
    }
}
