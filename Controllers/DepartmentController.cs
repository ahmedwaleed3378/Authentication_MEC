using Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private readonly AppDbContext _context;

		public DepartmentController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Order
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<IEnumerable<Department>>> GetOrders()
		{
			return await _context.Departments
				.ToListAsync();
		}

		[HttpGet("User")]
		[Authorize(Roles = "User")]
		public async Task<ActionResult<IEnumerable<Department>>> GetDepartmentsForUser()
		{
			return await _context.Departments.Where(d => d.Name != "Fiction").ToListAsync();
		}

	}
}
