namespace TaskManagement.Domain.Entities.Dtos
{
    public record TokenResponse
    {
        public string? AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string TokenType { get; set; } = "Bearer";
    }
}
