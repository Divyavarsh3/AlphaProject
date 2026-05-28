
using Alpha.Common.Models;

using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

using System.Text;

namespace Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            string role = "";

            // Admin Login
            if (request.Email == "admin@gmail.com"
                && request.Password == "123456")
            {
                role = "Admin";
            }

            // User Login
            else if (request.Email == "user@gmail.com"
                && request.Password == "123456")
            {
                role = "User";
            }

            else
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, request.Email),

                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],

                audience: _configuration["Jwt:Audience"],

                claims: claims,

                expires: DateTime.Now.AddMinutes(60),

                signingCredentials: credentials
            );

            var jwtToken =
            new JwtSecurityTokenHandler()
            .WriteToken(token);

            return Ok(jwtToken);
        }
    }
}

