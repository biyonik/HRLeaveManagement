using HRLeaveManagement.Domain.Common;

namespace HRLeaveManagement.Domain;

public sealed class LeaveType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}