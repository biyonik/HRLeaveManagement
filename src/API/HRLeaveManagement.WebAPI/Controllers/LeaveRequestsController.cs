using HRLeaveManagement.Application.DataTransformationObjects.LeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequestFeatures.Commands;
using HRLeaveManagement.Application.Features.LeaveRequestFeatures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.WebAPI.Controllers;

public class LeaveRequestsController: BaseApiController
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllLeaveRequest.Query();
        var mediatr = await Mediator!.Send(query);
        return HandleDataResult(mediatr);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetLeaveRequestById.Query(id);
        var mediatr = await Mediator!.Send(query);
        return HandleDataResult(mediatr);
    }

    [HttpPost]
    public async Task<IActionResult> Add(LeaveRequestForAddDto leaveRequestForAddDto)
    {
        var command = new CreateLeaveRequest.Command(leaveRequestForAddDto);
        return await HandlePostRequest(command);
    }

    [HttpPut]
    public async Task<IActionResult> Update(LeaveRequestForUpdateDto leaveRequestForUpdateDto)
    {
        var command = new UpdateLeaveRequest.Command(leaveRequestForUpdateDto);
        return await HandlePutRequest(command);
    }

    [HttpPut("cancelLeaveRequest/{id}")]
    public async Task<IActionResult> CancelLeaveRequest(Guid id)
    {
        var command = new CancelLeaveRequest.Command(id);
        return await HandlePutRequest(command);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var command = new DeleteLeaveRequest.Command(id);
        return await HandleDeleteRequest(command);
    }
}