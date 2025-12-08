using dotnetWebApiCoreCBA.Models.Entities;

namespace dotnetWebApiCoreCBA.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(int id);
        Task AddAsync(Todo entity);
        Task UpdateAsync(Todo entity);
        Task RemoveAsync(int id);
        Task SaveChangesAsync();
    }
}
