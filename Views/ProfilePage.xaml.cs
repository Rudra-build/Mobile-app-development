using CodelingoApp.Models;
using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class ProfilePage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly UserStateService _userState;

    public ProfilePage()
    {
        InitializeComponent();

        _apiService = new ApiService();
        _userState = new UserStateService();

        NameEntry.Text = Preferences.Get("username", "");

        PointsLabel.Text = $"Points: {_userState.GetPoints()}";
        StreakLabel.Text = $"Streak: {_userState.GetStreak()} days";
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        string username = NameEntry.Text ?? "User";
        Preferences.Set("username", username);

        try
        {
            int currentUserId = _userState.GetCurrentUserId();
            if (currentUserId > 0)
            {
                var existingUser = await _apiService.GetUserAsync(currentUserId);

                if (existingUser != null)
                {
                    existingUser.Username = username;
                    existingUser.Points = _userState.GetPoints();
                    existingUser.Streak = _userState.GetStreak();
                    existingUser.Plan = _userState.GetPlan();

                    await _apiService.UpdateUserAsync(currentUserId, existingUser);
                    _userState.SetCurrentUser(currentUserId, username);
                }
            }
        }
        catch
        {
        }

        await DisplayAlertAsync("Saved", "Name updated.", "OK");
    }
}