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
        // 1 para pergunta discursiva, 2 para objetiva e 3 vou f;

        [Required]
        public byte Dificult { get; set; } = 2;
        // 1 para fácil, 2 para medio e 3 pra difícil 

        [Required]
        public bool IsPrivate { get; set; } = true;
        // rela��o n n
        public List<Favorites> Favorites { get; } = new();

        public List<Tag> Tags { get; } = new();

        public Guid? InstitutionId { get; set; }

        public Institution? Institution { get; set; }

        // relacao 1 para n
        public ICollection<Answer> Answers { get; } = new List<Answer>();

        // relacao 1 para n
        public ICollection<Comment> Comment { get; } = new List<Comment>();

        // relacao n para n
        public List<Test> Tests { get; } = new();

    }
}