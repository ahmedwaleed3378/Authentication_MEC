﻿namespace Authentication.Models.DTOs
{
	public class RegisterDto
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Role { get; set; } // Admin or Reader
	}
}
