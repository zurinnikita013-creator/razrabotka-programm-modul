using System.Text;
using System.Text.Json;

namespace API.Services;

public class GamesApiService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    // GET все игры
    public async Task<List<Game>?> GetGamesAsync()
    {
        var response = await _httpClient.GetAsync("api/game");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Game>>(json);
    }

    // GET игра по id
    public async Task<Game?> GetGameByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/game/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Game>(json);
    }

    // POST создание игры
    public async Task<Game?> CreateGameAsync(Game game)
    {
        var json = JsonSerializer.Serialize(game);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/game", content);
        response.EnsureSuccessStatusCode();
        var resultJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Game>(resultJson);
    }

    // PUT обновление игры
    public async Task<bool> UpdateGameAsync(int id, Game game)
    {
        var json = JsonSerializer.Serialize(game);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/game/{id}", content);
        return response.IsSuccessStatusCode;
    }

    // DELETE удаление игры
    public async Task<bool> DeleteGameAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/game/{id}");
        return response.IsSuccessStatusCode;
    }
}

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
}