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
        
        [MaxLength(int.MaxValue)]
        public String? Justify { get; set; }

        public Question Question { get; set; }
    }
}