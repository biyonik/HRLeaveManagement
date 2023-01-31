using AutoMapper;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveAllocation;
using HRLeaveManagement.Application.Messaging;

namespace HRLeaveManagement.Application.Features.LeaveAllocationFeatures.Queries;

public class GetAllLeaveAllocations
{
    public sealed record Query : IQuery<IDataResult<IEnumerable<LeaveAllocationForListDto>>>;
    
    public class Handler: IQueryHandler<Query, IDataResult<IEnumerable<LeaveAllocationForListDto>>>
    {
        private readonly ILeaveAllocationService _leaveAllocationService;
        private readonly IMapper _mapper;
        private readonly IAppLogger<Handler> _logger;

        public Handler(ILeaveAllocationService leaveAllocationService, IMapper mapper, IAppLogger<Handler> logger)
        {
            _leaveAllocationService = leaveAllocationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IDataResult<IEnumerable<LeaveAllocationForListDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await _leaveAllocationService.GetAllAsync(cancellationToken);
            if (leaveAllocations == null)
                return new ErrorDataResult<IEnumerable<LeaveAllocationForListDto>>("Have not any leave allocations!");

            var mappedData = _mapper.Map<IReadOnlyList<LeaveAllocationForListDto>>(leaveAllocations);
            _logger.LogInformation("All leave allocations were retrieved successfully.");
            return new SuccessDataResult<IEnumerable<LeaveAllocationForListDto>>(mappedData);
        }
    }
}