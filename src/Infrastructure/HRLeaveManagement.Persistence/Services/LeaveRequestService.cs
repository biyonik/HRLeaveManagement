using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.Services.Common;

namespace HRLeaveManagement.Persistence.Services;

public class LeaveRequestService: GenericService<LeaveRequest>, ILeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository) : base(leaveRequestRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestWithDetails(Guid Id)
    {
        return await _leaveRequestRepository.GetLeaveRequestWithDetails(Id);
    }

    public async Task<IReadOnlyList<LeaveRequest>?> GetLeaveRequestsWithDetails()
    {
        return await _leaveRequestRepository.GetLeaveRequestsWithDetails();
    }

    public async Task<IReadOnlyList<LeaveRequest>?> GetLeaveRequestsWithDetailsByUserId(Guid userId)
    {
        return await _leaveRequestRepository.GetLeaveRequestsWithDetailsByUserId(userId);
    }
}