using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Persistence.Context;

namespace TaskManagement.Infrastructure.Repository
{
    internal class TaskService(ApplicationDbContext dbContext) : ITaskService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<bool> CreateAsync(Domain.Entities.Persistence.Task task)
        {
            _dbContext.Add(task);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Domain.Entities.Persistence.Task task)
        {
            _dbContext.Tasks.Remove(task);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Domain.Entities.Persistence.Task>> GetAsync(string userId)
        {
            return await _dbContext.Tasks.Where(x => x.CreatedBy.Equals(userId)).ToListAsync();
        }

        public async Task<Domain.Entities.Persistence.Task> GetByIdAsync(int id, string userId)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id && x.CreatedBy.Equals(userId));
        }

        public async Task<bool> UpdateAsync(Domain.Entities.Persistence.Task task)
        {
            return await _dbContext.SaveChangesAsync() > 0; // Note: Si no existe actualización, retorna un badrequest
        }
    }
}
