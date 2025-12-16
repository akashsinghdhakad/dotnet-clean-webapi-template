using Microsoft.Data.SqlClient;

namespace dotnetWebApiCoreCBA.Common.Database;

public interface IDbConnectionFactory
{
    SqlConnection Create();
}
