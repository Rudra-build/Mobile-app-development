namespace CodelingoApp.Views;

public partial class ResultsPage : ContentPage
{
    public ResultsPage()
    {
        InitializeComponent();
    }

    private async void OnBackHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
