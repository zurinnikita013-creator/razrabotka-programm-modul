using System.Data;

namespace DataLayer;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}