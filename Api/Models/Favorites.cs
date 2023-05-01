using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Favorites
    {
        public bool own { get; set; }

        /// n n
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }

        public User User { get; set; } = null!;

        public Question question { get; set; } = null!;

        
    }
}