using Microsoft.EntityFrameworkCore;
using dotnetWebApiCoreCBA.Data;
using dotnetWebApiCoreCBA.Models.Entities;
using dotnetWebApiCoreCBA.Repositories.Interfaces;

namespace dotnetWebApiCoreCBA.Repositories.Implementations.EfCore
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

        public Task UpdateAsync(Todo entity)
        {
            _db.Todos.Update(entity);
            return Task.CompletedTask;
        }

        public async Task RemoveAsync(int id)
        {
            var item = await _db.Todos.FindAsync(id);
            if (item != null)
            {
                _db.Todos.Remove(item);
            }
        }


        public async Task SaveChangesAsync()
            => await _db.SaveChangesAsync();
    }
}
