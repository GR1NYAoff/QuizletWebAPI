namespace QuizletWebAPI.Auth.Models
{
    public class Answer
    {
        public string BodyAnswer { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
