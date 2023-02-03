using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using Moq;

namespace HRLeaveManagement.UnitTests.Mocks;

public class MockLeaveRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypeMockLeaveTypeRepository()
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
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();
        mockRepo.Setup(r => r.GetAllAsync(default, null)).Returns(() => leaveTypes);
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>(), default)).Returns((LeaveType leaveType) =>
        {
            leaveTypes.Add(leaveType);
            return Task.CompletedTask;
        });

        return mockRepo;
    }
}