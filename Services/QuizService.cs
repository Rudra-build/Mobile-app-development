using CodelingoApp.Models;

namespace CodelingoApp.Services;

public class QuizService
{
    private readonly List<QuizQuestion> _allQuestions = new()
    {
        new()
        {
            QuestionText = "What does HTML stand for?",
            Options = new() { "HyperText Markup Language", "HighText Machine Language", "HyperTool Markup Language", "Home Tool Markup Language" },
            CorrectOptionIndex = 0,
            Category = "Web",
            Difficulty = "Easy"
        },
        new()
        {
            QuestionText = "Which HTML tag is used for the largest heading?",
            Options = new() { "<h6>", "<head>", "<h1>", "<header>" },
            CorrectOptionIndex = 2,
            Category = "Web",
            Difficulty = "Easy"
        },
        new()
        {
            QuestionText = "Which company created C#?",
            Options = new() { "Google", "Microsoft", "Apple", "Oracle" },
            CorrectOptionIndex = 1,
            Category = "Programming",
            Difficulty = "Easy"
        },
        new()
        {
            QuestionText = "Which keyword creates an object in C#?",
            Options = new() { "make", "init", "new", "class" },
            CorrectOptionIndex = 2,
            Category = "Programming",
            Difficulty = "Easy"
        },
        new()
        {
            QuestionText = "Which data structure uses FIFO?",
            Options = new() { "Stack", "Queue", "Tree", "Graph" },
            CorrectOptionIndex = 1,
            Category = "DSA",
            Difficulty = "Medium"
        },
        new()
        {
            QuestionText = "Which data structure uses LIFO?",
            Options = new() { "Queue", "Stack", "Graph", "Heap" },
            CorrectOptionIndex = 1,
            Category = "DSA",
            Difficulty = "Medium"
        },
        new()
        {
            QuestionText = "What does SQL stand for?",
            Options = new() { "Structured Query Language", "Simple Query Language", "Sequential Query Logic", "System Query Language" },
            CorrectOptionIndex = 0,
            Category = "Database",
            Difficulty = "Easy"
        },
        new()
        {
            QuestionText = "Which SQL command is used to retrieve data?",
            Options = new() { "GET", "FETCH", "SELECT", "OPEN" },
            CorrectOptionIndex = 2,
            Category = "Database",
            Difficulty = "Easy"
        }
    };

    public QuizSession CreateSampleQuiz()
    {
        return new QuizSession
        {
            Questions = _allQuestions.Take(5).ToList()
        };
    }

    public QuizSession CreateQuiz(string category, string difficulty)
    {
        var filtered = _allQuestions
            .Where(q =>
                (category == "All" || q.Category == category) &&
                (difficulty == "All" || q.Difficulty == difficulty))
            .Take(5)
            .ToList();

        if (filtered.Count == 0)
        {
            filtered = _allQuestions.Take(5).ToList();
        }

        return new QuizSession
        {
            Questions = filtered
        };
    }

    public bool SubmitAnswer(QuizSession session, int selectedIndex)
    {
        var question = session.CurrentQuestion;
        if (question == null) return false;

        bool correct = selectedIndex == question.CorrectOptionIndex;
        if (correct) session.Score++;

        session.CurrentQuestionIndex++;
        return correct;
    }


    public QuizSession CreateQuizFromAi(List<AiQuizQuestion> aiQuestions)
    {
        var questions = aiQuestions.Select(q => new QuizQuestion
        {
            QuestionText = q.QuestionText,
            Options = q.Options,
            CorrectOptionIndex = q.CorrectOptionIndex,
            Category = q.Category,
            Difficulty = "AI"
        }).ToList();

        return new QuizSession
        {
            Questions = questions
        };
    }
}