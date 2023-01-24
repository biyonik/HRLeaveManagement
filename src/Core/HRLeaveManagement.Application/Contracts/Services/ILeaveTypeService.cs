using HRLeaveManagement.Application.Contracts.Services.Common;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Services;

public interface ILeaveTypeService: IGenericService<LeaveType>
{
    Task<bool> IsLeaveTypeUnique(string leaveTypeName, CancellationToken cancellationToken = default);
}