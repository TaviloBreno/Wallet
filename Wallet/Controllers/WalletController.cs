using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wallet.DTOs;
using Wallet.Models;

namespace Wallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WalletController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém o saldo da carteira do usuário autenticado.
        /// </summary>
        [HttpGet("balance")]
        public IActionResult GetWalletBalance()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == int.Parse(userId));
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(new { Balance = user.WalletBalance });
        }

        /// <summary>
        /// Adiciona saldo à carteira do usuário autenticado.
        /// </summary>
        [HttpPost("add-balance")]
        public IActionResult AddBalance([FromBody] AddBalanceDto dto)
        {
            if (dto.Amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == int.Parse(userId));
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.WalletBalance += dto.Amount;
            _context.SaveChanges();

            return Ok(new { Message = "Balance added successfully.", NewBalance = user.WalletBalance });
        }
    }
}
