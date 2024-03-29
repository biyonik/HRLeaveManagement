﻿using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;

namespace HRLeaveManagement.Application.DataTransformationObjects.LeaveRequest;

public class LeaveRequestForListDto
{
    public Guid? RequestingEmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
    public LeaveTypeForListDto? LeaveType { get; set; }
}