using Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		private readonly AppDbContext _context;

		public HomeController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Order
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Department>>> GetOrders()
		{
			return await _context.Departments
				.ToListAsync();
		}
	}
}
