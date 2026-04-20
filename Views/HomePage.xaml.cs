using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class HomePage : ContentPage
{
    private readonly UserStateService _userState;
    private readonly AiQuizService _aiQuizService;
    private readonly QuizService _quizService;

    public HomePage()
    {
        InitializeComponent();
        _userState = new UserStateService();
        _aiQuizService = new AiQuizService();
        _quizService = new QuizService();

        CategoryPicker.SelectedIndex = 0;
        DifficultyPicker.SelectedIndex = 0;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        int points = _userState.GetPoints();
        int streak = _userState.GetStreak();
        int level = _userState.GetLevel();
        string plan = _userState.GetPlan();

        PointsLabel.Text = $"Current Points: {points}";
        StreakLabel.Text = $"Streak: {streak} days";
        PlanLabel.Text = $"Plan: {plan}";
        LevelLabel.Text = $"Level: {level}";
    }

    private async void OnStartQuizClicked(object sender, EventArgs e)
    {
        if (!_userState.CanStartQuiz())
        {
            await DisplayAlertAsync("Free limit reached", "Upgrade to Premium for unlimited quizzes.", "OK");
            return;
        }

        if (!_userState.IsPremium())
        {
            _userState.IncrementFreeQuizCount();
        }

        string category = CategoryPicker.SelectedItem?.ToString() ?? "All";
        string difficulty = DifficultyPicker.SelectedItem?.ToString() ?? "All";

        var quiz = _quizService.CreateQuiz(category, difficulty);
        await Navigation.PushAsync(new QuizPage(quiz));
    }

    private async void OnAiQuizClicked(object sender, EventArgs e)
    {
        if (!_userState.IsPremium())
        {
            await DisplayAlertAsync("Premium feature", "AI-generated quizzes are available on Premium only.", "OK");
            return;
        }

        var aiQuiz = _aiQuizService.GenerateQuiz("AI Quiz", "Medium");
        await Navigation.PushAsync(new QuizPage(aiQuiz));
    }

    private async void OnProfileClicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new ProfilePage());

    private async void OnSubscriptionClicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new SubscriptionPage());

    private async void OnLeaderboardClicked(object sender, EventArgs e) =>
    await Navigation.PushAsync(new LeaderboardPage());
}