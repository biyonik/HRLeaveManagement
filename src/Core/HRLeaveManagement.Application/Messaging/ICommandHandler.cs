using MediatR;

namespace HRLeaveManagement.Application.Messaging;

public interface ICommandHandler<in TCommand, TResponse>: IRequestHandler<TCommand, TResponse> where TCommand : IRequest<TResponse>
{
    
}