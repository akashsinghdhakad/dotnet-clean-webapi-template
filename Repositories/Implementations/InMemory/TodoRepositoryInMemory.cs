using DotnetWebApiCoreCBA.Models.Entities;
using DotnetWebApiCoreCBA.Repositories.Interfaces;

namespace DotnetWebApiCoreCBA.Repositories.Implementations.InMemory
{
    public class TodoRepositoryInMemory : ITodoRepository
    {
        private readonly List<Todo> _items = new();
        private int _nextId = 1;

        public Task<IEnumerable<Todo>> GetAllAsync()
            => Task.FromResult(_items.AsEnumerable());

        public Task<Todo?> GetByIdAsync(int id)
            => Task.FromResult(_items.FirstOrDefault(x => x.Id == id));

        public Task AddAsync(Todo entity)
        {
            entity.Id = _nextId++;
            _items.Add(entity);
            return Task.CompletedTask;
        }

        public void Update(Todo entity)
        {
            // nothing special for in-memory list
        }

        public void Remove(Todo entity) => _items.Remove(entity);

        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}
