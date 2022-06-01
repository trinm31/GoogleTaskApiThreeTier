using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationTier.Domain.Entities;

public class TaskItem : AuditEntity<int>
{
    [Required]
    public string Name { get; set; }
    public string Content { get; set; }
    public bool IsDone { get; set; } = false ;
    [Required]
    public int ListTaskId { get; set; }
    [ForeignKey("ListTaskId")] 
    public virtual ListTask ListTask { get; set; }
}  