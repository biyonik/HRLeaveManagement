using MediatR;

namespace HRLeaveManagement.Application.Messaging;

public interface ICommand<out TResponse>: IRequest<TResponse>
{
    
}