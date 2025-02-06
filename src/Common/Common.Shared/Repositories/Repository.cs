using System.Linq.Expressions;
using Common.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Common.Shared.Repositories;

public class Repository<TDbContext, TEntity>(TDbContext dbContext) : IRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : BaseEntity
{
    protected readonly TDbContext DbContext = dbContext;

    public virtual IQueryable<TEntity> GetAll(int position = 1, int size = 10)
        => DbContext.Set<TEntity>().AsNoTracking()
            .Skip((position - 1) * size)
            .Take(size);

    public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        => DbContext.Set<TEntity>().Where(filter);

    public virtual TEntity? GetById(Guid id)
        => DbContext.Set<TEntity>().SingleOrDefault(x => x.Id == id);

    public virtual void Add(TEntity entity)
        => DbContext.Set<TEntity>().Add(entity);

    public virtual void Update(TEntity entity)
        => DbContext.Set<TEntity>().Add(entity);

    public virtual void Remove(TEntity entity)
        => DbContext.Set<TEntity>().Remove(entity);
}
