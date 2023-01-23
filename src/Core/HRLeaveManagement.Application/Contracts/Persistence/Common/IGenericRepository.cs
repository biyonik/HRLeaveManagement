using System.Linq.Expressions;

namespace HRLeaveManagement.Application.Contracts.Persistence.Common;

public interface IGenericRepository<T> where T: class, new()
{
    Task<bool> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(T entity, CancellationToken cancellationToken);
    Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(Guid Id, CancellationToken cancellationToken); 
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken,
        Expression<Func<T, bool>>? expression = null);
}