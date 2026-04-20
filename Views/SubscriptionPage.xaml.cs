using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class SubscriptionPage : ContentPage
{
    private readonly UserStateService _userState;

    public SubscriptionPage()
    {
        InitializeComponent();
        _userState = new UserStateService();
        UpdatePlanLabel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdatePlanLabel();
    }

    private void UpdatePlanLabel()
    {
        CurrentPlanLabel.Text = $"Current Plan: {_userState.GetPlan()}";
    }

    private async void OnFreeClicked(object sender, EventArgs e)
    {
        _userState.SetPlan("Free");
        UpdatePlanLabel();
        await DisplayAlertAsync("Updated", "Plan switched to Free.", "OK");
    }

    private async void OnPremiumClicked(object sender, EventArgs e)
    {
        _userState.SetPlan("Premium");
        UpdatePlanLabel();
        await DisplayAlertAsync("Updated", "Plan upgraded to Premium.", "OK");
    }
}