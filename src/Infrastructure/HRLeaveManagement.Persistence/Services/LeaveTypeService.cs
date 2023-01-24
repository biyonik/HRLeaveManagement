using HRLeaveManagement.Application.Contracts.Persistence.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.Services.Common;

namespace HRLeaveManagement.Persistence.Services;

public class LeaveTypeService: GenericService<LeaveType>, ILeaveTypeService
{
    private readonly IGenericRepository<LeaveType> _genericRepository;
    public LeaveTypeService(IGenericRepository<LeaveType> genericRepository) : base(genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<bool> IsLeaveTypeUnique(string leaveTypeName, CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetAsync(x => x.Name == leaveTypeName, cancellationToken) != null;
    }
}