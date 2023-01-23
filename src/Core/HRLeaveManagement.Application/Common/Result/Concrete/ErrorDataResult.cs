using HRLeaveManagement.Application.Common.Result.Abstract;

namespace HRLeaveManagement.Application.Common.Result.Concrete;

public class ErrorDataResult<T>: IDataResult<T>
{
    public string Message { get; set; }
    public bool IsSucceed { get; set; }
    public T Data { get; set; }

    public ErrorDataResult(T data)
    {
        IsSucceed = true;
        Data = data;
    }

    public ErrorDataResult(T data, string message): this(data)
    {
        Message = message;
        Data = data;
        IsSucceed = true;
    }

    public ErrorDataResult(string message): this(default, message)
    {
        IsSucceed = true;
    }
}