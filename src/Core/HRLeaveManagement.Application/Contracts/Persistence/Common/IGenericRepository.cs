using System.Linq.Expressions;
using HRLeaveManagement.Domain.Common;

namespace HRLeaveManagement.Application.Contracts.Persistence.Common;

public interface IGenericRepository<T> where T: BaseEntity, new()
{
    Task<bool> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(T entity, CancellationToken cancellationToken);
    Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>>? expression, CancellationToken cancellationToken);
    Task<IQueryable<T>> GetByIdAsync(Guid Id, CancellationToken cancellationToken); 
    Task<IQueryable<T>> GetAllAsync(CancellationToken cancellationToken,
        Expression<Func<T, bool>>? expression = null);

    Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
}