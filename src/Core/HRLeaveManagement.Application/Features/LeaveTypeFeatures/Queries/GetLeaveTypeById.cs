using AutoMapper;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Application.Messaging;

namespace HRLeaveManagement.Application.Features.LeaveTypeFeatures.Queries;

public class GetLeaveTypeById
{
    public sealed record Query(
        Guid Id
    ) : IQuery<IDataResult<LeaveTypeForDetailDto>>;

    
    public class Handler: IQueryHandler<Query, IDataResult<LeaveTypeForDetailDto>>
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IMapper _mapper;

        public Handler(ILeaveTypeService leaveTypeService, IMapper mapper)
        {
            _leaveTypeService = leaveTypeService;
            _mapper = mapper;
        }

        public async Task<IDataResult<LeaveTypeForDetailDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeService.GetByIdAsync(request.Id, cancellationToken);
            if (leaveType == null) return new ErrorDataResult<LeaveTypeForDetailDto>("Leave type not found!");
            var mappedData = _mapper.Map<LeaveTypeForDetailDto>(leaveType);
            return new SuccessDataResult<LeaveTypeForDetailDto>(mappedData);
        }
    }
}