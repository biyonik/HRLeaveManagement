using HRLeaveManagement.Application.Common.Result.Abstract;
using HRLeaveManagement.Application.Common.Result.Concrete;
using HRLeaveManagement.Application.Messaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = HRLeaveManagement.Application.Common.Result.Abstract.IResult;

namespace HRLeaveManagement.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class BaseApiController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected ICommand<IResult> _command;

    public IActionResult HandleResult(IResult? result)
    {
        if (result == null) return NotFound();

        if (result.IsSucceed) return Ok(result);
        
        return BadRequest(result);
    }

    public IActionResult HandleDataResult<T>(IDataResult<T>? result)
    {
        if (result == null) return NotFound();

        if (result.IsSucceed && result.Data != null) return Ok(new SuccessDataResult<T>(result.Data));

        if (!result.IsSucceed && result.Data == null) return NotFound(new ErrorDataResult<T>(result.Message));

        return BadRequest(new ErrorDataResult<T>(result.Message));
    }

    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [NonAction]
    public async Task<IActionResult> HandlePostRequest(ICommand<IResult> command)
    {
        IResult mediatr = await Mediator!.Send(command);
        return HandleResult(mediatr);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [NonAction]
    public async Task<IActionResult> HandlePutRequest(ICommand<IResult> command)
    {
        IResult mediatr = await Mediator!.Send(command);
        return HandleResult(mediatr);
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [NonAction]
    public async Task<IActionResult> HandleDeleteRequest(ICommand<IResult> command)
    {
        IResult mediatr = await Mediator!.Send(command);
        return HandleResult(mediatr);
    }


}