using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagementApp.Api.Models;
using TaskManagementApp.Api.Services;

namespace TaskManagementApp.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly IColumnService _columnService;

        public ColumnController(IColumnService columnService)
        {
            _columnService = columnService ?? throw new ArgumentNullException(nameof(columnService));
        }

        /// <summary>
        /// GET: api/column
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Column>> GetColumns()
        {
            var columns = _columnService.GetAllColumns();
            return Ok(columns);
        }

        /// <summary>
        /// GET: api/column/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Column> GetColumn(int id)
        {
            var column = _columnService.GetColumnById(id);

            if (column == null)
            {
                return NotFound();
            }

            column.Tasks = _columnService.SortTaskByNameAscending(column).ToList();

            return Ok(column);
        }

        /// <summary>
        /// GET: api/column/5?sortOrder=asc
        /// </summary>
        /// <param name="columnId"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SortTasksByColumn(int columnId, [FromQuery] string sortOrder)
        {
            var column = _columnService.GetColumnById(columnId);
            if (column == null)
            {
                return NotFound();
            }

            string sortType = sortOrder.ToLower();
            switch (sortType)
            {
                case "asc": _columnService.SortTaskByNameAscending(column).ToList();
                    break;
                case "desc": _columnService.SortTaskByNameDescending(column).ToList();
                    break;
                default:
                    break;

            }

            return Ok(column);
        }

        /// <summary>
        /// POST: api/column
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Column> CreateColumn(Column column)
        {
            var createdColumn = _columnService.AddColumn(column);
            return CreatedAtAction(nameof(GetColumn), new { id = createdColumn.Id }, createdColumn);
        }

        /// <summary>
        /// PUT: api/column/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="column"></param>
        /// <returns></returns>
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

        /// <summary>
        /// DELETE: api/column/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
