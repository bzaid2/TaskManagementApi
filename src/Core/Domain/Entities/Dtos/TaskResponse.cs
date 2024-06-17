namespace TaskManagement.Domain.Entities.Dtos
{
    public record TaskResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsChecked { get; set; }
        public string? Description { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
