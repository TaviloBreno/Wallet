namespace Wallet.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Models.User user);
    }
}
