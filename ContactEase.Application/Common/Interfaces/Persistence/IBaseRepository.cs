namespace ContactEase.Application.Common.Interfaces.Persistence;

public interface IBaseRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity?> GetById(Guid id);
    Task<TEntity> Add(TEntity entity);
    void Edit(TEntity entity);
    Task Delete(TEntity entity);
    Task Save(CancellationToken cancellationToken);
}
