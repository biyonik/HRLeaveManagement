using HRLeaveManagement.Domain;
using HRLeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace HRLeaveManagement.Persistence.DataAccess.Context;

public class AppDbContext: DbContext
{
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{envName}.json", false)
            .Build();
        optionsBuilder.UseSqlServer(configurationRoot.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        foreach (var entry in entries)   
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.Id).CurrentValue = Guid.NewGuid();
                entry.Property(p => p.CreatedDate).CurrentValue = DateTime.Now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(p => p.UpdatedDate).CurrentValue = DateTime.Now;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.Property(p => p.DeletedDate).CurrentValue = DateTime.Now;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}