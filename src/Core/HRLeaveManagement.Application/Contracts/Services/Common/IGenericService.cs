using System.Linq.Expressions;

namespace HRLeaveManagement.Application.Contracts.Services.Common;

public interface IGenericService<TEntity> 
    where TEntity: class, new()
{
    Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(Guid Id, CancellationToken cancellationToken); 
    Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken cancellationToken,
        Expression<Func<TEntity, bool>>? expression = null);
}