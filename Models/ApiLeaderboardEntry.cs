namespace CodelingoApp.Models;

public class ApiLeaderboardEntry
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public int Points { get; set; }
    public int Streak { get; set; }
    public string Plan { get; set; } = "Free";
}