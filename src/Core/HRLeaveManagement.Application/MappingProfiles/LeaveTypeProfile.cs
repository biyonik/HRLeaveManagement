using AutoMapper;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.MappingProfiles;

public class LeaveTypeProfile: Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeForListDto, LeaveType>().ReverseMap();
    }
}