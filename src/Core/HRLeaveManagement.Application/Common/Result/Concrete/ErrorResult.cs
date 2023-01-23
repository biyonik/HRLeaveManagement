using HRLeaveManagement.Application.Common.Result.Abstract;

namespace HRLeaveManagement.Application.Common.Result.Concrete;

public class ErrorResult: IResult
{
    public string Message { get; set; }
    public bool IsSucceed { get; set; }

    public ErrorResult()
    {
        IsSucceed = false;
    }

    public ErrorResult(string message)
    {
        Message = message;
        IsSucceed = false;
    }
}