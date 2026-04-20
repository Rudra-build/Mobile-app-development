using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class HomePage : ContentPage
{
    private readonly UserStateService _userState;
    private readonly AiQuizService _aiQuizService;
    private readonly QuizService _quizService;
    private readonly ApiService _apiService;

    public HomePage()
    {
        InitializeComponent();
        _userState = new UserStateService();
        _aiQuizService = new AiQuizService();
        _quizService = new QuizService();
        _apiService = new ApiService();

        CategoryPicker.SelectedIndex = 0;
        DifficultyPicker.SelectedIndex = 0;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        int currentUserId = _userState.GetCurrentUserId();

        try
        {
            if (currentUserId > 0)
            {
                var user = await _apiService.GetUserAsync(currentUserId);
                if (user != null)
                {
                    int localPoints = _userState.GetPoints();
                    int localStreak = _userState.GetStreak();

                    int finalPoints = Math.Max(localPoints, user.Points);
                    int finalStreak = Math.Max(localStreak, user.Streak);

                    _userState.SetCurrentUser(user.Id, user.Username);
                    _userState.SetPoints(finalPoints);
                    _userState.SetStreak(finalStreak);
                    _userState.SetPlan(user.Plan);
                    Preferences.Set("username", user.Username);
                }
            }
        }
        catch
        {
        }

        PointsLabel.Text = $"Current Points: {_userState.GetPoints()}";
        StreakLabel.Text = $"Streak: {_userState.GetStreak()} days";
        PlanLabel.Text = $"Plan: {_userState.GetPlan()}";
        LevelLabel.Text = $"Level: {_userState.GetLevel()}";
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

        string difficulty = DifficultyPicker.SelectedItem?.ToString() ?? "Medium";

        var aiQuestions = await _apiService.GenerateAiQuizAsync("Programming", difficulty);

        if (aiQuestions.Count == 0)
        {
            await DisplayAlertAsync("Error", "AI endpoint returned 0 questions.", "OK");
            return;
        }

        var quiz = _quizService.CreateQuizFromAi(aiQuestions);
        await Navigation.PushAsync(new QuizPage(quiz));
    }

    private async void OnLeaderboardClicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new LeaderboardPage());

    private async void OnProfileClicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new ProfilePage());

    private async void OnSubscriptionClicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new SubscriptionPage());

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        _userState.Logout();
        Preferences.Remove("username");
        Application.Current!.Windows[0].Page = new NavigationPage(new LoginPage());
        await Task.CompletedTask;
    }

    private async void OnAnalyticsClicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new AnalyticsPage());
}