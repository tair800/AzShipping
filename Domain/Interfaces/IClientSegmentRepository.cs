using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface IClientSegmentRepository
{
    Task<IEnumerable<ClientSegment>> GetAllAsync();
    Task<ClientSegment?> GetByIdAsync(Guid id);
    Task<ClientSegment> CreateAsync(ClientSegment clientSegment);
    Task<ClientSegment> UpdateAsync(ClientSegment clientSegment);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

