using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Application.Features.LeaveTypeFeatures.Commands;

public class DeleteLeaveType
{
    public sealed record Command(
        Guid Id
    ) : ICommand<IResult>;
    
    public sealed class Handler: ICommandHandler<Command, IResult>
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public Handler(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _leaveTypeService.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(LeaveType), request.Id);
            
            var deleteResult = await _leaveTypeService.RemoveAsync(entity, cancellationToken);

            if (deleteResult) return new SuccessResult("Leave type removed successfully.");
            return new ErrorResult("Leave type remove failed!");
        }
    }
}