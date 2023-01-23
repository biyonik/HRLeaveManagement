namespace HRLeaveManagement.Application.Common.Result.Abstract;

public interface IResult
{
    public string Message { get; set; }
    public bool IsSucceed { get; set; }
}