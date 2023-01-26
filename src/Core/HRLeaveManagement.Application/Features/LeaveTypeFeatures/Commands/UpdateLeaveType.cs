using AutoMapper;
using FluentValidation;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveTypeFeatures.Commands;

public class UpdateLeaveType
{
    public sealed record Command(
        LeaveTypeForUpdateDto LeaveTypeForUpdateDto    
    ) : ICommand<IResult>;

    public class Validator : AbstractValidator<Command>
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public Validator(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;

            RuleFor(p => p.LeaveTypeForUpdateDto.Id)
                .NotNull()
                .MustAsync(LeaveTypeExist);
            
            RuleFor(x => x.LeaveTypeForUpdateDto.Name)
                .NotNull().WithMessage("Leave type name is required")
                .NotEmpty().WithMessage("Leave type name is required")
                .MaximumLength(70).WithMessage("Leave type name must be fewer than 70 characters");

            RuleFor(x => x.LeaveTypeForUpdateDto.DefaultDays)
                .GreaterThan(1).WithMessage("Default days cannot be less than 1")
                .LessThan(100).WithMessage("Default days cannot exceed 100");

            RuleFor(x => x)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave type name is exists!");
        }

        private async Task<bool> LeaveTypeExist(Guid Id, CancellationToken cancellationToken) 
            => await _leaveTypeService.GetByIdAsync(Id, cancellationToken) != null;

        private async Task<bool> LeaveTypeNameUnique(Command command, CancellationToken cancellationToken) 
            => await _leaveTypeService.IsLeaveTypeUnique(command.LeaveTypeForUpdateDto.Name, cancellationToken);
    }


    public sealed class Handler : ICommandHandler<Command, IResult>
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
            var isExist = await _leaveTypeService.GetByIdAsync(request.LeaveTypeForUpdateDto.Id, cancellationToken);
            if (isExist == null) return new ErrorResult("No such leave type was found!");

            var validator = new Validator(_leaveTypeService);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveType), request.LeaveTypeForUpdateDto.Id);
                return new ErrorDataResult<List<string>>(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
            
            var mappedData = _mapper.Map<LeaveType>(request.LeaveTypeForUpdateDto);
            var result = await _leaveTypeService.UpdateAsync(mappedData, cancellationToken);
            
            if (result) return new SuccessResult("Leave type updated successfully.");
            return new ErrorResult("Leave type updated failed!");
        }
    }
}