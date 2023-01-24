using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DataAccess.Context;
using HRLeaveManagement.Persistence.Repositories.Common;

namespace HRLeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository: GenericRepository<LeaveType, AppDbContext>, ILeaveTypeRepository
{
}