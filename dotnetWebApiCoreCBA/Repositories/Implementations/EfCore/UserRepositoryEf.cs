using Microsoft.EntityFrameworkCore;
using dotnetWebApiCoreCBA.Data;
using dotnetWebApiCoreCBA.Models.Entities;
using dotnetWebApiCoreCBA.Repositories.Interfaces;

namespace dotnetWebApiCoreCBA.Repositories.Implementations.EfCore;

public class UserRepositoryEf : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepositoryEf(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> CreateAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
}
