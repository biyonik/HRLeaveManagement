using AutoMapper;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveAllocation;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.MappingProfiles;

public class LeaveAllocationProfile: Profile 
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocationForListDto, LeaveAllocation>().ReverseMap();
        CreateMap<LeaveAllocationForAddDto, LeaveAllocation>().ReverseMap();
        CreateMap<LeaveAllocationForUpdateDto, LeaveAllocation>().ReverseMap();
    }
}