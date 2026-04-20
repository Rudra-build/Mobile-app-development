using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class ResultsPage : ContentPage
{
    public ResultsPage(int score, int totalQuestions)
    {
        InitializeComponent();

        var gamificationService = new GamificationService();
        var userState = new UserStateService();

        int pointsEarned = gamificationService.CalculatePoints(score);
        userState.AddPoints(pointsEarned);
        userState.UpdateStreakAfterQuiz();

        ScoreLabel.Text = $"Score: {score}/{totalQuestions}";

        double accuracy = (double)score / totalQuestions * 100;
        AccuracyLabel.Text = $"Accuracy: {accuracy:F1}%";

        SummaryLabel.Text = score >= totalQuestions * 0.7
            ? "Great job!"
            : "Good try. Keep practicing!";

        PointsLabel.Text = $"Points earned: {pointsEarned}";
    }

    private async void OnBackHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
    }
}