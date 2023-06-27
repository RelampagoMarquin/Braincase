using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models{
    public class User : IdentityUser {

        [Key]
        [StringLength(36)]
        public override String Id { get; set; } = new Guid().ToString();

        [StringLength(120)]
        [Required]
        public String Name { get; set; }
   
        [StringLength(120)]
        [Required]
        public override String Email { get; set; }

        [StringLength(14)]
        public String? Registration { get; set; } = "";

        // Relação um para muitos, um usuario tem varios comentarios
        public ICollection<Comment> Comments { get; } = new List<Comment>();
        
        // Relação n n
        public List<Favorites> Favorites { get; } = new();
    }
}
