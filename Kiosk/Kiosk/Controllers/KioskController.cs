using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly KioskManagementContext _context;

    public ReportsController(KioskManagementContext context)
    {
        _context = context;
    }

    [HttpGet("service-stats")]
    public async Task<ActionResult<IEnumerable<ServiceAssignmentStat>>> GetServiceStats()
    {
        return await _context.ServiceAssignmentStats.ToListAsync();
    }

    [HttpGet("usage-stats")]
    public async Task<ActionResult<IEnumerable<AssignmentUsageStat>>> GetUsageStats()
    {
        return await _context.AssignmentUsageStats.ToListAsync();
    }

    [HttpGet("device-stats")]
    public async Task<ActionResult<IEnumerable<DeviceAssignmentStat>>> GetDeviceStats()
    {
        return await _context.DeviceAssignmentStats.ToListAsync();
    }

    [HttpPost("refresh-views")]
    public async Task<IActionResult> RefreshViews()
    {
        await _context.Database.ExecuteSqlRawAsync("SELECT refresh_all_materialized_views()");
        return Ok("Materialized views refreshed successfully");
    }
}