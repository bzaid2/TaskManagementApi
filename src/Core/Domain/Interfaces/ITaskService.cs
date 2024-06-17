namespace TaskManagement.Domain.Interfaces
{
    public interface ITaskService
    {
        Task<Entities.Persistence.Task> GetByIdAsync(int id, string userId);
        Task<IEnumerable<Entities.Persistence.Task>> GetAsync(string userId);
        Task<bool> CreateAsync(Entities.Persistence.Task task);
        Task<bool> UpdateAsync(Entities.Persistence.Task task);
        Task<bool> DeleteAsync(Entities.Persistence.Task task);
    }
}
