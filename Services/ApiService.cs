using System.Net.Http.Json;
using CodelingoApp.Models;

namespace CodelingoApp.Services;

public class ApiService
{
    private const string BaseUrl = "http://192.168.0.69:5257";

    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(BaseUrl)
    };

    public async Task<List<ApiLeaderboardEntry>> GetLeaderboardAsync()
    {
        var data = await _httpClient.GetFromJsonAsync<List<ApiLeaderboardEntry>>("/leaderboard");
        return data ?? new List<ApiLeaderboardEntry>();
    }

    public async Task<ApiSubscription?> GetSubscriptionAsync(int userId)
    {
        return await _httpClient.GetFromJsonAsync<ApiSubscription>($"/subscription/{userId}");
    }

    public async Task<bool> UpgradeSubscriptionAsync(int userId)
    {
        var response = await _httpClient.PostAsync($"/subscription/upgrade/{userId}", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SaveResultAsync(ApiResultRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/results", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<ApiUser?> GetUserAsync(int userId)
    {
        return await _httpClient.GetFromJsonAsync<ApiUser>($"/users/{userId}");
    }

    public async Task<bool> UpdateUserAsync(int userId, ApiUser user)
    {
        var response = await _httpClient.PutAsJsonAsync($"/users/{userId}", user);
        return response.IsSuccessStatusCode;
    }

    public async Task<ApiUser?> RegisterAsync(ApiUser user)
    {
        var response = await _httpClient.PostAsJsonAsync("/users/register", user);
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<ApiUser>();
    }

    public async Task<ApiUser?> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/users/login", request);
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<ApiUser>();
    }

    public async Task<List<AiQuizQuestion>> GenerateAiQuizAsync(string category, string difficulty)
    {
        var request = new
        {
            category = "Programming",
            difficulty,
            questionCount = 5
        };

        var response = await _httpClient.PostAsJsonAsync("/ai/generate", request);
        var raw = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return new List<AiQuizQuestion>();

        var options = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // case 1: backend returns a plain array
        if (raw.TrimStart().StartsWith("["))
        {
            var list = System.Text.Json.JsonSerializer.Deserialize<List<AiQuizQuestion>>(raw, options);
            return list ?? new List<AiQuizQuestion>();
        }

        // case 2: backend returns { "questions": [...] }
        if (raw.TrimStart().StartsWith("{"))
        {
            using var doc = System.Text.Json.JsonDocument.Parse(raw);

            if (doc.RootElement.TryGetProperty("questions", out var questionsElement))
            {
                var list = System.Text.Json.JsonSerializer.Deserialize<List<AiQuizQuestion>>(
                    questionsElement.GetRawText(), options);

                return list ?? new List<AiQuizQuestion>();
            }
        }

        return new List<AiQuizQuestion>();
    }

    public async Task<bool> DowngradeSubscriptionAsync(int userId)
    {
        var response = await _httpClient.PostAsync($"/subscription/downgrade/{userId}", null);
        return response.IsSuccessStatusCode;
    }


    public async Task<ApiAnalytics?> GetAnalyticsAsync(int userId)
    {
        return await _httpClient.GetFromJsonAsync<ApiAnalytics>($"/analytics/{userId}");
    }

    public async Task<string?> CreateStripeCheckoutSessionAsync(int userId)
    {
        var response = await _httpClient.PostAsync($"/stripe/create-checkout-session/{userId}", null);
        if (!response.IsSuccessStatusCode) return null;

        var raw = await response.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(raw);

        if (doc.RootElement.TryGetProperty("url", out var urlElement))
            return urlElement.GetString();

        return null;
    }
}