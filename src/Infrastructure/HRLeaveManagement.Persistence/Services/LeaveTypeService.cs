using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Services;

public class LeaveTypeService: GenericService<LeaveType>, ILeaveTypeService
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository) : base(leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<bool> IsLeaveTypeUnique(string leaveTypeName, CancellationToken cancellationToken = default)
    {
        return await (await _leaveTypeRepository.GetAsync(x => x.Name == leaveTypeName, cancellationToken)).FirstOrDefaultAsync(cancellationToken: cancellationToken) != null;
    }
}