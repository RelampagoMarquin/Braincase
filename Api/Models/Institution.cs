using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Institution
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        [Required]
        public String Name { get; set; }

        public ICollection<Question> questions { get; } = new List<Question>();
    }
}