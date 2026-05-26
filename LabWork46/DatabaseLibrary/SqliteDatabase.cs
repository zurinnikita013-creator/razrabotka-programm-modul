using Microsoft.Data.Sqlite;

namespace DatabaseLibrary
{
    public class SqliteDatabase : IDatabase
    {
        private readonly string _connectionString;

        public SqliteDatabase(string folderPath, string dbFileName)
        {
            var builder = new SqliteConnectionStringBuilder
            {
                DataSource = Path.Combine(folderPath, dbFileName)
            };
            _connectionString = builder.ToString();
        }

        public int ExecuteQuery(string sqlCommand)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            using var cmd = new SqliteCommand(sqlCommand, conn);
            return cmd.ExecuteNonQuery();
        }

        public bool UpdateGame(int id, string newName, decimal newPrice)
        {
            string sql = $"UPDATE Games SET Name = '{newName}', Price = {newPrice} WHERE Id = {id}";
            return ExecuteQuery(sql) == 1;
        }

        public bool InsertGame(string name, decimal price, int year)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            string sql = "INSERT INTO Games (Name, Price, Year) VALUES (@name, @price, @year)";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@year", year);
            return cmd.ExecuteNonQuery() == 1;
        }
    }
}