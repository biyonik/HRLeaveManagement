using HRLeaveManagement.Application.Contracts.Persistence.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.Services.Common;

namespace HRLeaveManagement.Persistence.Services;

public class LeaveAllocationService: GenericService<LeaveAllocation>, ILeaveAllocationService
{
    public LeaveAllocationService(IGenericRepository<LeaveAllocation> genericRepository) : base(genericRepository)
    {
    }
}