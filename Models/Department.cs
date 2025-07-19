namespace Authentication.Models
{
	public class Department
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		// Navigation
		public ICollection<Book>? Books { get; set; }
		public ICollection<ApplicationUser>? Users { get; set; }
	}

}
