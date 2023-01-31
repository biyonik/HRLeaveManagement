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

public class UpdateLeaveAllocation
{
    public sealed record Command(
        LeaveAllocationForUpdateDto LeaveAllocationForUpdateDto
    ) : ICommand<IResult>;

    public class UpdateLeaveAllocationValidator : AbstractValidator<LeaveAllocationForUpdateDto>
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveAllocationService _leaveAllocationService;
        public UpdateLeaveAllocationValidator(ILeaveTypeService leaveTypeService, ILeaveAllocationService leaveAllocationService)
        {
            _leaveTypeService = leaveTypeService;
            _leaveAllocationService = leaveAllocationService;

            RuleFor(x => x.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisomValue}");

            RuleFor(x => x.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{ProperyName} must be after {ComparisonValue}");
            
            RuleFor(x => x.LeaveTypeId)
                .MustAsync(IsValidGuid).WithMessage("{PropertyName} must be a valid GUID!")
                .MustAsync(LeaveTypeMustExist).WithMessage("{PropertyName} does not exist");

            RuleFor(x => x.Id)
                .MustAsync(IsValidGuid).WithMessage("{PropertyName} must be a valid GUID!")
                .MustAsync(LeaveAllocationMustExist).WithMessage("{ProperyName} must be present.");
        }

        private async Task<bool> LeaveAllocationMustExist(Guid id, CancellationToken cancellation)
        {
            var entity = await _leaveAllocationService.GetByIdAsync(id, cancellation);
            return entity != null;
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
            var validator = new UpdateLeaveAllocationValidator(_leaveTypeService, _leaveAllocationService);
            var validationResult =
                await validator.ValidateAsync(request.LeaveAllocationForUpdateDto, cancellationToken);
            if (validationResult.Errors.Any())
                return new ErrorDataResult<IList<string>>(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            
            var isExist = await _leaveAllocationService.GetByIdAsync(request.LeaveAllocationForUpdateDto.Id, cancellationToken);
            if (isExist is null) return new ErrorResult("No such leave allocation was found!");
            
            var mappedData = _mapper.Map<LeaveAllocation>(request.LeaveAllocationForUpdateDto);
            var result = await _leaveAllocationService.UpdateAsync(mappedData, cancellationToken);
            
            if (result) return new SuccessResult("Leave allocation updated successfully.");
            return new ErrorResult("Leave allocation updated failed!");
        }
    }
}