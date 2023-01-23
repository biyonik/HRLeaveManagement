using HRLeaveManagement.Application.Messaging;

namespace HRLeaveManagement.Application.Features.LeaveTypeFeatures.Queries;

public sealed class GetAllLeaveTypes
{
    public sealed record Query : IQuery<Response>;

    public sealed record Response;
    
    public sealed class Handler: IQueryHandler<Query, Response>
    {
        public Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}