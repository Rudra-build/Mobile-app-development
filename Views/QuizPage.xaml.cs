using CodelingoApp.Models;
using CodelingoApp.Services;

namespace CodelingoApp.Views;

public partial class QuizPage : ContentPage
{
    private readonly QuizService _quizService;
    private QuizSession _session;
    private int _timeLeft;
    private bool _isProcessingAnswer;

    public QuizPage() : this(null)
    {
    }

    public QuizPage(QuizSession? session)
    {
        InitializeComponent();

        _quizService = new QuizService();
        _session = session ?? _quizService.CreateSampleQuiz();

        LoadQuestion();
    }

    private void LoadQuestion()
    {
        if (_session.IsFinished)
        {
            Navigation.PushAsync(new ResultsPage(_session.Score, _session.Questions.Count));
            return;
        }

        var question = _session.CurrentQuestion;
        if (question == null) return;

        _isProcessingAnswer = false;

        QuestionLabel.Text = question.QuestionText;
        OptionButton1.Text = question.Options[0];
        OptionButton2.Text = question.Options[1];
        OptionButton3.Text = question.Options[2];
        OptionButton4.Text = question.Options[3];

        ProgressLabel.Text = $"Question {_session.CurrentQuestionIndex + 1} of {_session.Questions.Count}";

        StartTimer();
    }

    private void StartTimer()
    {
        _timeLeft = _session.SecondsPerQuestion;
        TimerLabel.Text = $"Time left: {_timeLeft}s";

        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (_isProcessingAnswer)
                return false;

            _timeLeft--;
            TimerLabel.Text = $"Time left: {_timeLeft}s";

            if (_timeLeft <= 0)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    if (_isProcessingAnswer) return;

                    _isProcessingAnswer = true;
                    _session.CurrentQuestionIndex++;

                    await DisplayAlertAsync("Time up", "Moving to the next question.", "OK");
                    LoadQuestion();
                });

                return false;
            }

            return true;
        });
    }

    private async void OnOptionClicked(object sender, EventArgs e)
    {
        if (_isProcessingAnswer) return;
        _isProcessingAnswer = true;

        if (sender is not Button button) return;

        int selectedIndex = int.Parse(button.CommandParameter?.ToString() ?? "0");
        bool isCorrect = _quizService.SubmitAnswer(_session, selectedIndex);

        await DisplayAlertAsync(
            isCorrect ? "Correct" : "Wrong",
            isCorrect ? "Nice one." : "That was not the right answer.",
            "Next"
        );

        LoadQuestion();
    }
}