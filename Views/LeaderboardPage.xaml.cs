using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class LeaderboardPage : ContentPage
{
    private readonly ApiService _apiService;

    public LeaderboardPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            var leaderboard = await _apiService.GetLeaderboardAsync();
            LeaderboardList.ItemsSource = leaderboard;
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", $"Failed to load leaderboard: {ex.Message}", "OK");
        }
    }
}