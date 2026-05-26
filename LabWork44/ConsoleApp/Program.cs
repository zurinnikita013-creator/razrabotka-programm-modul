using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace SqliteLab
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbPath = "GamesStore.sqlite";

            CreateDatabase(dbPath);
            CreateTables(dbPath);

            Console.WriteLine("БД SQLite и таблицы созданы");
        }

        static void CreateDatabase(string dbFilePath)
        {
            if (!File.Exists(dbFilePath))
            {
                using (var connection = new SqliteConnection($"Data Source={dbFilePath}"))
                {
                    connection.Open();
                }
            }
        }

        static void CreateTables(string dbFilePath)
        {
            using (var connection = new SqliteConnection($"Data Source={dbFilePath}"))
            {
                connection.Open();

                string createRoles = @"CREATE TABLE IF NOT EXISTS Roles (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL)";

                string createUsers = @"CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    RoleId INTEGER NOT NULL,
                    Login TEXT NOT NULL,
                    Password TEXT NOT NULL,
                    FOREIGN KEY (RoleId) REFERENCES Roles(Id))";

                using (var cmd = new SqliteCommand(createRoles, connection))
                    cmd.ExecuteNonQuery();

                using (var cmd = new SqliteCommand(createUsers, connection))
                    cmd.ExecuteNonQuery();
            }
        }
    }
}