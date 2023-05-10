using ContactEase.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContactEase.Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> Get()
    {
        return _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetById(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        return await Task.FromResult(entity);
    }

    public async Task Delete(TEntity entity)
    {
        await Task.Run(() => _context.Set<TEntity>().Remove(entity));
    }

    public void Edit(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task Save(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
