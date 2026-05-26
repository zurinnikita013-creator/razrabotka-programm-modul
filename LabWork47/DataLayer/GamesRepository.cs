using Dapper;
using System.Data;

namespace DataLayer;

public class GamesRepository(IDbConnectionFactory factory)
{
    private readonly IDbConnection _connection = factory.CreateConnection();

    public IEnumerable<Game> GetAll() =>
        _connection.Query<Game>("SELECT * FROM Game");

    public Game? GetById(int id) =>
        _connection.QueryFirstOrDefault<Game>("SELECT * FROM Game WHERE Id = @Id", new { Id = id });

    public void Create(Game game) =>
        _connection.Execute("INSERT INTO Game (Title, Genre, Year) VALUES (@Title, @Genre, @Year)", game);

    public void Update(Game game) =>
        _connection.Execute("UPDATE Game SET Title = @Title, Genre = @Genre, Year = @Year WHERE Id = @Id", game);

    public void Delete(int id) =>
        _connection.Execute("DELETE FROM Game WHERE Id = @Id", new { Id = id });
}