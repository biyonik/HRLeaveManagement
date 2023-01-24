using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Infrastructure.LoggingService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        
        return services;
    }
}