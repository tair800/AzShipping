using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.CompanyAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class CompanyRepository(SettingsDbContext context) : ICompanyRepository
{
    public async Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Companies
            .Include(x => x.BankAccounts)
            .Include(x => x.Signatures)
            .Include(x => x.WorkerPost)
            .Include(x => x.PricingTypeEntity)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Company>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Companies
            .Include(x => x.BankAccounts)
            .Include(x => x.Signatures)
            .Include(x => x.WorkerPost)
            .Include(x => x.PricingTypeEntity)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

    public async Task<Company> AddAsync(Company entity, CancellationToken cancellationToken = default)
    {
        await context.Companies.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Company entity, CancellationToken cancellationToken = default)
    {
        context.Companies.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateWithChildrenAsync(Company entity, CancellationToken cancellationToken = default)
    {
        var existingBanks = await context.CompanyBankAccounts.Where(x => x.CompanyId == entity.Id).ToListAsync(cancellationToken);
        var existingSignatures = await context.CompanySignatures.Where(x => x.CompanyId == entity.Id).ToListAsync(cancellationToken);
        context.CompanyBankAccounts.RemoveRange(existingBanks);
        context.CompanySignatures.RemoveRange(existingSignatures);
        foreach (var b in entity.BankAccounts)
            context.CompanyBankAccounts.Add(b);
        foreach (var s in entity.Signatures)
            context.CompanySignatures.Add(s);
        context.Companies.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Companies.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Companies.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<CompanySignature?> UpsertSignatureAsync(Guid companyId, string type, string? fileName, string? filePath, CancellationToken cancellationToken = default)
    {
        var existing = await context.CompanySignatures.FirstOrDefaultAsync(x => x.CompanyId == companyId && x.Type == type, cancellationToken);
        if (existing != null)
        {
            existing.FileName = fileName;
            existing.FilePath = filePath;
        }
        else
        {
            existing = new CompanySignature { Id = Guid.NewGuid(), CompanyId = companyId, Type = type, FileName = fileName, FilePath = filePath };
            context.CompanySignatures.Add(existing);
        }
        await context.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task DeleteSignatureAsync(Guid companyId, string type, CancellationToken cancellationToken = default)
    {
        var existing = await context.CompanySignatures.FirstOrDefaultAsync(x => x.CompanyId == companyId && x.Type == type, cancellationToken);
        if (existing != null)
        {
            context.CompanySignatures.Remove(existing);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
