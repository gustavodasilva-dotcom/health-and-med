using System.Linq.Expressions;
using Common.Shared.Abstractions;

namespace Common.Shared.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll(int position = 1, int size = 10);

    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

    TEntity? GetById(Guid id);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);    
}
