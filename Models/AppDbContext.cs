using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Models
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
		{
		}


		public DbSet<Book> Books { get; set; }
		public DbSet<Department> Departments { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Sample config (optional)
			builder.Entity<Department>()
				   .HasIndex(d => d.Name)
				   .IsUnique();

			builder.Entity<Book>()
				   .HasOne(b => b.Department)
				   .WithMany(d => d.Books)
				   .HasForeignKey(b => b.DepartmentId);

			builder.Entity<Book>()
				   .HasOne(b => b.Borrower)
				   .WithMany(u => u.BorrowedBooks)
				   .HasForeignKey(b => b.BorrowerId)
				   .OnDelete(DeleteBehavior.SetNull);
		}
	}
}
