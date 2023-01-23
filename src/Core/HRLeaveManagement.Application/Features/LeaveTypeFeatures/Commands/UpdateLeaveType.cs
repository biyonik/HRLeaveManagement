using AutoMapper;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveTypeFeatures.Commands;

public class UpdateLeaveType
{
    public sealed record Command(
        LeaveTypeForUpdateDto LeaveTypeForAddDto    
    ) : ICommand<IResult>;


    public sealed class Handler : ICommandHandler<Command, IResult>
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
            var isExist = await _leaveTypeService.GetByIdAsync(request.LeaveTypeForAddDto.Id, cancellationToken);
            if (isExist == null) return new SuccessResult("Leave type not found!");
            
            var mappedData = _mapper.Map<LeaveType>(request.LeaveTypeForAddDto);
            var result = await _leaveTypeService.UpdateAsync(mappedData, cancellationToken);
            if (result) return new SuccessResult("Leave type updated successfully.");
            return new ErrorResult("Leave type updated failed!");
        }
    }
}