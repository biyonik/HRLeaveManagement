using System.Linq.Expressions;
using HRLeaveManagement.BlazorUI.Base;
using HRLeaveManagement.BlazorUI.Models.LeaveTypes;

namespace HRLeaveManagement.BlazorUI.Contracts.Abstract;

public interface ILeaveTypeService
{
    Task<DataResult<IReadOnlyList<LeaveTypeViewModel>>?> GetAllAsync();
    Task<DataResult<LeaveTypeViewModel>> GetAsync(Expression<Func<LeaveTypeViewModel, bool>> expression);
    Task<DataResult<LeaveTypeViewModel>> GetByIdAsync(Guid id);
}