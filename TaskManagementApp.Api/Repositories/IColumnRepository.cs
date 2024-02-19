using System.Collections.Generic;
using TaskManagementApp.Api.Models;

namespace TaskManagementApp.Api.Repositories
{
    public interface IColumnRepository
    {
        IEnumerable<Column> GetAllColumns();
        Column GetColumnById(int id);
        Column AddColumn(Column column);
        void UpdateColumn(Column column);
        void DeleteColumn(int id);
    }
}
