using HRLeaveManagement.Application.Contracts.Persistence.Common;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository: IGenericRepository<LeaveRequest>
{
    Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestWithDetails(Guid Id);
    Task<IReadOnlyList<LeaveRequest>?> GetLeaveRequestsWithDetails();
    Task<IReadOnlyList<LeaveRequest>?> GetLeaveRequestsWithDetailsByUserId(Guid userId);
}