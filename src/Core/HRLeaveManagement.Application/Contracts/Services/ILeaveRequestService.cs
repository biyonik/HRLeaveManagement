using HRLeaveManagement.Application.Contracts.Services.Common;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Services;

public interface ILeaveRequestService: IGenericService<LeaveRequest>
{
    Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestWithDetails(Guid Id);
    Task<IReadOnlyList<LeaveRequest>?> GetLeaveRequestsWithDetails();
    Task<IReadOnlyList<LeaveRequest>?> GetLeaveRequestsWithDetailsByUserId(Guid userId);
}