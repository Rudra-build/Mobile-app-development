using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class LeaderboardPage : ContentPage
{
    public LeaderboardPage()
    {
        InitializeComponent();

        var userState = new UserStateService();
        var leaderboardService = new LeaderboardService();

        string name = Preferences.Get("username", "You");
        int points = userState.GetPoints();

        LeaderboardList.ItemsSource =
            leaderboardService.GetLeaderboard(points, name);
    }
}