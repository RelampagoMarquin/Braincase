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

        // relação um para muitos
        public User? User { get; set; }

    }
}
