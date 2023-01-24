using System.Linq.Expressions;
using HRLeaveManagement.Application.Contracts.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories.Common;

public class GenericRepository<TEntity, TContext>: IGenericRepository<TEntity> 
    where TEntity : class, new()
    where TContext: DbContext, new()
{
    private TContext _context;
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

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
    {
        return expression != null
            ? Entity.Where(expression).FirstOrDefaultAsync(cancellationToken)
            : Entity.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await Entity.FindAsync(Id);
    }

    public Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? expression = null)
    {
        return Task.FromResult(expression != null
            ? Entity.Where(expression).ToListAsync(cancellationToken: cancellationToken) as IReadOnlyList<TEntity>
            : Entity.ToListAsync(cancellationToken) as IReadOnlyList<TEntity>);
    }
}