using System.ComponentModel.DataAnnotations;

namespace QuizletWebAPI.Resourse.Models
{
    public class Access
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string TestId { get; set; } = string.Empty;
    }
}
