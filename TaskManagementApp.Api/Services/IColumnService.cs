using System.Collections.Generic;
using TaskManagementApp.Api.Models;

namespace TaskManagementApp.Api.Services
{
    public interface IColumnService
    {
        IEnumerable<Column> GetAllColumns();
        Column GetColumnById(int id);
        Column AddColumn(Column column);
        void UpdateColumn(Column column);
        void DeleteColumn(int id);
        IEnumerable<Task> SortTaskByNameAscending(Column column);
        IEnumerable<Task> SortTaskByNameDescending(Column column);
    }
}
