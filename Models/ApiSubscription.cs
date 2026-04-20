namespace CodelingoApp.Models;

public class ApiSubscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Plan { get; set; } = "Free";
    public bool IsActive { get; set; }
    public DateTime UpdatedAt { get; set; }
}