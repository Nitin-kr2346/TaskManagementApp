using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagementApp.Api.Models;

namespace TaskManagementApp.Api.Repositories
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly List<Column> _columns;
        private int _nextId = 3;

        public ColumnRepository()
        {
            // Initialize with some dummy data
            _columns = new List<Column>
            {
                new Column { Id = 1, Name = "To Do", Tasks = new List<Task>() },
                new Column { Id = 2, Name = "In Progress", Tasks = new List<Task>() },
                new Column { Id = 3, Name = "Done", Tasks = new List<Task>() }
            };
        }

        public IEnumerable<Column> GetAllColumns()
        {
            return _columns;
        }

        public Column GetColumnById(int id)
        {
            return _columns.FirstOrDefault(c => c.Id == id);
        }

        public Column AddColumn(Column column)
        {
            if (column == null)
            {
                throw new ArgumentNullException(nameof(column));
            }
            column.Id = _nextId++;
            _columns.Add(column);
            return column;
        }

        public void UpdateColumn(Column column)
        {
            if (column == null)
            {
                throw new ArgumentNullException(nameof(column));
            }
            var existingColumn = _columns.FirstOrDefault(c => c.Id == column.Id);
            if (existingColumn == null)
            {
                throw new InvalidOperationException($"Column with id {column.Id} not found.");
            }
            // Update existing column properties
            existingColumn.Name = column.Name;
        }

        public void DeleteColumn(int id)
        {
            var existingColumn = _columns.FirstOrDefault(c => c.Id == id);
            if (existingColumn != null)
            {
                _columns.Remove(existingColumn);
            }
        }
    }
}
