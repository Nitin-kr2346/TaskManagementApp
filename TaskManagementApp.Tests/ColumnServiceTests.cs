using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagementApp.Api.Models;
using TaskManagementApp.Api.Repositories;
using TaskManagementApp.Api.Services;

namespace TaskManagementApp.Tests
{
    public class ColumnServiceTests
    {
        private IColumnService _columnService;
        private Mock<IColumnRepository> _mockColumnRepository;

        [SetUp]
        public void Setup()
        {
            _mockColumnRepository = new Mock<IColumnRepository>();
            _columnService = new ColumnService(_mockColumnRepository.Object);
        }

        [Test]
        public void GetAllColumns_ReturnsColumns()
        {
            // Arrange
            var columns = new List<Column>
            {
                new Column { Id = 1, Name = "To Do", Tasks = new List<Task>() },
                new Column { Id = 2, Name = "In Progress", Tasks = new List<Task>() },
                new Column { Id = 3, Name = "Done", Tasks = new List<Task>() }
            };
            _mockColumnRepository.Setup(repo => repo.GetAllColumns()).Returns(columns);

            // Act
            var result = _columnService.GetAllColumns();

            // Assert
            Assert.AreEqual(columns, result);
        }

        [Test]
        public void GetColumnById_ValidId_ReturnsColumn()
        {
            // Arrange
            int columnId = 1;
            var column = new Column { Id = columnId, Name = "To Do", Tasks = new List<Task>() };
            _mockColumnRepository.Setup(repo => repo.GetColumnById(columnId)).Returns(column);

            // Act
            var result = _columnService.GetColumnById(columnId);

            // Assert
            Assert.AreEqual(column, result);
        }

        [Test]
        public void AddColumn_ValidColumn_ReturnsAddedColumn()
        {
            // Arrange
            var columnToAdd = new Column { Name = "New Column", Tasks = new List<Task>() };
            var addedColumn = new Column { Id = 1, Name = "New Column", Tasks = new List<Task>() };
            _mockColumnRepository.Setup(repo => repo.AddColumn(columnToAdd)).Returns(addedColumn);

            // Act
            var result = _columnService.AddColumn(columnToAdd);

            // Assert
            Assert.AreEqual(addedColumn, result);
        }

        [Test]
        public void UpdateColumn_ValidColumn_CallsRepositoryUpdateColumn()
        {
            // Arrange
            var columnToUpdate = new Column { Id = 1, Name = "Updated Column", Tasks = new List<Task>() };
            var column = new Column { Id = 1, Name = "To Do", Tasks = new List<Task>() };
            _mockColumnRepository.Setup(repo => repo.GetColumnById(1)).Returns(column);

            // Act
            _columnService.UpdateColumn(columnToUpdate);

            // Assert
            _mockColumnRepository.Verify(repo => repo.UpdateColumn(columnToUpdate), Times.Once);
        }

        [Test]
        public void DeleteColumn_ValidId_CallsRepositoryDeleteColumn()
        {
            // Arrange
            int columnIdToDelete = 1;
            var column = new Column { Id = columnIdToDelete, Name = "To Do", Tasks = new List<Task>() };
            _mockColumnRepository.Setup(repo => repo.GetColumnById(columnIdToDelete)).Returns(column);

            // Act
            _columnService.DeleteColumn(columnIdToDelete);

            // Assert
            _mockColumnRepository.Verify(repo => repo.DeleteColumn(columnIdToDelete), Times.Once);
        }

        [Test]
        public void SortSortTaskByNameAscending_ReturnsTaskSortedByNameAscending()
        {
            // Arrange
            var tasks = new List<Task>
            {
                new Task { Id = 1, Name = "Task B", Description = "Description for Task 1", Deadline = DateTime.Now.AddDays(7) },
                new Task { Id = 2, Name = "Task A", Description = "Description for Task 2", Deadline = DateTime.Now.AddDays(14) }
            };

            var column = new Column { Tasks = tasks };

            // Act
            var sortedTasks = _columnService.SortTaskByNameAscending(column).ToList();

            // Assert
            Assert.AreEqual("Task A", sortedTasks[0].Name);
            Assert.AreEqual("Task B", sortedTasks[1].Name);
        }
    }
}
