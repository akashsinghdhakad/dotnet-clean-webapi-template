using System.Data;
using dotnetWebApiCoreCBA.Common.Database;
using dotnetWebApiCoreCBA.Models.Entities;
using dotnetWebApiCoreCBA.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace dotnetWebApiCoreCBA.Repositories.Implementations.Sql;

public class UserRepositorySql : IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    private readonly ILogger<UserRepositorySql> _logger;

    public UserRepositorySql(IDbConnectionFactory connectionFactor, ILogger<UserRepositorySql> logger)
    {
        _connectionFactory = connectionFactor;
        _logger = logger;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        const string sql = @"
            SELECT TOP 1 Id, Username, PasswordHash, PasswordSalt, Role
            FROM Users
            WHERE Username = @Username;";

        await using var conn = _connectionFactory.Create();
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 256) { Value = username });

        await conn.OpenAsync();

        await using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return null;
        }

        var user = new User
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Username = reader.GetString(reader.GetOrdinal("Username")),
            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
            PasswordSalt = reader.GetString(reader.GetOrdinal("PasswordSalt")),
            Role = reader.GetString(reader.GetOrdinal("Role"))
        };

        return user;
    }

    public async Task<User> CreateAsync(User user)
    {
        const string sql = @"
            INSERT INTO Users (Username, PasswordHash, PasswordSalt, Role, CreatedAt)
            OUTPUT INSERTED.Id
            VALUES (@Username, @PasswordHash, @PasswordSalt, @Role, @CreatedAt);";

        await using var conn = _connectionFactory.Create();
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 256) { Value = user.Username });
        cmd.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 512) { Value = user.PasswordHash });
        cmd.Parameters.Add(new SqlParameter("@PasswordSalt", SqlDbType.NVarChar, 512) { Value = user.PasswordSalt });
        cmd.Parameters.Add(new SqlParameter("@Role", SqlDbType.NVarChar, 50) { Value = user.Role });
        cmd.Parameters.Add(new SqlParameter("@CreatedAt", SqlDbType.DateTime) { Value = DateTime.UtcNow });


        await conn.OpenAsync();

        var insertedIdObj = await cmd.ExecuteScalarAsync();
        if (insertedIdObj is int insertedId)
        {
            user.Id = insertedId;
        }
        else
        {
            _logger.LogWarning("UserRepositorySql: Inserted user but could not read new Id for username {Username}",
                user.Username);
        }

        return user;
    }

    public Task<User?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
