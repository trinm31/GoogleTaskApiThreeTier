using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTier.Domain.Entities
{
    public class Project: AuditEntity<int>
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
