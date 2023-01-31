using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveAllocationFeatures.Commands;

public class DeleteLeaveAllocation
{
    public sealed record Command(Guid id) : ICommand<IResult>;
    
    public class Handler: ICommandHandler<Command, IResult>
    {
        private readonly ILeaveAllocationService _leaveAllocationService;
        private readonly IAppLogger<Handler> _logger;

        public Handler(ILeaveAllocationService leaveAllocationService, IAppLogger<Handler> logger)
        {
            _leaveAllocationService = leaveAllocationService;
            _logger = logger;
        }

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _leaveAllocationService.GetByIdAsync(request.id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(LeaveAllocation), request.id);
            
            var deleteResult = await _leaveAllocationService.RemoveAsync(entity, cancellationToken);

            if (deleteResult)
            {
                _logger.LogInformation($"Leave allocation removed successfully. Leave Allocation Info: {entity.Id}");
                return new SuccessResult("Leave allocation removed successfully.");
            }
            _logger.LogWarning("Leave allocation remove failed!");
            return new ErrorResult("Leave allocation remove failed!");
        }
    }
}