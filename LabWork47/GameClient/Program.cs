using DataLayer;

var factory = new SQLiteConnectionFactory("Data Source=games.db");
var repo = new GamesRepository(factory);

// CREATE
repo.Create(new Game { Title = "Half-Life", Genre = "FPS", Year = 1998 });

// GET ALL
Console.WriteLine("All games:");
foreach (var g in repo.GetAll())
    Console.WriteLine($"  {g.Id}: {g.Title} ({g.Year})");

// GET BY ID
var game = repo.GetById(1);
Console.WriteLine($"\nGame with Id=1: {game?.Title}");

// UPDATE
if (game != null)
{
    game.Genre = "Action";
    repo.Update(game);
    Console.WriteLine("Updated genre to Action");
}

// DELETE
repo.Delete(1);
Console.WriteLine("Deleted game with Id=1");