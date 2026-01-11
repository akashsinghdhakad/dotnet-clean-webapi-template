using Microsoft.Data.SqlClient;

namespace dotnetWebApiCoreCBA.Common.Database;

public class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public SqlConnection Create()
    {
        return new SqlConnection(_connectionString);
    }
}
