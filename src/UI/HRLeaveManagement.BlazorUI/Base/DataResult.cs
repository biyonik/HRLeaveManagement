namespace HRLeaveManagement.BlazorUI.Base;

public class DataResult<T>: Result
{
    public T Data { get; set; }
}