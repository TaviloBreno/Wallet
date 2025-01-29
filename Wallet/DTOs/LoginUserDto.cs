using Microsoft.AspNetCore.Mvc;

namespace Wallet.DTOs
{
    public class LoginUserDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
