namespace TaskManagement.Domain.Interfaces
{
    public interface ITokenService
    {
        public DateTime Expiration { get; }
        Task<string> GetTokenAsync(string userName, string password, CancellationToken cancellationToken);
    }
}
