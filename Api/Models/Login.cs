using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email é obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password é obrigatório!")]
        public string Password { get; set; }
    }
}
