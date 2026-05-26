using Microsoft.Data.SqlClient;

namespace DatabaseLibrary
{
    public class SqlDatabase : IDatabase
    {
        private readonly string _connectionString;

        public SqlDatabase(string server, string database, string user, string password)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = database,
                UserID = user,
                Password = password,
                TrustServerCertificate = true
            };
            _connectionString = builder.ToString();
        }

        public int ExecuteQuery(string sqlCommand)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand(sqlCommand, conn);
            return cmd.ExecuteNonQuery();
        }

        public bool UpdateGame(int id, string newName, decimal newPrice)
        {
            string sql = $"UPDATE Games SET Name = '{newName}', Price = {newPrice} WHERE Id = {id}";
            return ExecuteQuery(sql) == 1;
        }

        public bool InsertGame(string name, decimal price, int year)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = "INSERT INTO Games (Name, Price, Year) VALUES (@name, @price, @year)";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@year", year);
            return cmd.ExecuteNonQuery() == 1;
        }
    }
}