using System.Linq.Expressions;
using AutoMapper;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveTypeFeatures.Queries;

public class GetLeaveType
{
    public sealed record Query(
        Expression<Func<LeaveType, bool>>? Expression
    ) : IQuery<IDataResult<LeaveTypeForDetailDto>>;

    public sealed record Response(
        LeaveTypeForDetailDto LeaveTypeForDetailDto    
    );
    
    public class Handler: IQueryHandler<Query, IDataResult<LeaveTypeForDetailDto>>
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IMapper _mapper;
        private readonly IAppLogger<Handler> _logger;

        public Handler(ILeaveTypeService leaveTypeService, IMapper mapper, IAppLogger<Handler> logger)
        {
            _leaveTypeService = leaveTypeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IDataResult<LeaveTypeForDetailDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeService.GetAsync(request.Expression, cancellationToken);
            if (leaveType == null)
            {
                _logger.LogWarning("Leave type not found!");
                return new ErrorDataResult<LeaveTypeForDetailDto>(null!, "Leave type not found!");
            }
            var mappedData = _mapper.Map<LeaveTypeForDetailDto>(leaveType);
            _logger.LogInformation("Leave types were retrieved");
            return new SuccessDataResult<LeaveTypeForDetailDto>(mappedData);
        }
    }
}