using AutoMapper;
using FluentValidation;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveRequest;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveRequestFeatures.Commands;

public class CreateLeaveRequest
{
    public sealed record Command(LeaveRequestForAddDto LeaveRequestForAddDto) : ICommand<IResult>;
    
    public class Handler: ICommandHandler<Command, IResult>
    {
        public class CreateLeaveRequestValidator : AbstractValidator<LeaveRequestForAddDto>
        {
            public CreateLeaveRequestValidator()
            {
                
            }
        }

        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IMapper _mapper;
        private readonly IAppLogger<Handler> _logger;

        public Handler(ILeaveRequestService leaveRequestService, IMapper mapper, IAppLogger<Handler> logger)
        {
            _leaveRequestService = leaveRequestService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveRequestForAddDto, cancellationToken);
            if (validationResult.Errors.Any())
                return new ErrorDataResult<IList<string>>(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            
            var mappedData = _mapper.Map<LeaveRequest>(request.LeaveRequestForAddDto);
            var result = await _leaveRequestService.CreateAsync(mappedData, cancellationToken);
            if (result)
            {
                _logger.LogInformation("New leave request created successfully.");
                return new SuccessResult("Leave request added successfully.");
            }
            _logger.LogWarning("New leave request create failed!");
            return new ErrorResult("Leave request added failed!");
        }
    }
}