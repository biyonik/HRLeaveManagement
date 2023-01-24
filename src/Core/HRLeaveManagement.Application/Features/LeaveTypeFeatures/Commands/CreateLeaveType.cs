using AutoMapper;
using FluentValidation;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
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

    public class CommandValidator : AbstractValidator<Command>
    {
        private readonly ILeaveTypeService _leaveTypeService;
        public CommandValidator(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
            
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

        private async Task<bool> LeaveTypeNameUnique(Command command, CancellationToken cancellationToken)
        {
            var  entity = await _leaveTypeService.GetAsync(x => x.Name == command.LeaveTypeForAddDto.Name, cancellationToken);
            return entity != null;
        }
    }


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
            var validator = new CommandValidator(_leaveTypeService);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any()) throw new BadRequestException("Invalid LeaveType", validationResult);
            
            var mappedData = _mapper.Map<LeaveType>(request.LeaveTypeForAddDto);
            
            var result = await _leaveTypeService.CreateAsync(mappedData, cancellationToken);
            
            if (result) return new SuccessResult("Leave type added successfully");
            return new ErrorResult("Leave type added failed!");
        }
    }
}