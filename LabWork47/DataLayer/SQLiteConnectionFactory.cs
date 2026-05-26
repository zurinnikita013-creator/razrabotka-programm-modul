using Microsoft.Data.Sqlite;
using System.Data;

namespace DataLayer;

public class SQLiteConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection() => new SqliteConnection(connectionString);
}
