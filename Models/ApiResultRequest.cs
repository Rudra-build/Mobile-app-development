namespace CodelingoApp.Models;

public class ApiResultRequest
{
    public int UserId { get; set; }
    public int QuizId { get; set; }
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public double Accuracy { get; set; }
}