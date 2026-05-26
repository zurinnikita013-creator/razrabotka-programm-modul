using System;
using Microsoft.Data.SqlClient;

namespace MsSqlLab
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=mssql;Database=YourDB;User Id=isppXXNN;Password=XXNN;";

            CreateTables(connectionString);

            Console.WriteLine("Таблицы MSSQL созданы");
        }

        static void CreateTables(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Таблица Roles
                string createRoles = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Roles' AND xtype='U')
                    CREATE TABLE Roles (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(50) NOT NULL
                    )";

                // Таблица Users
                string createUsers = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
                    CREATE TABLE Users (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        RoleId INT NOT NULL,
                        Login NVARCHAR(50) NOT NULL,
                        Password NVARCHAR(50) NOT NULL,
                        FOREIGN KEY (RoleId) REFERENCES Roles(Id)
                    )";

                using (var cmd = new SqlCommand(createRoles, connection))
                    cmd.ExecuteNonQuery();

                using (var cmd = new SqlCommand(createUsers, connection))
                    cmd.ExecuteNonQuery();
            }
        }
    }
}