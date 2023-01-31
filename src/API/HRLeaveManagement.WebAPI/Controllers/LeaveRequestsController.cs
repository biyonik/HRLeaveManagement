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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var command = new DeleteLeaveRequest.Command(id);
        return await HandleDeleteRequest(command);
    }
}