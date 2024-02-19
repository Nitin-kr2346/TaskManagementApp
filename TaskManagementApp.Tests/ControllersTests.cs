using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TaskManagementApp.Api.Controllers;
using TaskManagementApp.Api.Models;
using TaskManagementApp.Api.Services;

namespace TaskManagementApp.Tests
{
    public class ControllersTests
    {
        private TaskController _taskController;
        private ColumnController _columnController;

        private Mock<ITaskService> _mockTaskService;
        private Mock<IColumnService> _mockColumnService;

        [SetUp]
        public void Setup()
        {
            _mockTaskService = new Mock<ITaskService>();
            _mockColumnService = new Mock<IColumnService>();

            _taskController = new TaskController(_mockTaskService.Object);
            _columnController = new ColumnController(_mockColumnService.Object);
        }

        // TaskController Tests
        [Test]
        public void GetAllTasks_ReturnsOkResult_WithTasks()
        {
            // Arrange
            var tasks = new List<Task>
            {
                new Task { Id = 1, Name = "Task 1", Description = "Description for Task 1", Deadline = DateTime.Now.AddDays(7) },
                new Task { Id = 2, Name = "Task 2", Description = "Description for Task 2", Deadline = DateTime.Now.AddDays(14) }
            };
            _mockTaskService.Setup(service => service.GetAllTasks()).Returns(tasks);

            // Act
            var result = _taskController.GetTasks();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(tasks, okResult.Value);
        }

        [Test]
        public void GetTaskById_ExistingId_ReturnsOkResult_WithTask()
        {
            // Arrange
            int taskId = 1;
            var task = new Task { Id = taskId, Name = "Task 1", Description = "Description for Task 1", Deadline = DateTime.Now.AddDays(7) };
            _mockTaskService.Setup(service => service.GetTaskById(taskId)).Returns(task);

            // Act
            var result = _taskController.GetTask(taskId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(task, okResult.Value);
        }

        [Test]
        public void GetTaskById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            int taskId = 1;
            _mockTaskService.Setup(service => service.GetTaskById(taskId)).Returns((Task)null);

            // Act
            var result = _taskController.GetTask(taskId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void AddTask_ValidTask_ReturnsCreatedResult_WithTask()
        {
            // Arrange
            var taskToAdd = new Task { Name = "New Task", Description = "Description for New Task", Deadline = DateTime.Now.AddDays(5) };
            var addedTask = new Task { Id = 1, Name = "New Task", Description = "Description for New Task", Deadline = DateTime.Now.AddDays(5) };
            _mockTaskService.Setup(service => service.AddTask(taskToAdd)).Returns(addedTask);

            // Act
            var result = _taskController.CreateTask(taskToAdd);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.AreEqual("GetTask", createdAtActionResult.ActionName);
            Assert.AreEqual(addedTask, createdAtActionResult.Value);
        }

        [Test]
        public void UpdateTask_ValidTask_ReturnsNoContentResult()
        {
            // Arrange
            var taskToUpdate = new Task { Id = 1, Name = "Task 1", Description = "Updated Description for Task 1", Deadline = DateTime.Now.AddDays(7) };

            // Act
            var result = _taskController.UpdateTask(1, taskToUpdate);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void UpdateTask_InvalidTask_ReturnsBadRequestResult()
        {
            // Arrange
            var taskToUpdate = new Task { Id = 2, Name = "Task 2", Description = "Updated Description for Task 2", Deadline = DateTime.Now.AddDays(7) };

            // Act
            var result = _taskController.UpdateTask(1, taskToUpdate);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void DeleteTask_ExistingId_ReturnsNoContentResult()
        {
            // Arrange
            int taskIdToDelete = 1;

            // Act
            var result = _taskController.DeleteTask(taskIdToDelete);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void DeleteTask_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            int taskIdToDelete = 1;
            _mockTaskService.Setup(service => service.DeleteTask(taskIdToDelete)).Throws(new InvalidOperationException());

            // Act
            var result = _taskController.DeleteTask(taskIdToDelete);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        // ColumnController Tests
        [Test]
        public void GetAllColumns_ReturnsOkResult_WithColumns()
        {
            // Arrange
            var columns = new List<Column>
            {
                new Column { Id = 1, Name = "To Do", Tasks = new List<Task>() },
                new Column { Id = 2, Name = "In Progress", Tasks = new List<Task>() },
                new Column { Id = 3, Name = "Done", Tasks = new List<Task>() }
            };
            _mockColumnService.Setup(service => service.GetAllColumns()).Returns(columns);

            // Act
            var result = _columnController.GetColumns();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(columns, okResult.Value);
        }

        [Test]
        public void GetColumnById_ExistingId_ReturnsOkResult_WithColumn()
        {
            // Arrange
            int columnId = 1;
            var column = new Column { Id = columnId, Name = "To Do", Tasks = new List<Task>() };
            _mockColumnService.Setup(service => service.GetColumnById(columnId)).Returns(column);

            // Act
            var result = _columnController.GetColumn(columnId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(column, okResult.Value);
        }

        [Test]
        public void GetColumnById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            int columnId = 1;
            _mockColumnService.Setup(service => service.GetColumnById(columnId)).Returns((Column)null);

            // Act
            var result = _columnController.GetColumn(columnId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void AddColumn_ValidColumn_ReturnsCreatedResult_WithColumn()
        {
            // Arrange
            var columnToAdd = new Column { Name = "New Column", Tasks = new List<Task>() };
            var addedColumn = new Column { Id = 1, Name = "New Column", Tasks = new List<Task>() };
            _mockColumnService.Setup(service => service.AddColumn(columnToAdd)).Returns(addedColumn);

            // Act
            var result = _columnController.CreateColumn(columnToAdd);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.AreEqual("GetColumn", createdAtActionResult.ActionName);
            Assert.AreEqual(addedColumn, createdAtActionResult.Value);
        }

        [Test]
        public void UpdateColumn_ValidColumn_ReturnsNoContentResult()
        {
            // Arrange
            var columnToUpdate = new Column { Id = 1, Name = "Updated Column", Tasks = new List<Task>() };

            // Act
            var result = _columnController.UpdateColumn(1, columnToUpdate);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void UpdateColumn_InvalidColumn_ReturnsBadRequestResult()
        {
            // Arrange
            var columnToUpdate = new Column { Id = 2, Name = "In Progress", Tasks = new List<Task>() };

            // Act
            var result = _columnController.UpdateColumn(1, columnToUpdate);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void DeleteColumn_ExistingId_ReturnsNoContentResult()
        {
            // Arrange
            int columnIdToDelete = 1;

            // Act
            var result = _columnController.DeleteColumn(columnIdToDelete);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void DeleteColumn_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            int columnIdToDelete = 1;
            _mockColumnService.Setup(service => service.DeleteColumn(columnIdToDelete)).Throws(new InvalidOperationException());

            // Act
            var result = _columnController.DeleteColumn(columnIdToDelete);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
