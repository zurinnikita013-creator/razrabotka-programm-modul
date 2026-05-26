using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer;

public class MSSQLConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection() => new SqlConnection(connectionString);
}