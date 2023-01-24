using System.Linq.Expressions;
using AutoMapper;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveTypeFeatures.Queries;

public class GetLeaveType
{
    public sealed record Query(
        Expression<Func<LeaveType, bool>>? Expression
    ) : IQuery<Response>;

    public sealed record Response(
        LeaveTypeForDetailDto LeaveTypeForDetailDto    
    );
    
    public class Handler: IQueryHandler<Query, Response>
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IMapper _mapper;

        public Handler(ILeaveTypeService leaveTypeService, IMapper mapper)
        {
            _leaveTypeService = leaveTypeService;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeService.GetAsync(request.Expression, cancellationToken);
            var mappedData = _mapper.Map<LeaveTypeForDetailDto>(leaveType);
            return new(mappedData);
        }
    }
}