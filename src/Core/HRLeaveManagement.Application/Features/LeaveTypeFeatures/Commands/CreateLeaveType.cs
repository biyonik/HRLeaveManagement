using AutoMapper;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveTypeFeatures.Commands;

public class CreateLeaveType
{
    public sealed record Command(
        LeaveTypeForAddDto LeaveTypeForAddDto    
    ) : ICommand<IResult>;
    
    
    public sealed class Handler: ICommandHandler<Command, IResult>
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IMapper _mapper;

        public Handler(ILeaveTypeService leaveTypeService, IMapper mapper)
        {
            _leaveTypeService = leaveTypeService;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var mappedData = _mapper.Map<LeaveType>(request.LeaveTypeForAddDto);
            var result = await _leaveTypeService.CreateAsync(mappedData, cancellationToken);
            if (result) return new SuccessResult("Leave type added successfully");
            return new ErrorResult("Leave type added failed!");
        }
    }
}