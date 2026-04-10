using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CodeLingo.Services;

namespace CodeLingo.ViewModels
{
    public partial class UpgradeViewModel : ObservableObject
    {
        private readonly SubscriptionService _subscriptionService;
        private readonly string _userId;

        public UpgradeViewModel(SubscriptionService service, string userId)
        {
            _subscriptionService = service;
            _userId = userId;
        }

        [RelayCommand]
        private async Task Upgrade()
        {
            // Simulate payment processing
            await Shell.Current.DisplayAlert(
                "Processing...",
                "Simulating payment... Please wait.",
                "OK");

            await _subscriptionService.UpgradeAsync(_userId);

            await Shell.Current.DisplayAlert(
                "🎉 Success!",
                "You are now a Premium member!",
                "Let's Go!");

            await Shell.Current.GoToAsync("//SubscriptionPage");
        }
    }
}
