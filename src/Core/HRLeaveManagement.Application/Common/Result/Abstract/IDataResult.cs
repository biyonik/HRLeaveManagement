namespace HRLeaveManagement.Application.Common.Result.Abstract;

public interface IDataResult<T>: IResult
{
    public T Data { get; set; }
}