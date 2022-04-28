namespace QuizletWebAPI.Auth.Models
{
    public class Answer
    {
        public int QuestionNumber { get; set; }
        public BodyAnswer[]? BodyAnswer { get; set; }
    }
}
