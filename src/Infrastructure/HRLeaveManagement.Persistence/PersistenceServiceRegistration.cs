using HRLeaveManagement.Persistence.DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();
        return services;
    }
}