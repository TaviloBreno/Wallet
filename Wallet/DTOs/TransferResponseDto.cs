namespace Wallet.DTOs
{
    public class TransferResponseDto
    {
        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; }
        public string? ReceiverWallet { get; set; }
    }
}
