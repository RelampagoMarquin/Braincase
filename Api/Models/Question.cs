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
        public byte Type { get; set; } = 1; // o '=' define o valor como padr�o

        [Required]
        public byte Dificult { get; set; } = 2;

        [Required]
        public bool IsPrivate { get; set; } = true;
        // rela��o n n
        public List<Favorites> Favorites { get; } = new();
        public List<Tag> Tags { get; } = new();
        public Institution? Institution {get; set;}

        // relacao 1 para n
        public ICollection<Answer> Answers { get; } = new List<Answer>();

        // relacao n para n
        public List<Test> Tests { get; } = new();

    }
}