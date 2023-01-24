using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DataAccess.Context;
using HRLeaveManagement.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository: GenericRepository<LeaveAllocation, AppDbContext>, ILeaveAllocationRepository
{
    public async Task<LeaveAllocation?> GetLeaveAllocationWithDetails(Guid Id)
    {
        var allocation = await (await GetByIdAsync(Id, default)).FirstOrDefaultAsync(x => x.Id == Id);
        return allocation;
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        var allocations = await (await GetAllAsync(default)).Include(q => q.LeaveType).ToListAsync();
        return allocations;
    }

    public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails(Guid userId)
    {
        var allocations = await (await GetAllAsync(default, x => x.EmployeeId == userId)).Include(p => p.LeaveType)
            .ToListAsync();
        return allocations;
    }

    public async Task<bool> AllocationExists(Guid userId, Guid leaveTypeId, int period)
    {
        var isExist = await AnyAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId && x.Period == period,
            default);
        return isExist;
    }

    public async Task AddAllocations(IEnumerable<LeaveAllocation> allocations)
    {
        await AddRangeAsync(allocations, default);
    }

    public async Task<LeaveAllocation?> GetUserAllocations(Guid userId, Guid leaveTypeId)
    {
        var allocation = await (await GetAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId, default))
            .FirstOrDefaultAsync(default);
        return allocation;
    }
}