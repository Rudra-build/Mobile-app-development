using CodelingoApp.Models;
using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly UserStateService _userState;

    public LoginPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
        _userState = new UserStateService();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var request = new LoginRequest
        {
            Email = EmailEntry.Text ?? string.Empty,
            Password = PasswordEntry.Text ?? string.Empty
        };

        var user = await _apiService.LoginAsync(request);

        if (user == null)
        {
            await DisplayAlertAsync("Error", "Invalid email or password.", "OK");
            return;
        }

        _userState.SetCurrentUser(user.Id, user.Username);
        _userState.SetPoints(user.Points);
        _userState.SetStreak(user.Streak);
        _userState.SetPlan(user.Plan);
        Preferences.Set("username", user.Username);

        Application.Current!.Windows[0].Page = new NavigationPage(new HomePage());
    }

    private async void OnGoToRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}