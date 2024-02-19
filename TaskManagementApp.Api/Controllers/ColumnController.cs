using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TaskManagementApp.Api.Models;
using TaskManagementApp.Api.Services;

namespace TaskManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly IColumnService _columnService;

        public ColumnController(IColumnService columnService)
        {
            _columnService = columnService ?? throw new ArgumentNullException(nameof(columnService));
        }

        // GET: api/column
        [HttpGet]
        public ActionResult<IEnumerable<Column>> GetColumns()
        {
            var columns = _columnService.GetAllColumns();
            return Ok(columns);
        }

        // GET: api/column/5
        [HttpGet("{id}")]
        public ActionResult<Column> GetColumn(int id)
        {
            var column = _columnService.GetColumnById(id);
            if (column == null)
            {
                return NotFound();
            }
            return Ok(column);
        }

        // POST: api/column
        [HttpPost]
        public ActionResult<Column> CreateColumn(Column column)
        {
            var createdColumn = _columnService.AddColumn(column);
            return CreatedAtAction(nameof(GetColumn), new { id = createdColumn.Id }, createdColumn);
        }

        // PUT: api/column/5
        [HttpPut("{id}")]
        public IActionResult UpdateColumn(int id, Column column)
        {
            if (id != column.Id)
            {
                return BadRequest();
            }
            try
            {
                _columnService.UpdateColumn(column);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/column/5
        [HttpDelete("{id}")]
        public IActionResult DeleteColumn(int id)
        {
            try
            {
                _columnService.DeleteColumn(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
