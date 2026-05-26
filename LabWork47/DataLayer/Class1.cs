using DataLayer;

var factory = new SQLiteConnectionFactory("Data Source=games.db");
var repo = new GamesRepository(factory);

// CREATE
repo.Create(new Game { Title = "StarCraft", Genre = "RTS", Year = 1998 });

// READ ALL
foreach (var g in repo.GetAll())
    Console.WriteLine($"{g.Id}: {g.Title} ({g.Year})");

// READ BY ID
var game = repo.GetById(1);
Console.WriteLine($"Found: {game?.Title}");

// UPDATE
if (game != null)
{
    game.Genre = "Strategy";
    repo.Update(game);
}

// DELETE
repo.Delete(1);