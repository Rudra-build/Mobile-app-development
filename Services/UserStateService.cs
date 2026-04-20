namespace CodelingoApp.Services;

public class UserStateService
{
    private const string CurrentUserIdKey = "current_user_id";
    private const string CurrentUsernameKey = "current_username";

    private string PointsKey => $"user_points_{GetCurrentUserId()}";
    private string StreakKey => $"user_streak_{GetCurrentUserId()}";
    private string LastQuizDateKey => $"last_quiz_date_{GetCurrentUserId()}";
    private string PlanKey => $"user_plan_{GetCurrentUserId()}";
    private string FreeQuizCountKey => $"free_quiz_count_{GetCurrentUserId()}";
    private string FreeQuizDateKey => $"free_quiz_date_{GetCurrentUserId()}";

    public int GetPoints() => Preferences.Get(PointsKey, 0);

    public void AddPoints(int points)
    {
        Preferences.Set(PointsKey, GetPoints() + points);
    }

    public void SetPoints(int points)
    {
        Preferences.Set(PointsKey, points);
    }

    public int GetLevel()
    {
        return (GetPoints() / 50) + 1;
    }

    public int GetStreak() => Preferences.Get(StreakKey, 0);

    public void SetStreak(int streak)
    {
        Preferences.Set(StreakKey, streak);
    }

    public void UpdateStreakAfterQuiz()
    {
        if (GetCurrentUserId() <= 0) return;

        string lastQuizDate = Preferences.Get(LastQuizDateKey, string.Empty);
        string today = DateTime.UtcNow.Date.ToString("yyyy-MM-dd");
        string yesterday = DateTime.UtcNow.Date.AddDays(-1).ToString("yyyy-MM-dd");

        if (lastQuizDate == today)
            return;

        int currentStreak = GetStreak();

        if (lastQuizDate == yesterday)
            Preferences.Set(StreakKey, currentStreak + 1);
        else
            Preferences.Set(StreakKey, 1);

        Preferences.Set(LastQuizDateKey, today);
    }

    public string GetPlan() => Preferences.Get(PlanKey, "Free");

    public void SetPlan(string plan)
    {
        Preferences.Set(PlanKey, plan);
    }

    public bool IsPremium() => GetPlan().Equals("Premium", StringComparison.OrdinalIgnoreCase);

    public int GetTodayFreeQuizCount()
    {
        if (GetCurrentUserId() <= 0) return 0;

        string today = DateTime.UtcNow.Date.ToString("yyyy-MM-dd");
        string savedDate = Preferences.Get(FreeQuizDateKey, string.Empty);

        if (savedDate != today)
        {
            Preferences.Set(FreeQuizDateKey, today);
            Preferences.Set(FreeQuizCountKey, 0);
            return 0;
        }

        return Preferences.Get(FreeQuizCountKey, 0);
    }

    public void IncrementFreeQuizCount()
    {
        int current = GetTodayFreeQuizCount();
        Preferences.Set(FreeQuizCountKey, current + 1);
    }

    public bool CanStartQuiz()
    {
        if (IsPremium()) return true;
        return GetTodayFreeQuizCount() < 3;
    }

    public void SetCurrentUser(int userId, string username)
    {
        Preferences.Set(CurrentUserIdKey, userId);
        Preferences.Set(CurrentUsernameKey, username);
    }

    public int GetCurrentUserId()
    {
        return Preferences.Get(CurrentUserIdKey, 0);
    }

    public string GetCurrentUsername()
    {
        return Preferences.Get(CurrentUsernameKey, string.Empty);
    }

    public bool IsLoggedIn()
    {
        return GetCurrentUserId() > 0;
    }

    public string GetBadge()
    {
        int points = GetPoints();

        if (points > 500) return "Gold";
        if (points > 200) return "Silver";
        if (points > 50) return "Bronze";
        return "Beginner";
    }

    public void Logout()
    {
        Preferences.Remove(CurrentUserIdKey);
        Preferences.Remove(CurrentUsernameKey);
    }
}