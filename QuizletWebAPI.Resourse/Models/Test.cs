using System.ComponentModel.DataAnnotations;

namespace QuizletWebAPI.Resourse.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Questions { get; set; } = string.Empty;

        [Required]
        public string Answers { get; set; } = string.Empty;
    }
}
