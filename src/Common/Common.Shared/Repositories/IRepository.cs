using Common.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Common.Shared.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll(int position = 1, int size = 10);

    TEntity? GetById(Guid id);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);    
}
