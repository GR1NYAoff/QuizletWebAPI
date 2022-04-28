namespace QuizletWebAPI.Auth.Models
{
    public class BodyAnswer
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
