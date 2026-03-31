namespace CodelingoApp.Views;

public partial class QuizPage : ContentPage
{
    public QuizPage()
    {
        InitializeComponent();
    }

    private async void OnFinishClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResultsPage());
    }
}
