using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Infrastructure.Persistence.Repositories;

public class RequestRepository(RequestDbContext context) : IRequestRepository
{
    public async Task<RequestEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Requests.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<RequestEntity>> GetAllAsync(string? typeCode = null, string? mode = null, string? direction = null, string? subType = null, CancellationToken cancellationToken = default)
    {
        var query = context.Requests.AsQueryable().Join(context.RequestTypes, r => r.RequestTypeId, t => t.Id, (r, t) => new { Request = r, Type = t });
        if (!string.IsNullOrWhiteSpace(typeCode))
        {
            var code = typeCode.Trim();
            query = query.Where(x => x.Type.Code == code);
        }
        if (!string.IsNullOrWhiteSpace(mode))
        {
            var modeList = mode.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Where(s => !string.IsNullOrEmpty(s)).ToList();
            if (modeList.Count == 1)
                query = query.Where(x => x.Type.Mode == modeList[0]);
            else if (modeList.Count > 1)
                query = query.Where(x => modeList.Contains(x.Type.Mode));
        }
        if (!string.IsNullOrWhiteSpace(direction))
        {
            var d = direction.Trim();
            query = query.Where(x => x.Type.Direction == d);
        }
        if (!string.IsNullOrWhiteSpace(subType))
        {
            var subTypeList = subType.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Where(s => !string.IsNullOrEmpty(s)).ToList();
            if (subTypeList.Count == 1)
                query = query.Where(x => x.Type.SubType == subTypeList[0]);
            else if (subTypeList.Count > 1)
                query = query.Where(x => subTypeList.Contains(x.Type.SubType ?? ""));
        }
        var requests = await query.OrderByDescending(x => x.Request.CreationDate).Select(x => x.Request).ToListAsync(cancellationToken);
        return requests;
    }

    public async Task<RequestEntity> AddAsync(RequestEntity entity, CancellationToken cancellationToken = default)
    {
        context.Requests.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(RequestEntity entity, CancellationToken cancellationToken = default)
    {
        context.Requests.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Requests.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.Requests.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
