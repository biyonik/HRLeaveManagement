using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DataAccess.Context;
using HRLeaveManagement.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository: GenericRepository<LeaveRequest, AppDbContext>, ILeaveRequestRepository
{
    public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestWithDetails(Guid Id)
    {
        var queryable = await GetAllAsync(default);
        var leaveRequests = await queryable.Include(q => q.LeaveType).ToListAsync();
        return leaveRequests;
    }

    public async Task<IReadOnlyList<LeaveRequest>?> GetLeaveRequestsWithDetails()
    {var queryable = await GetAllAsync(default);
        return await (queryable).Include(q => q.LeaveType).ToListAsync();
    }

    public async Task<IReadOnlyList<LeaveRequest>?> GetLeaveRequestsWithDetailsByUserId(Guid userId)
    {
        var queryable = await GetAllAsync(default, q => q.RequestingEmployeeId == userId);
        var leaveRequests = await queryable
            .Include(q => q.LeaveType).ToListAsync();
        return leaveRequests;
    }
}