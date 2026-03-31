namespace CodelingoApp.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnStartQuizClicked(object sender, EventArgs e) => await Navigation.PushAsync(new QuizPage());
    private async void OnProfileClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ProfilePage());
    private async void OnSubscriptionClicked(object sender, EventArgs e) => await Navigation.PushAsync(new SubscriptionPage());
}
