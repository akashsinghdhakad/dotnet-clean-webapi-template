using DotnetWebApiCoreCBA.Models.DTOs.Todo;

namespace DotnetWebApiCoreCBA.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoResponse>> GetAllAsync();
        Task<TodoResponse?> GetByIdAsync(int id);
        Task<TodoResponse> CreateAsync(TodoCreateRequest request);
        Task<TodoResponse?> UpdateAsync(int id, TodoCreateRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
