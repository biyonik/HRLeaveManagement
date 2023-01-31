using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveRequestFeatures.Commands;

public class DeleteLeaveRequest
{
    public sealed record Command(Guid id) : ICommand<IResult>;
    
    public class Handler: ICommandHandler<Command, IResult>
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IAppLogger<Handler> _logger;
        
        public Handler(ILeaveRequestService leaveRequestService, IAppLogger<Handler> logger)
        {
            _leaveRequestService = leaveRequestService;
            _logger = logger;
        }

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _leaveRequestService.GetByIdAsync(request.id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(LeaveRequest), request.id);

            var deleteResult = await _leaveRequestService.RemoveAsync(entity, cancellationToken);

            if (deleteResult)
            {
                _logger.LogInformation($"Leave request deleted successfully. Leave request id: {entity.Id}");
                return new SuccessResult("Leave request deleted successfully");
            }
            
            _logger.LogWarning("Leave request deleted failed!");
            return new ErrorResult("Leave request deleted failed!");
        }
    }
}