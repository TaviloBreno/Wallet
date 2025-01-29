namespace Wallet.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; } = DateTime.UtcNow;

        public User? Sender { get; set; }
        public User? Receiver { get; set; }
    }
}
