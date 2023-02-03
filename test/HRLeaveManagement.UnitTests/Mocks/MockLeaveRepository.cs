using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Contracts.Services;
using HRLeaveManagement.Application.DataTransformationObjects.LeaveType;
using HRLeaveManagement.Domain;
using Moq;

namespace HRLeaveManagement.UnitTests.Mocks;

public class MockLeaveRepository
{
    public static Mock<ILeaveTypeService> GetLeaveTypeMockLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>
        {
            new()
            {
                Id = Guid.NewGuid(),
                DefaultDays = 10,
                Name = "Test Vacation"
            },
            new()
            {
                Id = Guid.NewGuid(),
                DefaultDays = 20,
                Name = "Test Maternity"
            },
            new()
            {
                Id = Guid.NewGuid(),
                DefaultDays = 15,
                Name = "Test Stic"
            }
        } as IReadOnlyList<LeaveType>;

        var mockRepo = new Mock<ILeaveTypeService>();
        mockRepo.Setup(r => r.GetAllAsync(default, null))
            .ReturnsAsync(leaveTypes);
        // mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>(), default)).Returns((LeaveType leaveType) =>
        // {
        //     leaveTypes.Add(leaveType);
        //     return Task.CompletedTask;
        // });

        return mockRepo;
    }
}