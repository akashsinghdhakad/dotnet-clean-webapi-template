using DotnetWebApiCoreCBA.Models.Entities;

namespace DotnetWebApiCoreCBA.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(int id);
        Task AddAsync(Todo entity);
        void Update(Todo entity);
        void Remove(Todo entity);
        Task SaveChangesAsync();
    }
}
