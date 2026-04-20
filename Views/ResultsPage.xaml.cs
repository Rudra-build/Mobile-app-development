using CodelingoApp.Models;
using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class ResultsPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly UserStateService _userState;

    public ResultsPage(int score, int totalQuestions)
    {
        InitializeComponent();

        _apiService = new ApiService();
        _userState = new UserStateService();

        var gamificationService = new GamificationService();

        int pointsEarned = gamificationService.CalculatePoints(score);
        _userState.AddPoints(pointsEarned);
        _userState.UpdateStreakAfterQuiz();
       

        ScoreLabel.Text = $"Score: {score}/{totalQuestions}";

        double accuracy = (double)score / totalQuestions * 100;
        AccuracyLabel.Text = $"Accuracy: {accuracy:F1}%";

        SummaryLabel.Text = score >= totalQuestions * 0.7
            ? "Great job!"
            : "Good try. Keep practicing!";

        SummaryLabel.Text += $" Streak now: {_userState.GetStreak()}";
        
        PointsLabel.Text = $"Points earned: {pointsEarned}";

        _ = SaveResultToApiAsync(score, totalQuestions, accuracy);
        _ = SyncUserToApiAsync();
    }

    private async Task SaveResultToApiAsync(int score, int totalQuestions, double accuracy)
    {
        try
        {
            int currentUserId = _userState.GetCurrentUserId();
            if (currentUserId <= 0) return;

            var request = new ApiResultRequest
            {
                UserId = currentUserId,
                QuizId = 1,
                Score = score,
                TotalQuestions = totalQuestions,
                Accuracy = accuracy
            };

            await _apiService.SaveResultAsync(request);
        }
        catch
        {
        }
    }

    private async Task SyncUserToApiAsync()
    {
        try
        {
            int currentUserId = _userState.GetCurrentUserId();
            if (currentUserId <= 0) return;

            var existingUser = await _apiService.GetUserAsync(currentUserId);
            if (existingUser == null) return;

            existingUser.Username = Preferences.Get("username", existingUser.Username);
            existingUser.Points = _userState.GetPoints();
            existingUser.Streak = _userState.GetStreak();
            existingUser.Plan = _userState.GetPlan();

            await _apiService.UpdateUserAsync(currentUserId, existingUser);
        }
        catch
        {
        }
    }

    private async void OnBackHomeClicked(object sender, EventArgs e)
    {
        int score = int.Parse(ScoreLabel.Text.Split(':')[1].Split('/')[0].Trim());
        int totalQuestions = int.Parse(ScoreLabel.Text.Split('/')[1].Trim());

        double accuracy = (double)score / totalQuestions * 100;

        await SaveResultToApiAsync(score, totalQuestions, accuracy);
        await SyncUserToApiAsync();

        await Navigation.PushAsync(new HomePage());
    }
}