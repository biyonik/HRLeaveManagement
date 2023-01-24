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

        services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<,>));
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        
        #endregion

        #region Services

        services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
        services.AddScoped<ILeaveTypeService, LeaveTypeService>();
        services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
        services.AddScoped<ILeaveRequestService, LeaveRequestService>();

        #endregion
        
        return services;
    }
}