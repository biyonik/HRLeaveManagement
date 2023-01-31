using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocationFeatures.Commands;
using HRLeaveManagement.Application.Features.LeaveAllocationFeatures.Queries;
using Microsoft.AspNetCore.Mvc;
using IResult = HRLeaveManagement.Application.Common.Result.Abstract.IResult;

namespace HRLeaveManagement.WebAPI.Controllers;

public class LeaveAllocationsController: BaseApiController
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        GetAllLeaveAllocations.Query query = new GetAllLeaveAllocations.Query();
        IDataResult<IEnumerable<LeaveAllocationForListDto>>? mediatr = await Mediator!.Send(query);
        return HandleDataResult(mediatr);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        GetLeaveAllocationWithById.Query query = new GetLeaveAllocationWithById.Query(id);
        IDataResult<LeaveAllocationForListDto>? mediatr = await Mediator!.Send(query);
        return HandleDataResult(mediatr);
    }

    [HttpPost]
    public async Task<IActionResult> Add(LeaveAllocationForAddDto leaveAllocationForAddDto)
    {
        CreateLeaveAllocation.Command command = new CreateLeaveAllocation.Command(leaveAllocationForAddDto);
        return await HandlePostRequest(command);
    }

    [HttpPut]
    public async Task<IActionResult> Update(LeaveAllocationForUpdateDto leaveAllocationForUpdateDto)
    {
        UpdateLeaveAllocation.Command command = new UpdateLeaveAllocation.Command(leaveAllocationForUpdateDto);
        return await HandlePutRequest(command);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        DeleteLeaveAllocation.Command command = new DeleteLeaveAllocation.Command(id);
        return await HandleDeleteRequest(command);
    }
}