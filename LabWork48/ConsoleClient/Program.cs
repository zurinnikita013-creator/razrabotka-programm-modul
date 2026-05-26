using API.Services;
class Program
{
    static async Task Main(string[] args)
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7000/"); // ваш URL WebAPI

        var service = new GamesApiService(httpClient);

        try
        {
            Console.WriteLine("=== Получение всех игр ===");
            var games = await service.GetGamesAsync();
            if (games != null)
            {
                foreach (var game in games)
                {
                    Console.WriteLine($"Id: {game.Id}, Name: {game.Name}, Price: {game.Price}");
                }
            }

            Console.WriteLine("\n=== Получение игры с Id=1 ===");
            var game1 = await service.GetGameByIdAsync(1);
            if (game1 != null)
                Console.WriteLine($"Id: {game1.Id}, Name: {game1.Name}, Description: {game1.Description}");

            Console.WriteLine("\n=== Создание новой игры ===");
            var newGame = new Game
            {
                Name = "Новая игра",
                Price = 999,
                Description = "Описание",
                Genre = "Action",
                Publisher = "Publisher",
                ReleaseDate = DateTime.Now
            };
            var created = await service.CreateGameAsync(newGame);
            if (created != null)
                Console.WriteLine($"Создана игра с Id: {created.Id}");

            Console.WriteLine("\n=== Обновление игры ===");
            if (created != null)
            {
                created.Price = 1499;
                var updated = await service.UpdateGameAsync(created.Id, created);
                Console.WriteLine(updated ? "Обновлено" : "Ошибка");
            }

            Console.WriteLine("\n=== Удаление игры ===");
            if (created != null)
            {
                var deleted = await service.DeleteGameAsync(created.Id);
                Console.WriteLine(deleted ? "Удалено" : "Ошибка");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}