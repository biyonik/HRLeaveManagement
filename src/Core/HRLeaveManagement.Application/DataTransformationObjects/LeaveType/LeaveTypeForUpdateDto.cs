namespace HRLeaveManagement.Application.DataTransformationObjects.LeaveType;

public class LeaveTypeForUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int DefaultDays { get; set; }
}