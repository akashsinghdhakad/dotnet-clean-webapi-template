using dotnetWebApiCoreCBA.Data;
using dotnetWebApiCoreCBA.Repositories.Implementations.EfCore;
using dotnetWebApiCoreCBA.Repositories.Implementations.InMemory;
using dotnetWebApiCoreCBA.Repositories.Implementations.Sql;
using dotnetWebApiCoreCBA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnetWebApiCoreCBA.Configuration;

public static class PersistenceConfig
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var repoMode = configuration["RepositoryMode"] ?? "Ef";

        if (repoMode == "Ef")
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITodoRepository, TodoRepositoryEf>();
            services.AddScoped<IUserRepository, UserRepositoryEf>();
        }
        else if (repoMode == "Sql")
        {
            // no DbContext; use plain ADO.NET / Dapper style repository
            services.AddScoped<ITodoRepository, TodoRepositorySql>();
            // you could add Sql-based IUserRepository too if needed
        }
        else // InMemory (default for tests if you want)
        {
            services.AddScoped<ITodoRepository, TodoRepositoryInMemory>();
        }

        return services;
    }
}
