using Microsoft.AspNetCore.Mvc;
using OralData.Backend.Data;
using OralData.Backend.Interfaces;
using OralData.Shared.Entities;
using Orders.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Helpers;


namespace OralData.Backend.Controllers
{

    public class GenericController<T> : Controller where T : class
    {
        private readonly IGenericUnitOfWork<T> _unitOfWork;
        private readonly DbSet<T> _entity;

        public GenericController(IGenericUnitOfWork<T> unitOfWork, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _entity = context.Set<T>();
        }


        [HttpGet("totalPages")]
        public virtual async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _entity.AsQueryable();
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _entity.AsQueryable();
            return Ok(await queryable
                .Paginate(pagination)
                .ToListAsync());

        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(int id)
        {
            var action = await _unitOfWork.GetAsync(id);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound();
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync(T model)
        {
            var action = await _unitOfWork.AddAsync(model);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPut]
        public virtual async Task<IActionResult> PutAsync(T model)
        {
            var action = await _unitOfWork.UpdateAsync(model);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            var action = await _unitOfWork.GetAsync(id);
            if (!action.WasSuccess)
            {
                return NotFound();
            }
            action = await _unitOfWork.DeleteAsync(id);
            if (!action.WasSuccess)
            {
                return BadRequest(action.Message);
            }
            return NoContent();
        }
    }
}