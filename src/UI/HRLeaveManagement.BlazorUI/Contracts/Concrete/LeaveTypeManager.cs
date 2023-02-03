using System.Linq.Expressions;
using System.Net.Http.Json;
using AutoMapper;
using HRLeaveManagement.BlazorUI.Base;
using HRLeaveManagement.BlazorUI.Contracts.Abstract;
using HRLeaveManagement.BlazorUI.Models.LeaveTypes;


namespace HRLeaveManagement.BlazorUI.Contracts.Concrete;

public class LeaveTypeManager: ILeaveTypeService
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public LeaveTypeManager(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<DataResult<IReadOnlyList<LeaveTypeViewModel>>?> GetAllAsync()
    {
        var results = await _httpClient.GetFromJsonAsync<DataResult<IReadOnlyList<LeaveTypeViewModel>>>("api/v1/LeaveTypes");
        if (results.Data == null) return results;
        
        var mapper = _mapper.Map<IReadOnlyList<LeaveTypeViewModel>>(results.Data);
        return new DataResult<IReadOnlyList<LeaveTypeViewModel>>
        {
            Data = mapper,
            IsSuccess = results.IsSuccess,
            Message = results.Message
        };

    }

    public Task<DataResult<LeaveTypeViewModel>> GetAsync(Expression<Func<LeaveTypeViewModel, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<DataResult<LeaveTypeViewModel>> GetByIdAsync(Guid id)
    {
        var result = await _httpClient.GetFromJsonAsync<DataResult<LeaveTypeViewModel>>($"api/v1/LeaveTypes/{id}");
        if (result.Data == null) return result;

        var mapper = _mapper.Map<LeaveTypeViewModel>(result.Data);
        return new DataResult<LeaveTypeViewModel>
        {
            Data = mapper,
            IsSuccess = result.IsSuccess,
            Message = result.Message
        };
    }
}