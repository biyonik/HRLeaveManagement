using AutoMapper;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.MappingProfiles;

public class LeaveTypeProfile: Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeForListDto, LeaveType>().ReverseMap();
        CreateMap<LeaveTypeForDetailDto, LeaveType>().ReverseMap();
        CreateMap<LeaveTypeForAddDto, LeaveType>().ReverseMap();
        CreateMap<LeaveTypeForUpdateDto, LeaveType>().ReverseMap();
    }
}