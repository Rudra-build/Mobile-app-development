namespace CodelingoApp.Services;

public class GamificationService
{
    public int CalculatePoints(int score) => score * 10;

    public int UpdateStreak(bool completedToday, int currentStreak)
    {
        return completedToday ? currentStreak + 1 : currentStreak;
    }
}