using Microsoft.EntityFrameworkCore;
using DotnetWebApiCoreCBA.Data;
using DotnetWebApiCoreCBA.Models.Entities;
using DotnetWebApiCoreCBA.Repositories.Interfaces;

namespace DotnetWebApiCoreCBA.Repositories.Implementations.EfCore
{
    public class TodoRepositoryEf : ITodoRepository
    {
        private readonly AppDbContext _db;

        public TodoRepositoryEf(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
            => await _db.Todos.AsNoTracking().ToListAsync();

        public async Task<Todo?> GetByIdAsync(int id)
            => await _db.Todos.FindAsync(id);

        public async Task AddAsync(Todo entity)
            => await _db.Todos.AddAsync(entity);

        public void Update(Todo entity)
            => _db.Todos.Update(entity);

        public void Remove(Todo entity)
            => _db.Todos.Remove(entity);

        public async Task SaveChangesAsync()
            => await _db.SaveChangesAsync();
    }
}
