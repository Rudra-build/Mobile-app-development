namespace CodelingoApp.Models;

public class QuizQuestion
{
    public string QuestionText { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
    public int CorrectOptionIndex { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Difficulty { get; set; } = "Easy";
}