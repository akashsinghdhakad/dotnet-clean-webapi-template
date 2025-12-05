using DotnetWebApiCoreCBA.Models.DTOs.Todo;
using DotnetWebApiCoreCBA.Models.Entities;
using DotnetWebApiCoreCBA.Repositories.Interfaces;
using DotnetWebApiCoreCBA.Services.Interfaces;

namespace DotnetWebApiCoreCBA.Services.Implementations
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repo;

        public TodoService(ITodoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TodoResponse>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(x => new TodoResponse
            {
                Id = x.Id,
                Title = x.Title,
                IsCompleted = x.IsCompleted
            });
        }

        public async Task<TodoResponse?> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return null;

            return new TodoResponse
            {
                Id = item.Id,
                Title = item.Title,
                IsCompleted = item.IsCompleted
            };
        }

        public async Task<TodoResponse> CreateAsync(TodoCreateRequest request)
        {
            var entity = new Todo
            {
                Title = request.Title,
                IsCompleted = false
            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return new TodoResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                IsCompleted = entity.IsCompleted
            };
        }

        public async Task<TodoResponse?> UpdateAsync(int id, TodoCreateRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;

            entity.Title = request.Title;

            _repo.Update(entity);
            await _repo.SaveChangesAsync();

            return new TodoResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                IsCompleted = entity.IsCompleted
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
