using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Contracts.Persistence.Common;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.Contracts.Services.Common;
using HRLeaveManagement.Persistence.DataAccess.Context;
using HRLeaveManagement.Persistence.Repositories;
using HRLeaveManagement.Persistence.Repositories.Common;
using HRLeaveManagement.Persistence.Services;
using HRLeaveManagement.Persistence.Services.Common;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();

        #region Repositories

        // services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<,>));
        services.AddTransient<ILeaveAllocationRepository, LeaveAllocationRepository>();
        services.AddTransient<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddTransient<ILeaveTypeRepository, LeaveTypeRepository>();
        
        #endregion

        #region Services

        services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
        services.AddTransient<ILeaveTypeService, LeaveTypeService>();
        services.AddTransient<ILeaveAllocationService, LeaveAllocationService>();
        services.AddTransient<ILeaveRequestService, LeaveRequestService>();

        #endregion
        
        return services;
    }
}