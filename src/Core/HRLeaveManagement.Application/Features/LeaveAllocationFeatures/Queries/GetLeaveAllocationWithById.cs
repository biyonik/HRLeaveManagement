using AutoMapper;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveAllocation;
using HRLeaveManagement.Application.Messaging;

namespace HRLeaveManagement.Application.Features.LeaveAllocationFeatures.Queries;

public class GetLeaveAllocationWithById
{
    public sealed record Query(Guid id) : IQuery<IDataResult<LeaveAllocationForListDto>>;
    
    public class Handler: IQueryHandler<Query, IDataResult<LeaveAllocationForListDto>>
    {
        private readonly ILeaveAllocationService _leaveAllocationService;
        private readonly IMapper _mapper;

        public Handler(ILeaveAllocationService leaveAllocationService, IMapper mapper)
        {
            _leaveAllocationService = leaveAllocationService;
            _mapper = mapper;
        }

        public async Task<IDataResult<LeaveAllocationForListDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationService.GetByIdAsync(request.id, cancellationToken);
            if (leaveAllocation == null)
                return new ErrorDataResult<LeaveAllocationForListDto>("Leave allocation not found!");

            var mappedData = _mapper.Map<LeaveAllocationForListDto>(leaveAllocation);
            return new SuccessDataResult<LeaveAllocationForListDto>(mappedData);
        }
    }
}