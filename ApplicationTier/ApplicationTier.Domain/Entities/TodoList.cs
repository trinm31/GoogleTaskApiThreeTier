using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTier.Domain.Entities
{
    public class TodoList : AuditEntity<int>
    {
        [Required]
        public string Content { get; set; }
        public bool IsDone { get; set; } = false;
        [Required] 
        public int TaskId { get; set; }
        [ForeignKey("TaskId")] 
        public virtual TaskItem TaskItem { get; set; }

    }
}
