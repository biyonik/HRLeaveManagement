using AutoMapper;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveRequest;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveRequestFeatures.Queries;

public class GetAllLeaveRequest
{
    public sealed record Query : IQuery<IDataResult<IReadOnlyList<LeaveRequestForListDto>>>;
    
    public class Handler: IQueryHandler<Query, IDataResult<IReadOnlyList<LeaveRequestForListDto>>>
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IMapper _mapper;
        private readonly IAppLogger<Handler> _logger;

        public Handler(ILeaveRequestService leaveRequestService, IAppLogger<Handler> logger, IMapper mapper)
        {
            _leaveRequestService = leaveRequestService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IDataResult<IReadOnlyList<LeaveRequestForListDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var leaveRequests = await _leaveRequestService.GetAllAsync(cancellationToken);
            if (leaveRequests == null)
                return new ErrorDataResult<IReadOnlyList<LeaveRequestForListDto>>("Have not any leave types!");

            var mappedData = _mapper.Map<IReadOnlyList<LeaveRequestForListDto>>(leaveRequests);
            _logger.LogInformation("All leave requests were retrieved successfully.");
            return new SuccessDataResult<IReadOnlyList<LeaveRequestForListDto>>(mappedData);
        }
    }
}