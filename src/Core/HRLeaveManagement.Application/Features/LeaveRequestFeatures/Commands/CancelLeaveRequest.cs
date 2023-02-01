using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.Messaging;

namespace HRLeaveManagement.Application.Features.LeaveRequestFeatures.Commands;

public class CancelLeaveRequest
{
    public sealed record Command(Guid id) : ICommand<IResult>;
    
    public class Handler: ICommandHandler<Command, IResult>
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public Handler(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestService.GetByIdAsync(request.id, cancellationToken);
            if (leaveRequest is null) return new ErrorResult("Leave request not found!");

            leaveRequest.Cancelled = true;

            var result = await _leaveRequestService.UpdateAsync(leaveRequest, cancellationToken);
            return result
                ? new SuccessResult("Leave request cancelled successfully.")
                : new ErrorResult("Leave request cancelled failed!");
        }
    }
}