using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(AssemblyReference).Assembly);
        services.AddAutoMapper(typeof(AssemblyReference).Assembly);
        
        return services;
    }
}