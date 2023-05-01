using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Question
    {

        [Key]
        public Guid Id { get; set; }

        [MaxLength(int.MaxValue)]
        [Required]
        public String Text { get; set; }

        [Required]
        public byte type { get; set; } = 1; // o '=' define o valor como padrão

        [Required]
        public byte dificult { get; set; } = 2;

        [Required]
        public bool isPrivate { get; set; } = true;

        // relação n n
        public List<Favorites> Favorites { get; } = new();

    }
}