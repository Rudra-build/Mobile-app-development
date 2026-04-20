using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();

        NameEntry.Text = Preferences.Get("username", "");

        var userState = new UserStateService();
        PointsLabel.Text = $"Points: {userState.GetPoints()}";
        StreakLabel.Text = $"Streak: {userState.GetStreak()} days";
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        Preferences.Set("username", NameEntry.Text ?? "User");
        await DisplayAlertAsync("Saved", "Name updated.", "OK");
    }
}