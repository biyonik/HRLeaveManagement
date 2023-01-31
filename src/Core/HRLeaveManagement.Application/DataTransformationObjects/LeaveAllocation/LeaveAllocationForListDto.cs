using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;

namespace HRLeaveManagement.Application.DataTransformationObjects.LeaveAllocation;

public class LeaveAllocationForListDto
{
    public Guid Id { get; set; }
    public int NumberOfDays { get; set; }
    public LeaveTypeForListDto LeaveType { get; set; }
    public Guid LeaveTypeId { get; set; }
    public int Period { get; set; }
}