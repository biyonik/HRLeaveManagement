﻿using System.Linq.Expressions;
using AutoMapper;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Application.Messaging;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Features.LeaveTypeFeatures.Queries;

public sealed class GetAllLeaveTypes
{
    public sealed record Query(
        Expression<Func<LeaveType, bool>>? Expression = null
    ) : IQuery<Response>;

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
            var leaveTypes = await _leaveTypeService.GetAllAsync(cancellationToken, request.Expression);
            var mappedData = _mapper.Map<IReadOnlyList<LeaveTypeForListDto>>(leaveTypes);
            return new(mappedData);
        }
    }
}