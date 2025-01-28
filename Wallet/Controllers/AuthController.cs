using Microsoft.AspNetCore.Mvc;
using Wallet.DTOs;
using Wallet.Models;

namespace Wallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
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
    }

}
