using HRLeaveManagement.Application.Contracts.Persistence.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.Services.Common;

namespace HRLeaveManagement.Persistence.Services;

public class LeaveTypeService: GenericService<LeaveType>, ILeaveTypeService
{
    public LeaveTypeService(IGenericRepository<LeaveType> genericRepository) : base(genericRepository)
    {
    }
}