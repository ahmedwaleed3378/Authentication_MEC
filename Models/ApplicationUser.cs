using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? FullName { get; set; }

		// Optional: link to department (if users belong to a department)
		public int? DepartmentId { get; set; }
		public Department? Department { get; set; }

		// Navigation for books borrowed (optional)
		public ICollection<Book>? BorrowedBooks { get; set; }
	}

}
