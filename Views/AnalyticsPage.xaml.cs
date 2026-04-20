using CodelingoApp.Models;
using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class AnalyticsPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly UserStateService _userState;

    public AnalyticsPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
        _userState = new UserStateService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_userState.IsPremium())
        {
            StatusLabel.Text = "Premium only feature.";
            TotalQuizzesLabel.Text = "Upgrade to Premium to view analytics.";
            AverageAccuracyLabel.Text = "";
            BestScoreLabel.Text = "";
            CurrentStreakLabel.Text = "";
            return;
        }

        try
        {
            int currentUserId = _userState.GetCurrentUserId();
            var analytics = await _apiService.GetAnalyticsAsync(currentUserId);

            if (analytics == null)
            {
                StatusLabel.Text = "Failed to load analytics.";
                return;
            }

            StatusLabel.Text = "Analytics loaded.";
            TotalQuizzesLabel.Text = $"Total Quizzes: {analytics.TotalQuizzes}";
            AverageAccuracyLabel.Text = $"Average Accuracy: {analytics.AverageAccuracy:F1}%";
            BestScoreLabel.Text = $"Best Score: {analytics.BestScore}";
            CurrentStreakLabel.Text = $"Current Streak: {analytics.CurrentStreak}";
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Error: {ex.Message}";
        }
    }
}