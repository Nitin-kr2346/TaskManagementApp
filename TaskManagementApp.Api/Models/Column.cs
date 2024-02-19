using System.Collections.Generic;

namespace TaskManagementApp.Api.Models
{
    public class Column
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
