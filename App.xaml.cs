using CodelingoApp.Services;
using CodelingoApp.Views;

namespace CodelingoApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var userState = new UserStateService();

        if (userState.IsLoggedIn())
            MainPage = new NavigationPage(new HomePage());
        else
            MainPage = new NavigationPage(new LoginPage());
    }
}