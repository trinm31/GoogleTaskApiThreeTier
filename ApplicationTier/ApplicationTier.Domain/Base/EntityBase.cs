using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationTier.Domain.Entities;

public interface IEntityBase<TKey>
{
    TKey Id { get; set; }
}

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    [Key]
    public virtual TKey Id { get; set; }
}
