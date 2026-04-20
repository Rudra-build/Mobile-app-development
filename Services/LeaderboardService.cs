namespace CodelingoApp.Services;

public class LeaderboardEntry
{
    public string Name { get; set; } = "";
    public int Points { get; set; }
}

public class LeaderboardService
{
    public List<LeaderboardEntry> GetLeaderboard(int currentUserPoints, string currentUserName)
    {
        var mock = new List<LeaderboardEntry>
        {
            new() { Name = "Alice", Points = 320 },
            new() { Name = "Bob", Points = 250 },
            new() { Name = "Charlie", Points = 200 },
            new() { Name = currentUserName, Points = currentUserPoints }
        };

        return mock.OrderByDescending(x => x.Points).ToList();
    }
}