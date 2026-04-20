namespace CodelingoApp.Services;

public class UserStateService
{
    private const string PointsKey = "user_points";
    private const string StreakKey = "user_streak";
    private const string LastQuizDateKey = "last_quiz_date";
    private const string PlanKey = "user_plan";
    private const string FreeQuizCountKey = "free_quiz_count";
    private const string FreeQuizDateKey = "free_quiz_date";

    public int GetPoints() => Preferences.Get(PointsKey, 0);

    public void AddPoints(int points)
    {
        Preferences.Set(PointsKey, GetPoints() + points);
    }

    public int GetLevel()
    {
        return (GetPoints() / 50) + 1;
    }

    public int GetStreak() => Preferences.Get(StreakKey, 0);

    public void UpdateStreakAfterQuiz()
    {
        string lastQuizDate = Preferences.Get(LastQuizDateKey, string.Empty);
        string today = DateTime.UtcNow.Date.ToString("yyyy-MM-dd");
        string yesterday = DateTime.UtcNow.Date.AddDays(-1).ToString("yyyy-MM-dd");

        if (lastQuizDate == today) return;

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
}