using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Application.Features.LeaveTypeFeatures.Commands;
using HRLeaveManagement.Application.Features.LeaveTypeFeatures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.WebAPI.Controllers;

public class LeaveTypesController : BaseApiController
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        GetAllLeaveTypes.Query query = new GetAllLeaveTypes.Query();
        IDataResult<IReadOnlyList<LeaveTypeForListDto>> mediatr = (await Mediator!.Send(query))!;
        return HandleDataResult(mediatr!);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(Guid Id)
    {
        GetLeaveTypeById.Query query = new GetLeaveTypeById.Query(Id);
        IDataResult<LeaveTypeForDetailDto> mediatr = await Mediator!.Send(query);
        return HandleDataResult(mediatr);
    }

    [HttpPost]
    public async Task<IActionResult> Add(LeaveTypeForAddDto leaveTypeForAddDto)
    {
        CreateLeaveType.Command command = new CreateLeaveType.Command(leaveTypeForAddDto);
        return await HandlePostRequest(command);
    }

    [HttpPut]
    public async Task<IActionResult> Update(LeaveTypeForUpdateDto leaveTypeForUpdateDto)
    {
        UpdateLeaveType.Command command = new UpdateLeaveType.Command(leaveTypeForUpdateDto);
        return await HandlePutRequest(command);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(Guid Id)
    {
        DeleteLeaveType.Command command = new DeleteLeaveType.Command(Id);
        return await HandleDeleteRequest(command);
    }

}