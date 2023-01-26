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

public sealed class GetAllLeaveTypes
{
    public sealed record Query(
        Expression<Func<LeaveType, bool>>? Expression = null
    ) : IQuery<IDataResult<IReadOnlyList<LeaveTypeForListDto?>>>;
    
    public sealed class Handler: IQueryHandler<Query, IDataResult<IReadOnlyList<LeaveTypeForListDto?>>>
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

        public async Task<IDataResult<IReadOnlyList<LeaveTypeForListDto?>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _leaveTypeService.GetAllAsync(cancellationToken, request.Expression);
            if (leaveTypes == null) return new ErrorDataResult<IReadOnlyList<LeaveTypeForListDto?>>("Have not any leave types!");
            
            var mappedData = _mapper.Map<IReadOnlyList<LeaveTypeForListDto>>(leaveTypes);
            _logger.LogInformation("All leave types were retrieved successfully.");
            return new SuccessDataResult<IReadOnlyList<LeaveTypeForListDto?>>(mappedData);

        }
    }
}