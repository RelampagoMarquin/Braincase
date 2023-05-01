using System.ComponentModel.DataAnnotations;

namespace Api.Models{
    public class User{

        [Key]
        public Guid Id { get; set; }

        [StringLength(120)]
        [Required]
        public String Name { get; set; }

        [StringLength(120)]
        [Required]
        public String Email { get; set; }

        [StringLength(120)]
        [Required]
        public String Password { get; set; }
    }
}
