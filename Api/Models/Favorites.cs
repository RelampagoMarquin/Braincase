using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Favorites
    {
        public bool Own { get; set; }

        /// n n
        public String UserId { get; set; }
        public Guid QuestionId { get; set; }

        public User User { get; set; } = null!;

        public Question Question { get; set; } = null!;

        
    }
}