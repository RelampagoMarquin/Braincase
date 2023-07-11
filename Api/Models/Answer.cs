using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(int.MaxValue)]
        [Required]
        public String Text { get; set; }

        public Boolean IsCorrect { get; set; } = false;

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }
    }
}