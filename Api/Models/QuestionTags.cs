using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class QuestionTags
    {
        public Guid QuestionId { get; set; }

        public Guid TagId { get; set; }

        public Question question { get; set; } = null!;

        public Tag tag { get; set; } = null!;
    }
}