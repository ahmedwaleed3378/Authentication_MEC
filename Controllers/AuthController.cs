using Authentication.Models;
using Authentication.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IConfiguration _configuration;

	public AuthController(UserManager<ApplicationUser> userManager,
		RoleManager<IdentityRole> roleManager,
		IConfiguration configuration)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_configuration = configuration;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register(RegisterDto dto)
	{
		var user = new ApplicationUser { UserName = dto.Username };
		var result = await _userManager.CreateAsync(user, dto.Password);

		if (!result.Succeeded)
			return BadRequest(result.Errors);

		if (!await _roleManager.RoleExistsAsync(dto.Role))
			await _roleManager.CreateAsync(new IdentityRole(dto.Role));

		await _userManager.AddToRoleAsync(user, dto.Role);

		return Ok("User registered");
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginDto dto)
	{
		var user = await _userManager.FindByNameAsync(dto.Username);
		if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
			return Unauthorized();

		var roles = await _userManager.GetRolesAsync(user);

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

		var jwtSettings = _configuration.GetSection("Jwt");
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: jwtSettings["Issuer"],
			audience: jwtSettings["Audience"],
			claims: claims,
			//expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"])),
			signingCredentials: creds
		);

		return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), 
		
		ID = user.Id,
		});
	}
}
