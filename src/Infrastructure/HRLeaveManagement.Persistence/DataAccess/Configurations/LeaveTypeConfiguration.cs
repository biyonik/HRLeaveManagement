using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Persistence.DataAccess.Configurations;

public class LeaveTypeConfiguration: IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.ToTable("LeaveTypes");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Name).IsRequired().HasMaxLength(100);

        builder.HasData(new LeaveType
        {
            Id = Guid.NewGuid(),
            Name = "Vacation",
            DefaultDays = 10,
            CreatedDate = DateTime.Now
        });
    }
}