namespace ApplicationTier.Domain.Dtos;

public class TaskUpSertDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.Now;
}