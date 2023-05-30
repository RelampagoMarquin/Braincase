using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models{
    public class User {

        [Key]
        public Guid Id { get; set; }

        [StringLength(120)]
        [Required]
        public String Name { get; set; }
   
        [StringLength(120)]
        [Required]
        public String Email { get; set; }

        [StringLength(14)]
        [Required]
        public String Registration { get; set; }

        [StringLength(120)]
        [Required]
        public String Password { get; set; }

        // Relação um para muitos, um usuario tem varios comentarios
        public ICollection<Comment> Comments { get; } = new List<Comment>();
        
        // Relação n n
        public List<Favorites> Favorites { get; } = new();
    }
}
