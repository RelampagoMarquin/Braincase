using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Comment
    {

        [Key]
        public Guid Id { get; set; }

        [MaxLength(int.MaxValue)]
        [Required]
        public string Text { get; set; }

        // rela��o um para muitos
        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
