namespace HRLeaveManagement.Application.DataTransformationObjects.LeaveRequest;

public class LeaveRequestForAddDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndTime { get; set; }
    public Guid LeaveTypeId { get; set; }
    public string? RequestComments { get; set; } = string.Empty;
}