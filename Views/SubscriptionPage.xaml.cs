using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class SubscriptionPage : ContentPage
{
    private readonly UserStateService _userState;
    private readonly ApiService _apiService;

    public SubscriptionPage()
    {
        InitializeComponent();
        _userState = new UserStateService();
        _apiService = new ApiService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadSubscriptionAsync();
    }

    private async Task LoadSubscriptionAsync()
    {
        int currentUserId = _userState.GetCurrentUserId();
        if (currentUserId <= 0) return;

        try
        {
            var sub = await _apiService.GetSubscriptionAsync(currentUserId);

            if (sub != null)
            {
                _userState.SetPlan(sub.Plan);
                CurrentPlanLabel.Text = $"Current Plan: {sub.Plan}";
            }
            else
            {
                CurrentPlanLabel.Text = $"Current Plan: {_userState.GetPlan()}";
            }
        }
        catch (Exception ex)
        {
            CurrentPlanLabel.Text = $"Current Plan: {_userState.GetPlan()}";
            await DisplayAlertAsync("Warning", $"Backend sync failed: {ex.Message}", "OK");
        }
    }

    private async void OnFreeClicked(object sender, EventArgs e)
    {
        int currentUserId = _userState.GetCurrentUserId();
        if (currentUserId <= 0) return;

        try
        {
            bool success = await _apiService.DowngradeSubscriptionAsync(currentUserId);

            if (success)
            {
                _userState.SetPlan("Free");
                CurrentPlanLabel.Text = "Current Plan: Free";
                await DisplayAlertAsync("Updated", "Plan switched to Free.", "OK");
            }
            else
            {
                await DisplayAlertAsync("Error", "Failed to switch to Free.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", $"Failed to downgrade: {ex.Message}", "OK");
        }
    }

    private async void OnPremiumClicked(object sender, EventArgs e)
    {
        int currentUserId = _userState.GetCurrentUserId();
        if (currentUserId <= 0) return;

        try
        {
            var checkoutUrl = await _apiService.CreateStripeCheckoutSessionAsync(currentUserId);

            if (string.IsNullOrWhiteSpace(checkoutUrl))
            {
                await DisplayAlertAsync("Error", "Failed to create Stripe checkout session.", "OK");
                return;
            }

            await Launcher.Default.OpenAsync(checkoutUrl);
            await DisplayAlertAsync("Stripe", "Complete the test payment in browser, then return to the app and reopen Subscription page.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", $"Stripe failed: {ex.Message}", "OK");
        }
    }

    private async void OnRefreshClicked(object sender, EventArgs e)
    {
        await LoadSubscriptionAsync();
    }
}