namespace HRLeaveManagement.Application.DataTransformationObjects.LeaveAllocation;

public class LeaveAllocationForUpdateDto
{
    public Guid Id { get; set; }
    public int NumberOfDays { get; set; }
    public Guid LeaveTypeId { get; set; }
    public int Period { get; set; }
}