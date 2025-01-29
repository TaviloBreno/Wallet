using Microsoft.AspNetCore.Mvc;
using Wallet.DTOs;
using Wallet.Models;
using Wallet.Services;
using System.Linq;
using Wallet.Interfaces;

namespace Wallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterUserDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
            {
                return BadRequest("Email já registrado.");
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Usuário registrado com sucesso!");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new { Token = token });
        }
    }

}
