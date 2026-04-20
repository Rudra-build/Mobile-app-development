namespace CodelingoApp.Models;

public class ApiAnalytics
{
    public int TotalQuizzes { get; set; }
    public double AverageAccuracy { get; set; }
    public int BestScore { get; set; }
    public int CurrentStreak { get; set; }
}