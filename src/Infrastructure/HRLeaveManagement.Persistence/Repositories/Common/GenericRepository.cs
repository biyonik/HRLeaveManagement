using System.Linq.Expressions;
using HRLeaveManagement.Application.Contracts.Persistence.Common;
using HRLeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories.Common;

public class GenericRepository<TEntity, TContext>: IGenericRepository<TEntity> 
    where TEntity : BaseEntity, new()
    where TContext: DbContext, new()
{
    private readonly TContext _context;
    private DbSet<TEntity> Entity { get; set; }

    public GenericRepository()
    {
        _context = new TContext();
        Entity = _context.Set<TEntity>();
    }
    
    public async Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entity.AddAsync(entity, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Entity.Update(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Entity.Remove(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await _context.AddRangeAsync(entities, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? expression,
        CancellationToken cancellationToken)
    {
        return Task.Run(() => expression != null
            ? Entity.Where(expression).AsNoTracking().AsQueryable()
            : Entity.AsNoTracking().AsQueryable(), cancellationToken);
    }

    public async Task<IQueryable<TEntity>> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        return  await Task.Run(() => _context.Set<TEntity>().AsNoTracking().AsQueryable(), cancellationToken);
    }

    public Task<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken,
        Expression<Func<TEntity, bool>>? expression = null)
    {
        return Task.FromResult(expression != null
            ? Entity.Where(expression).AsNoTracking().AsQueryable()
            : Entity.AsNoTracking().AsQueryable());
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
    {
        return await Entity.AnyAsync(expression, cancellationToken);
    }
}