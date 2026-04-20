using CodelingoApp.Models;
using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class RegisterPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly UserStateService _userState;

    public RegisterPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
        _userState = new UserStateService();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var newUser = new ApiUser
        {
            Username = UsernameEntry.Text ?? string.Empty,
            Email = EmailEntry.Text ?? string.Empty,
            Password = PasswordEntry.Text ?? string.Empty,
            Points = 0,
            Streak = 0,
            Plan = "Free"
        };

        var createdUser = await _apiService.RegisterAsync(newUser);

        if (createdUser == null)
        {
            await DisplayAlertAsync("Error", "Registration failed. Email may already exist.", "OK");
            return;
        }

        _userState.SetCurrentUser(createdUser.Id, createdUser.Username);
        _userState.SetPoints(createdUser.Points);
        _userState.SetStreak(createdUser.Streak);
        _userState.SetPlan(createdUser.Plan);
        Preferences.Set("username", createdUser.Username);

        await DisplayAlertAsync("Success", "Account created.", "OK");
        Application.Current!.Windows[0].Page = new NavigationPage(new HomePage());
    }

    private async void OnGoToLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}