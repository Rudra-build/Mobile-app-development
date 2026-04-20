namespace CodelingoApp.Models;

public class AiQuizQuestion
{
    public string QuestionText { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
    public int CorrectOptionIndex { get; set; }
    public string Category { get; set; } = string.Empty;
}