using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class ClientSegmentRepository : IClientSegmentRepository
{
    private readonly ApplicationDbContext _context;

    public ClientSegmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ClientSegment>> GetAllAsync()
    {
        return await _context.ClientSegments
            .OrderBy(cs => cs.SegmentPriority)
            .ToListAsync();
    }

    public async Task<ClientSegment?> GetByIdAsync(Guid id)
    {
        return await _context.ClientSegments.FindAsync(id);
    }

    public async Task<ClientSegment> CreateAsync(ClientSegment clientSegment)
    {
        _context.ClientSegments.Add(clientSegment);
        await _context.SaveChangesAsync();
        return clientSegment;
    }

    public async Task<ClientSegment> UpdateAsync(ClientSegment clientSegment)
    {
        _context.ClientSegments.Update(clientSegment);
        await _context.SaveChangesAsync();
        return clientSegment;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var segment = await _context.ClientSegments.FindAsync(id);
        if (segment == null)
            return false;

        _context.ClientSegments.Remove(segment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.ClientSegments.AnyAsync(cs => cs.Id == id);
    }
}

