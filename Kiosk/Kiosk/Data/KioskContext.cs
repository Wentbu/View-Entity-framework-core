using Microsoft.EntityFrameworkCore;

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