using HRLeaveManagement.Application.Contracts.Persistence.Common;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository: IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation?> GetLeaveAllocationWithDetails(Guid Id);
    Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails();
    Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails(Guid userId);
    Task<bool> AllocationExists(Guid userId, Guid leaveTypeId, int period);
    Task AddAllocations(IEnumerable<LeaveAllocation> allocations);
    Task<LeaveAllocation?> GetUserAllocations(Guid userId, Guid leaveTypeId);
}