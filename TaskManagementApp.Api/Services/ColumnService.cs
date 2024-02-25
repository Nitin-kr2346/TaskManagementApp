using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagementApp.Api.Models;
using TaskManagementApp.Api.Repositories;

namespace TaskManagementApp.Api.Services
{
    public class ColumnService : IColumnService
    {
        private readonly IColumnRepository _columnRepository;

        public ColumnService(IColumnRepository columnRepository)
        {
            _columnRepository = columnRepository ?? throw new ArgumentNullException(nameof(columnRepository));
        }

        public IEnumerable<Column> GetAllColumns()
        {
            return _columnRepository.GetAllColumns();
        }

        public Column GetColumnById(int id)
        {
            return _columnRepository.GetColumnById(id);
        }

        public Column AddColumn(Column column)
        {
            // You can add additional business logic/validation here if needed
            return _columnRepository.AddColumn(column);
        }

        public void UpdateColumn(Column column)
        {
            // Check if column exists
            var existingColumn = _columnRepository.GetColumnById(column.Id);
            if (existingColumn == null)
            {
                throw new InvalidOperationException($"Column with id {column.Id} not found.");
            }
            _columnRepository.UpdateColumn(column);
        }

        public void DeleteColumn(int id)
        {
            // Check if column exists
            var existingColumn = _columnRepository.GetColumnById(id);
            if (existingColumn == null)
            {
                throw new InvalidOperationException($"Column with id {id} not found.");
            }
            _columnRepository.DeleteColumn(id);
        }

        public IEnumerable<Task> SortTaskByNameAscending(Column column)
        {
            return column.Tasks.OrderBy(task => task.Name);
        }

        public IEnumerable<Task> SortTaskByNameDescending(Column column)
        {
            return column.Tasks.OrderByDescending(task => task.Name);
        }
    }
}
