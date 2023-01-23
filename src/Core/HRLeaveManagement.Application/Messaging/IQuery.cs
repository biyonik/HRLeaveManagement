using MediatR;

namespace HRLeaveManagement.Application.Messaging;

public interface IQuery<out TResponse>: IRequest<TResponse>
{
    
}