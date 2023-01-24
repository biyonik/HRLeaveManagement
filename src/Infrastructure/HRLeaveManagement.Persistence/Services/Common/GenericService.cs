using System.Linq.Expressions;
using HRLeaveManagement.Application.Contracts.Persistence.Common;
using HRLeaveManagement.Application.Contracts.Services.Common;
using HRLeaveManagement.Domain.Common;

namespace HRLeaveManagement.Persistence.Services.Common;

public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity, new()
{
    private readonly IGenericRepository<TEntity> _genericRepository;

    public GenericService(IGenericRepository<TEntity> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return await _genericRepository.CreateAsync(entity, cancellationToken);
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return await _genericRepository.UpdateAsync(entity, cancellationToken);
    }

    public async Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return await _genericRepository.RemoveAsync(entity, cancellationToken);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
    {
        return await _genericRepository.GetAsync(expression, cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await _genericRepository.GetByIdAsync(Id, cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? expression = null)
    {
        return await _genericRepository.GetAllAsync(cancellationToken, expression);
    }
}