namespace TaskManagement.Domain.Entities.Dtos
{
    public record UserResponse
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
    }
}
