using Carrier.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/vehiclelookups")]
public class VehicleLookupsController(CarrierDbContext db) : ControllerBase
{
    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<object>>> GetBrands(CancellationToken ct)
        => Ok(await db.VehicleBrands.OrderBy(x => x.Name).Select(x => new { x.Id, x.Name }).ToListAsync(ct));

    [HttpGet("models")]
    public async Task<ActionResult<IEnumerable<object>>> GetModels(CancellationToken ct)
        => Ok(await db.VehicleModels.OrderBy(x => x.Name).Select(x => new { x.Id, x.Name }).ToListAsync(ct));

    [HttpGet("euroemissionclasses")]
    public async Task<ActionResult<IEnumerable<object>>> GetEuroEmissionClasses(CancellationToken ct)
        => Ok(await db.EuroEmissionClasses.OrderBy(x => x.Name).Select(x => new { x.Id, x.Name }).ToListAsync(ct));

    [HttpGet("groups")]
    public async Task<ActionResult<IEnumerable<object>>> GetGroups(CancellationToken ct)
        => Ok(await db.VehicleGroups.OrderBy(x => x.Name).Select(x => new { x.Id, x.Name }).ToListAsync(ct));
}

