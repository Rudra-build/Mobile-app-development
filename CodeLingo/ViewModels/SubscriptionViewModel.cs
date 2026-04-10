using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CodeLingo.Services;

namespace CodeLingo.ViewModels
{
    public partial class SubscriptionViewModel : ObservableObject
    {
        private readonly SubscriptionService _subscriptionService;
        private readonly string _userId;

        [ObservableProperty] private string planLabel;
        [ObservableProperty] private string planDescription;
        [ObservableProperty] private string expiryText;
        [ObservableProperty] private string planColor;
        [ObservableProperty] private bool isFreePlan;
        [ObservableProperty] private bool isPremiumPlan;
        [ObservableProperty] private string quizAccessIcon;
        [ObservableProperty] private string quizAccessText;
        [ObservableProperty] private string aiIcon;
        [ObservableProperty] private string aiText;
        [ObservableProperty] private string analyticsIcon;
        [ObservableProperty] private string analyticsText;

        public SubscriptionViewModel(SubscriptionService service, string userId)
        {
            _subscriptionService = service;
            _userId = userId;
            LoadSubscriptionAsync();
        }

        private async void LoadSubscriptionAsync()
        {
            var sub = await _subscriptionService.GetSubscriptionAsync(_userId);

            if (sub.IsPremium)
            {
                PlanLabel       = "⭐ Premium Plan";
                PlanDescription = "You have full access to all features!";
                ExpiryText      = $"Renews on: {sub.EndDate?.ToString("dd MMM yyyy")}";
                PlanColor       = "#6C63FF";
                IsFreePlan      = false;
                IsPremiumPlan   = true;

                QuizAccessIcon = "✅"; QuizAccessText = "Unlimited Quizzes";
                AiIcon         = "✅"; AiText         = "AI-Generated Questions";
                AnalyticsIcon  = "✅"; AnalyticsText  = "Advanced Analytics";
            }
            else
            {
                PlanLabel       = "Free Plan";
                PlanDescription = "Limited access. Upgrade to unlock everything!";
                ExpiryText      = "";
                PlanColor       = "#A0A0A0";
                IsFreePlan      = true;
                IsPremiumPlan   = false;

                QuizAccessIcon = "⚠️"; QuizAccessText = "Up to 5 Quizzes only";
                AiIcon         = "❌"; AiText         = "No AI-Generated Questions";
                AnalyticsIcon  = "❌"; AnalyticsText  = "No Advanced Analytics";
            }
        }

        [RelayCommand]
        private async Task GoToUpgrade() =>
            await Shell.Current.GoToAsync("UpgradePage");

        [RelayCommand]
        private async Task Downgrade()
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Downgrade",
                "Are you sure you want to downgrade to Free?",
                "Yes", "Cancel");

            if (confirm)
            {
                await _subscriptionService.DowngradeAsync(_userId);
                LoadSubscriptionAsync();
            }
        }
    }
}
