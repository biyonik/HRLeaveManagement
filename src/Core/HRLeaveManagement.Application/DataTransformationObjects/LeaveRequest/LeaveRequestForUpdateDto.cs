namespace HRLeaveManagement.Application.DataTransformationObjects.LeaveRequest;

public class LeaveRequestForUpdateDto
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndTime { get; set; }
    public Guid LeaveTypeId { get; set; }
    public string? RequestComments { get; set; } = string.Empty;
    public bool Cancelled { get; set; }
}