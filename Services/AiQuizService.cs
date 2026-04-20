using CodelingoApp.Models;

namespace CodelingoApp.Services;

public class AiQuizService
{
    public QuizSession GenerateQuiz(string category = "General", string difficulty = "Medium")
    {
        return new QuizSession
        {
            Questions = new List<QuizQuestion>
            {
                new()
                {
                    QuestionText = $"[{difficulty}] Which of these best describes an algorithm?",
                    Options = new() { "A random guess", "A step-by-step procedure", "A database table", "A network cable" },
                    CorrectOptionIndex = 1,
                    Category = category,
                    Difficulty = difficulty
                },
                new()
                {
                    QuestionText = $"[{difficulty}] What does API stand for?",
                    Options = new() { "Application Programming Interface", "Applied Program Internet", "Advanced Processing Input", "Automated Program Integration" },
                    CorrectOptionIndex = 0,
                    Category = category,
                    Difficulty = difficulty
                },
                new()
                {
                    QuestionText = $"[{difficulty}] Which data structure is LIFO?",
                    Options = new() { "Queue", "Stack", "Array", "Tree" },
                    CorrectOptionIndex = 1,
                    Category = category,
                    Difficulty = difficulty
                },
                new()
                {
                    QuestionText = $"[{difficulty}] What is a compiler mainly used for?",
                    Options = new() { "Playing audio", "Converting code into executable form", "Browsing websites", "Storing images" },
                    CorrectOptionIndex = 1,
                    Category = category,
                    Difficulty = difficulty
                },
                new()
                {
                    QuestionText = $"[{difficulty}] Which of these is a programming language?",
                    Options = new() { "HTTP", "HTML", "C#", "Wi-Fi" },
                    CorrectOptionIndex = 2,
                    Category = category,
                    Difficulty = difficulty
                }
            }
        };
    }
}