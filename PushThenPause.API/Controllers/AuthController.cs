using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PushThenPause.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _users;
        private readonly IConfiguration _config;

        public AuthController(UserManager<IdentityUser> users, IConfiguration config)
        {
            _users = users;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            IdentityUser user = new IdentityUser { UserName = registerDto.Email, Email = registerDto.Email };
            IdentityResult result = await _users
                .CreateAsync(user, registerDto.Password);
            return !result.Succeeded ? BadRequest(result.Errors) : Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            IdentityUser? user = await _users.FindByEmailAsync(loginDto.Email);
            if (user is null || !await _users.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            SymmetricSecurityKey? key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: null,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}

public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
