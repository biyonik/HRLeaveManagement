using AutoMapper;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.BlazorUI.Models.LeaveTypes;

namespace HRLeaveManagement.BlazorUI.Profiles.AutoMapper;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<LeaveTypeForListDto, LeaveTypeViewModel>().ReverseMap();
    }
}