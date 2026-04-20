namespace CodelingoApp.Models;

public class ApiUser
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Points { get; set; }
    public int Streak { get; set; }
    public string Plan { get; set; } = "Free";
}