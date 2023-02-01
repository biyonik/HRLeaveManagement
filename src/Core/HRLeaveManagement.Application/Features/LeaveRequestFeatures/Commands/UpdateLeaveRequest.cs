using System.Text.RegularExpressions;
using AutoMapper;
using FluentValidation;
using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveRequest;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveRequestFeatures.Commands;

public class UpdateLeaveRequest
{
    public sealed record Command(LeaveRequestForUpdateDto LeaveRequestForUpdateDto) : ICommand<IResult>;
    
    public class Handler: ICommandHandler<Command, IResult>
    {
        public class UpdateLeaveRequestValidator : AbstractValidator<LeaveRequestForUpdateDto>
        {
            private readonly ILeaveTypeService _leaveTypeService;
            private readonly ILeaveRequestService _leaveRequestService;
            public UpdateLeaveRequestValidator(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService)
            {
                _leaveTypeService = leaveTypeService;
                _leaveRequestService = leaveRequestService;

                RuleFor(x => x.Id)
                    .MustAsync(LeaveRequestMustExist).WithMessage("{PropertyName} must be present")
                    .MustAsync(IsValidGuid).WithMessage("{PropertyName} must be a valid GUID!");

                RuleFor(x => x.StartDate)
                    .LessThan(p => p.EndTime).WithMessage("{PropertyName} must be before {ComparisonValue}");
                
                RuleFor(x => x.EndTime)
                    .GreaterThan(x => x.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");

                RuleFor(x => x.LeaveTypeId)
                    .MustAsync(LeaveTypeMustExist).WithMessage("{PropertyName} does not exist");
            }
            
            private async Task<bool> LeaveRequestMustExist(Guid id, CancellationToken cancellation)
            {
                var entity = await _leaveRequestService.GetByIdAsync(id, cancellation);
                return entity != null;
            }
            
            private async Task<bool> LeaveTypeMustExist(Guid id, CancellationToken cancellation)
            {
                var entity = await _leaveTypeService.GetByIdAsync(id, cancellation);
                return entity != null;
            }
            
            private async Task<bool> IsValidGuid(Guid id, CancellationToken cancellation)
            {
                Regex isGuid = 
                    new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
                bool isValid = isGuid.IsMatch(id.ToString());
                return isValid;
            }
        }
        
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IMapper _mapper;

        public Handler(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService, IMapper mapper)
        {
            _leaveTypeService = leaveTypeService;
            _leaveRequestService = leaveRequestService;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveRequestValidator(_leaveTypeService, _leaveRequestService);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestForUpdateDto, cancellationToken);
            if (validationResult.Errors.Any())
                return new ErrorDataResult<IList<string>>(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            
            var isExist = await _leaveRequestService.GetByIdAsync(request.LeaveRequestForUpdateDto.Id, cancellationToken);
            if (isExist is null) return new ErrorResult("No such leave request was found!");
            
            var mappedData = _mapper.Map<LeaveRequest>(request.LeaveRequestForUpdateDto);
            var result = await _leaveRequestService.UpdateAsync(mappedData, cancellationToken);
            
            if (result) return new SuccessResult("Leave request updated successfully.");
            return new ErrorResult("Leave request updated failed!");
        }
    }
}