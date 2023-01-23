﻿using AutoMapper;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Application.Messaging;

namespace HRLeaveManagement.Application.Features.LeaveTypeFeatures.Queries;

public sealed class GetAllLeaveTypes
{
    public sealed record Query : IQuery<Response>;

    public sealed record Response(
        IReadOnlyList<LeaveTypeForListDto?> LeaveTypeForListDtos
    );
    
    public sealed class Handler: IQueryHandler<Query, Response>
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IMapper _mapper;

        public Handler(ILeaveTypeService leaveTypeService, IMapper mapper)
        {
            _leaveTypeService = leaveTypeService;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _leaveTypeService.GetAllAsync(cancellationToken);
            var mappedData = _mapper.Map<IReadOnlyList<LeaveTypeForListDto>>(leaveTypes);
            return new(mappedData);
        }
    }
}