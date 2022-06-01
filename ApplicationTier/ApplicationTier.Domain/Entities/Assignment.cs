﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTier.Domain.Entities
{
    public class Assignment: AuditEntity<int>
    {
        [Required] 
        public int UserId { get; set; }
        [ForeignKey("UserId")] 
        public virtual User User { get; set; }
        [Required]
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual TaskItem TaskItem { get; set; }

    } 
}
