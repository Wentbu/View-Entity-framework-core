//Model
using Microsoft.EntityFrameworkCore;

public class Device
{
    public string DeviceCode { get; set; }
    public string DeviceName { get; set; }
    public string IpAddress { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool Connected { get; set; }
    public bool OperationStatus { get; set; }

    public ICollection<Assignment> Assignments { get; set; }
}

public class Service
{
    public string ServiceCode { get; set; }
    public string ServiceName { get; set; }
    public string Description { get; set; }
    public bool IsInOperation { get; set; }

    public ICollection<Assignment> Assignments { get; set; }
}

public class Assignment
{
    public string Code { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string Telephone { get; set; }
    public DateTime AssignmentDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public short Status { get; set; }

    public string ServiceCode { get; set; }
    public Service Service { get; set; }

    public string DeviceCode { get; set; }
    public Device Device { get; set; }
}

// Materialized View Models
public class ServiceAssignmentStat
{
    public string ServiceCode { get; set; }
    public string ServiceName { get; set; }
    public int YearCount { get; set; }
    public int MonthCount { get; set; }
    public int WeekCount { get; set; }
}

public class AssignmentUsageStat
{
    public string UsageStatus { get; set; }
    public int Count { get; set; }
}

public class DeviceAssignmentStat
{
    public string DeviceCode { get; set; }
    public string DeviceName { get; set; }
    public int AssignmentCount { get; set; }
}

// DbContext
public class KioskManagementContext : DbContext
{
    public KioskManagementContext(DbContextOptions<KioskManagementContext> options) : base(options) { }

    public DbSet<Device> Devices { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Assignment> Assignments { get; set; }

    // Materialized Views
    public DbSet<ServiceAssignmentStat> ServiceAssignmentStats { get; set; }
    public DbSet<AssignmentUsageStat> AssignmentUsageStats { get; set; }
    public DbSet<DeviceAssignmentStat> DeviceAssignmentStats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Device>().HasKey(d => d.DeviceCode);
        modelBuilder.Entity<Service>().HasKey(s => s.ServiceCode);
        modelBuilder.Entity<Assignment>().HasKey(a => new { a.Code, a.AssignmentDate });

        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Service)
            .WithMany(s => s.Assignments)
            .HasForeignKey(a => a.ServiceCode);
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Device)
            .WithMany(d => d.Assignments)
            .HasForeignKey(a => a.DeviceCode);
        modelBuilder.Entity<ServiceAssignmentStat>().HasNoKey().ToView("ServiceAssignmentStats");
        modelBuilder.Entity<AssignmentUsageStat>().HasNoKey().ToView("AssignmentUsageStats");
        modelBuilder.Entity<DeviceAssignmentStat>().HasNoKey().ToView("DeviceAssignmentStats");
    }
}