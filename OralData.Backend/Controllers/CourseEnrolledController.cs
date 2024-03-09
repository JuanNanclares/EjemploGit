using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OralData.Backend.Controllers;
using OralData.Backend.Data;
using OralData.Backend.Interfaces;
using OralData.Backend.UnitsOfWork;
using OralData.Shared.Entities;

namespace OralData.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class CourseEnrolledController : GenericController<CourseEnrolled>
    {
        private readonly DataContext _context;
        public CourseEnrolledController(IGenericUnitOfWork<CourseEnrolled> unitOfWork, DataContext context) : base(unitOfWork, context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _context.CoursesEnrolled.ToListAsync());
        }
    }
}