using HRLeaveManagement.Application.Contracts.Persistence.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.Services.Common;

namespace HRLeaveManagement.Persistence.Services;

public class LeaveRequestService: GenericService<LeaveRequest>, ILeaveRequestService
{
    public LeaveRequestService(IGenericRepository<LeaveRequest> genericRepository) : base(genericRepository)
    {
    }
}