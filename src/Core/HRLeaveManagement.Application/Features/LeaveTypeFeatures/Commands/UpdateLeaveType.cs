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

public class UpdateLeaveType
{
    public sealed record Command(
        LeaveTypeForUpdateDto LeaveTypeForAddDto    
    ) : ICommand<IResult>;

    public class Validator : AbstractValidator<Command>
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public Validator(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;

            RuleFor(p => p.LeaveTypeForAddDto.Id)
                .NotNull()
                .MustAsync(LeaveTypeExist);
            
            RuleFor(x => x.LeaveTypeForAddDto.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

            RuleFor(x => x.LeaveTypeForAddDto.DefaultDays)
                .GreaterThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .LessThan(1).WithMessage("{PropertyName cannot be less than 1}");

            RuleFor(x => x)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave type name is exists!");
            
            
        }

        private async Task<bool> LeaveTypeExist(Guid Id, CancellationToken cancellationToken) 
            => await _leaveTypeService.GetByIdAsync(Id, cancellationToken) != null;

        private async Task<bool> LeaveTypeNameUnique(Command command, CancellationToken cancellationToken) 
            => await _leaveTypeService.IsLeaveTypeUnique(command.LeaveTypeForAddDto.Name, cancellationToken);
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
            var isExist = await _leaveTypeService.GetByIdAsync(request.LeaveTypeForAddDto.Id, cancellationToken);
            if (isExist == null) throw new NotFoundException(nameof(LeaveType), request.LeaveTypeForAddDto.Id);

            var validator = new Validator(_leaveTypeService);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveType), request.LeaveTypeForAddDto.Id);
                throw new BadRequestException("Invalid Leave type", validationResult);
            }
            
            var mappedData = _mapper.Map<LeaveType>(request.LeaveTypeForAddDto);
            var result = await _leaveTypeService.UpdateAsync(mappedData, cancellationToken);
            
            if (result) return new SuccessResult("Leave type updated successfully.");
            return new ErrorResult("Leave type updated failed!");
        }
    }
}