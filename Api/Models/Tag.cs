using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models
{
    public class Tag{

        [Key]
        public Guid Id { get; set; }

        [StringLength(120)]
        [Required]
        public String Name { get; set; }

        public Subject? Subject {get; set; }
        public List<Question> Question { get; } = new();
    }
}