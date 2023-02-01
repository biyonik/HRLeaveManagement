using System.Text.RegularExpressions;
using AutoMapper;
using FluentValidation;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveAllocation;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveAllocationFeatures.Commands;

public class CreateLeaveAllocation
{
    public sealed record Command(LeaveAllocationForAddDto LeaveAllocationForAddDto) : ICommand<IResult>;

    public class CreateLeaveAllocationValidator : AbstractValidator<LeaveAllocationForAddDto>
    {
        private readonly ILeaveTypeService _leaveTypeService;
        public CreateLeaveAllocationValidator(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;

            RuleFor(x => x.LeaveTypeId)
                .MustAsync(IsValidGuid).WithMessage("{PropertyName} must be a valid GUID!")
                .MustAsync(LeaveTypeMustExist).WithMessage("{PropertyName} does not exist");
        }

        private async Task<bool> IsValidGuid(Guid id, CancellationToken cancellation)
        {
            Regex isGuid = 
                new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
            bool isValid = isGuid.IsMatch(id.ToString());
            return isValid;
        }

        private async Task<bool> LeaveTypeMustExist(Guid id, CancellationToken cancellation)
        {
            var entity = await _leaveTypeService.GetByIdAsync(id, cancellation);
            return entity != null;
        }
    }

    public class Handler: ICommandHandler<Command, IResult>
    {
        private readonly ILeaveAllocationService _leaveAllocationService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IMapper _mapper;
        private readonly IAppLogger<Handler> _logger;

        public Handler(ILeaveAllocationService leaveAllocationService, IMapper mapper, IAppLogger<Handler> logger, ILeaveTypeService leaveTypeService)
        {
            _leaveAllocationService = leaveAllocationService;
            _mapper = mapper;
            _logger = logger;
            _leaveTypeService = leaveTypeService;
        }

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationValidator(_leaveTypeService);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationForAddDto, cancellationToken);
            if (validationResult.Errors.Any())
                return new ErrorDataResult<IList<string>>(validationResult.Errors.Select(x => x.ErrorMessage).ToList());

            var leaveType = await _leaveTypeService.GetByIdAsync(request.LeaveAllocationForAddDto.LeaveTypeId, cancellationToken);
            if (leaveType != null) request.LeaveAllocationForAddDto.LeaveTypeId = leaveType.Id;

            var mappedData = _mapper.Map<LeaveAllocation>(request.LeaveAllocationForAddDto);
            var result = await _leaveAllocationService.CreateAsync(mappedData, cancellationToken);
            if (result)
            {
                _logger.LogInformation("New leave allocation created successfully.");
                return new SuccessResult("Leave allocation added successfully.");
            }
            _logger.LogWarning("New leave allocation create failed!");
            return new ErrorResult("Leave allocation added failed!");
        }
    }
}