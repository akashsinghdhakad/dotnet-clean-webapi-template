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

        public Task UpdateAsync(Todo entity)
        {
            // nothing special for in-memory list
            return Task.CompletedTask;

        }

        public Task RemoveAsync(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item != null) _items.Remove(item);
            return Task.CompletedTask;
        }


        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}
