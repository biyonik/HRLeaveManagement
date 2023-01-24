using HRLeaveManagement.Domain.Common;

namespace HRLeaveManagement.Domain;

public sealed class LeaveAllocation: BaseEntity
{
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    
    public Guid LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }

    public Guid EmployeeId { get; set; } = Guid.Empty;
}