namespace HRLeaveManagement.Application.DataTransformationObjects.LeaveAllocation;

public class LeaveAllocationForAddDto
{
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    
    public Guid LeaveTypeId { get; set; }
    public Guid? EmployeeId { get; set; } = Guid.Empty;
}