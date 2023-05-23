using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models
{
    public class Subject{

        [Key]
        public Guid Id { get; set; }

        [StringLength(150)]
        [Required]
        public String Nome { get; set; }

        public ICollection<Tag> Tags {get; } = new List<Tag>();
    }
}