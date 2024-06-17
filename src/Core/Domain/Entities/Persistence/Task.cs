namespace TaskManagement.Domain.Entities.Persistence
{
    public record Task
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsChecked { get; set; }
        public string? Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
