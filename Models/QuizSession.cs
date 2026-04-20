namespace CodelingoApp.Models;

public class QuizSession
{
    public List<QuizQuestion> Questions { get; set; } = new();
    public int CurrentQuestionIndex { get; set; }
    public int Score { get; set; }
    public int SecondsPerQuestion { get; set; } = 15;

    public QuizQuestion? CurrentQuestion =>
        CurrentQuestionIndex < Questions.Count ? Questions[CurrentQuestionIndex] : null;

    public bool IsFinished => CurrentQuestionIndex >= Questions.Count;
}