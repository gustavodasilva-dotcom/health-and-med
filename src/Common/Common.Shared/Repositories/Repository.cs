using Common.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Common.Shared.Repositories;

public class Repository<TDbContext, TEntity>(TDbContext dbContext) : IRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : BaseEntity
{
    protected readonly TDbContext DbContext = dbContext;

    public IQueryable<TEntity> GetAll(int position = 1, int size = 10)
        => DbContext.Set<TEntity>().AsNoTracking()
            .Skip((position - 1) * size)
            .Take(size);

    public TEntity? GetById(Guid id)
        => DbContext.Set<TEntity>().SingleOrDefault(x => x.Id == id);

    public void Add(TEntity entity)
        => DbContext.Set<TEntity>().Add(entity);

    public void Update(TEntity entity)
        => DbContext.Set<TEntity>().Add(entity);

    public void Remove(TEntity entity)
        => DbContext.Set<TEntity>().Remove(entity);
}
