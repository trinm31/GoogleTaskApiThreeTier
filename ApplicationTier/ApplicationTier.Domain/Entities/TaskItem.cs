using System.ComponentModel.DataAnnotations;

namespace ApplicationTier.Domain.Entities;

public class TaskItem : EntityBase<int>
{
    public string Name { get; set; }
    public string Content { get; set; }
    public bool IsDone { get; set; } = false;
    public DateTime DateAdded { get; set; } = DateTime.Now;
    
}