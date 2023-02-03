using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging.Common;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Application.Features.LeaveTypeFeatures.Queries;
using HRLeaveManagement.Application.MappingProfiles;
using HRLeaveManagement.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HRLeaveManagement.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypesTest
{
    private readonly Mock<ILeaveTypeService> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetAllLeaveTypes.Handler>> _logger;

    public GetLeaveTypesTest()
    {
        _mockRepo = MockLeaveRepository.GetLeaveTypeMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();

        _logger = new Mock<IAppLogger<GetAllLeaveTypes.Handler>>();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetAllLeaveTypes.Handler(_mockRepo.Object, _mapper, _logger.Object);
        var result = (await handler.Handle(new GetAllLeaveTypes.Query(null), CancellationToken.None)).Data;
        result.ShouldBeOfType<IReadOnlyList<LeaveTypeForListDto?>>();
        result.Count.ShouldBe(3);
    }
}