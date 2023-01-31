using AutoMapper;
using FluentValidation;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveTypeFeatures.Commands;

public class CreateLeaveType
{
    public sealed record Command(
        LeaveTypeForAddDto LeaveTypeForAddDto    
    ) : ICommand<IResult>;

    public class CommandValidator : AbstractValidator<LeaveTypeForAddDto>
    {
        private readonly ILeaveTypeService _leaveTypeService;
        public CommandValidator(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
            
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Leave type name is required")
                .NotEmpty().WithMessage("Leave type name is required")
                .MaximumLength(70).WithMessage("Leave type name must be fewer than 70 characters");

            RuleFor(x => x.DefaultDays)
                .GreaterThan(1).WithMessage("Default days cannot be less than 1")
                .LessThan(100).WithMessage("Default days cannot exceed 100");

            RuleFor(x => x)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave type name is exists!");
        }

        private async Task<bool> LeaveTypeNameUnique(LeaveTypeForAddDto leaveTypeForAddDto, CancellationToken cancellationToken) 
            => !await _leaveTypeService.IsLeaveTypeUnique(leaveTypeForAddDto.Name, cancellationToken);
    }


    public sealed class Handler: ICommandHandler<Command, IResult>
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

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var validator = new CommandValidator(_leaveTypeService);
            var validationResult = await validator.ValidateAsync(request.LeaveTypeForAddDto, cancellationToken);
            if (validationResult.Errors.Any())
                return new ErrorDataResult<IList<string>>(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            
            var mappedData = _mapper.Map<LeaveType>(request.LeaveTypeForAddDto);
            
            var result = await _leaveTypeService.CreateAsync(mappedData, cancellationToken);

            if (result)
            {
                _logger.LogInformation("New leave type created successfully.");
                return new SuccessResult("Leave type added successfully.");
            }
            _logger.LogWarning("New leave type create failed!");
            return new ErrorResult("Leave type added failed!");
        }
    }
}