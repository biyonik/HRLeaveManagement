using AutoMapper;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveRequest;
using HRLeaveManagement.Application.Messaging;

namespace HRLeaveManagement.Application.Features.LeaveRequestFeatures.Queries;

public class GetLeaveRequestById
{
    public sealed record Query(Guid id) : ICommand<IDataResult<LeaveRequestForListDto>>;
    
    public class Handler: ICommandHandler<Query, IDataResult<LeaveRequestForListDto>>
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IMapper _mapper;
        private readonly IAppLogger<Handler> _logger;

        public Handler(ILeaveRequestService leaveRequestService, IMapper mapper, IAppLogger<Handler> logger)
        {
            _leaveRequestService = leaveRequestService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IDataResult<LeaveRequestForListDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _leaveRequestService.GetByIdAsync(request.id, cancellationToken);
            if (entity == null) return new ErrorDataResult<LeaveRequestForListDto>("Leave type not found!");

            var mappedData = _mapper.Map<LeaveRequestForListDto>(entity);
            _logger.LogInformation($"Leave request fetched. Id: {entity.Id}");
            return new SuccessDataResult<LeaveRequestForListDto>(mappedData);
        }
    }
}