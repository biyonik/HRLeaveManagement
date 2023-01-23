namespace HRLeaveManagement.Application.DataTransformationObjects.LeaveType;

public sealed class LeaveTypeForListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int DefaultDays { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}