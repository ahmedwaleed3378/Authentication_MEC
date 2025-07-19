namespace Authentication.Models
{
	public class Book
	{
		public int Id { get; set; }

		public string Title { get; set; } = null!;

		public string Author { get; set; } = null!;

		public DateTime PublishedDate { get; set; }

		public string? ISBN { get; set; }

		// Department relationship
		public int DepartmentId { get; set; }
		public Department Department { get; set; } = null!;

		// Optional: track who borrowed the book
		public string? BorrowerId { get; set; }
		public ApplicationUser? Borrower { get; set; }
	}

}
