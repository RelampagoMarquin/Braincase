using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models
{
    public class Test{

        [Key]
        public Guid Id { get; set; }

        [StringLength(100)]
        [Required]
        public String Name { get; set; }

        [StringLength(60)]
        [Required]
        public String ClassName { get; set; }

        [Required]
        public DateTime CreateAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime LastUse { get; set; }

        [StringLength(120)]
        public String? LogoUrl { get; set; }

        public List<Question> Questions { get; } = new();

        public String UserId { get; set; }

        public User User { get; set; }
    }
}