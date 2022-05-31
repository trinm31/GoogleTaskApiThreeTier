using System.Linq.Expressions;
using ApplicationTier.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTier.Infrastructure;

public class Repository<T> : IRepository<T> where T : class
{
    public DbSet<T> Entities => DbContext.Set<T>();

    public DbContext DbContext { get; private set; }

    public Repository(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task DeleteAsync(int id, bool saveChanges = true)
    {
        var entity = await Entities.FindAsync(id);
        await DeleteAsync(entity);

        if (saveChanges)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(T entity, bool saveChanges = true)
    {
        Entities.Remove(entity);
        if (saveChanges)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
    {
        var enumerable = entities as T[] ?? entities.ToArray();
        if (enumerable.Any())
        {
            Entities.RemoveRange(enumerable);
        }

        if (saveChanges)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    public async Task<IList<T>> GetAllAsync()
    {
        return await Entities.ToListAsync();
    }

    public T Find(params object[] keyValues)
    {
        return Entities.Find(keyValues);
    }

    public virtual async Task<T> FindAsync(params object[] keyValues)
    {
        return await Entities.FindAsync(keyValues);
    }

    public async Task InsertAsync(T entity, bool saveChanges = true)
    {
        await Entities.AddAsync(entity);

        if (saveChanges)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    public async Task InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
    {
        await DbContext.AddRangeAsync(entities);

        if (saveChanges)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
    {
        IQueryable<T> query = Entities;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
    {
        IQueryable<T> query = Entities;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }
}