using System.Data;
using Microsoft.Data.SqlClient;
using DotnetWebApiCoreCBA.Models.Entities;
using DotnetWebApiCoreCBA.Repositories.Interfaces;

namespace DotnetWebApiCoreCBA.Repositories.Implementations.Sql;

public class TodoRepositorySql : ITodoRepository
{
    private readonly string _connectionString;

    public TodoRepositorySql(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    private SqlConnection CreateConnection()
        => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
        var items = new List<Todo>();

        using var conn = CreateConnection();
        await conn.OpenAsync();

        using var cmd = new SqlCommand(
            "SELECT Id, Title, IsCompleted FROM Todos",
            conn);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            items.Add(new Todo
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                IsCompleted = reader.GetBoolean(2)
            });
        }

        return items;
    }

    public async Task<Todo?> GetByIdAsync(int id)
    {
        using var conn = CreateConnection();
        await conn.OpenAsync();

        using var cmd = new SqlCommand(
            "SELECT Id, Title, IsCompleted FROM Todos WHERE Id = @Id",
            conn);

        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new Todo
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                IsCompleted = reader.GetBoolean(2)
            };
        }

        return null;
    }

    public async Task AddAsync(Todo entity)
    {
        using var conn = CreateConnection();
        await conn.OpenAsync();

        using var cmd = new SqlCommand(
            @"INSERT INTO Todos (Title, IsCompleted)
              OUTPUT INSERTED.Id
              VALUES (@Title, @IsCompleted);",
            conn);

        cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 200) { Value = entity.Title });
        cmd.Parameters.Add(new SqlParameter("@IsCompleted", SqlDbType.Bit) { Value = entity.IsCompleted });

        var result = await cmd.ExecuteScalarAsync();
        var insertedId = Convert.ToInt32(result);
        entity.Id = insertedId;
    }

    public Task UpdateAsync(Todo entity)
    {
        // We will execute SQL directly here, so make this async-friendly using SaveChangesAsync
        // This method will be used only to hold modified entity in other implementations.
        // For SQL implementation, we'll do nothing here and perform update in SaveChangesAsync
        // OR we can choose to perform update directly here.
        // To keep API contract, we'll execute SQL here synchronously but wrap in Task in SaveChangesAsync.
        throw new NotSupportedException("Call UpdateAsyncSql instead for SQL implementation.");
    }

    public Task RemoveAsync(int id)
    {
        // Same idea as Update
        throw new NotSupportedException("Call RemoveAsyncSql instead for SQL implementation.");
    }

    // SQL-specific helpers (optional) – or you can change the interface if you want.
    public async Task UpdateAsyncSql(Todo entity)
    {
        using var conn = CreateConnection();
        await conn.OpenAsync();

        using var cmd = new SqlCommand(
            @"UPDATE Todos
              SET Title = @Title,
                  IsCompleted = @IsCompleted
              WHERE Id = @Id;",
            conn);

        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id });
        cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 200) { Value = entity.Title });
        cmd.Parameters.Add(new SqlParameter("@IsCompleted", SqlDbType.Bit) { Value = entity.IsCompleted });

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task RemoveAsyncSql(int id)
    {
        using var conn = CreateConnection();
        await conn.OpenAsync();

        using var cmd = new SqlCommand(
            "DELETE FROM Todos WHERE Id = @Id;",
            conn);

        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

        await cmd.ExecuteNonQueryAsync();
    }

    public Task SaveChangesAsync()
    {
        // For raw SQL we execute immediately, so nothing to save
        return Task.CompletedTask;
    }
}
