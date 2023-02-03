
using HRLeaveManagement.BlazorUI.Contracts.Abstract;
using HRLeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HRLeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Index
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    
    [Inject] public ILeaveTypeService LeaveTypeService { get; set; }

    public IReadOnlyList<LeaveTypeViewModel> LeaveTypes { get; private set; }
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await LeaveTypeService.GetAllAsync();
        LeaveTypes = result.Data;
        Message = result.Message;
        IsSuccess = result.IsSuccess;
    }
}