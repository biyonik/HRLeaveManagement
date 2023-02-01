using AutoMapper;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveRequest;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.MappingProfiles;

public class LeaveRequestProfile: Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequestForListDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequestForDetailDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequestForAddDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequestForUpdateDto, LeaveRequest>().ReverseMap();

    }
}