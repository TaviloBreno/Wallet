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
    public class TransferController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransferController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Criar uma transferência entre usuários.
        /// </summary>
        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateTransfer([FromBody] TransferDto dto)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID is missing from the token.");
            }

            if (!int.TryParse(userIdClaim, out int senderId))
            {
                return Unauthorized("Invalid user ID format.");
            }

            if (dto.Amount <= 0)
            {
                return BadRequest("Transfer amount must be greater than zero.");
            }

            var sender = _context.Users.FirstOrDefault(u => u.Id == senderId);
            var receiver = _context.Users.FirstOrDefault(u => u.Id == dto.ReceiverId);

            if (sender == null || receiver == null)
            {
                return NotFound("Sender or receiver not found.");
            }

            if (sender.WalletBalance < dto.Amount)
            {
                return BadRequest("Insufficient balance for this transfer.");
            }

            // Realizar a transferência
            sender.WalletBalance -= dto.Amount;
            receiver.WalletBalance += dto.Amount;

            // Registrar a transferência no banco de dados
            var transfer = new Transfer
            {
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                Amount = dto.Amount,
                TransferDate = DateTime.UtcNow
            };

            _context.Transfers.Add(transfer);
            _context.SaveChanges();

            return Ok(new { Message = "Transfer completed successfully.", NewBalance = sender.WalletBalance });
        }


        /// <summary>
        /// Listar transferências realizadas por um usuário, com filtro opcional por período de data.
        /// </summary>
        [HttpGet("history")]
        public IActionResult GetTransferHistory([FromQuery] TransferFilterDto filter)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID is missing from the token.");
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("Invalid user ID format.");
            }

            var transfers = _context.Transfers
                .Where(t => t.SenderId == userId)
                .OrderByDescending(t => t.TransferDate)
                .ToList();

            if (filter.StartDate.HasValue)
            {
                transfers = transfers.Where(t => t.TransferDate >= filter.StartDate.Value).ToList();
            }

            if (filter.EndDate.HasValue)
            {
                transfers = transfers.Where(t => t.TransferDate <= filter.EndDate.Value).ToList();
            }

            var transferHistory = transfers.Select(t => new TransferResponseDto
            {
                Amount = t.Amount,
                TransferDate = t.TransferDate,
                ReceiverWallet = _context.Users.FirstOrDefault(u => u.Id == t.ReceiverId)?.Name ?? "Unknown"
            });

            return Ok(transferHistory);
        }
    }
}
