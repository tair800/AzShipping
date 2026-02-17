using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;
using AzShipping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzShipping.Infrastructure.Persistence.Repositories;

public class BankRepository : IBankRepository
{
    private readonly ApplicationDbContext _context;

    public BankRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Bank>> GetAllAsync()
    {
        return await _context.Banks
            .OrderBy(b => b.Name)
            .ToListAsync();
    }

    public async Task<Bank?> GetByIdAsync(Guid id)
    {
        return await _context.Banks
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Bank> CreateAsync(Bank bank)
    {
        _context.Banks.Add(bank);
        await _context.SaveChangesAsync();
        return bank;
    }

    public async Task<Bank> UpdateAsync(Bank bank)
    {
        var existing = await _context.Banks.FindAsync(bank.Id);
        if (existing != null)
        {
            existing.Name = bank.Name;
            existing.UnofficialName = bank.UnofficialName;
            existing.Branch = bank.Branch;
            existing.Code = bank.Code;
            existing.Swift = bank.Swift;
            existing.Country = bank.Country;
            existing.City = bank.City;
            existing.Address = bank.Address;
            existing.PostCode = bank.PostCode;
            existing.UpdatedAt = bank.UpdatedAt;
            
            _context.Entry(existing).Property(e => e.Name).IsModified = true;
        }
        else
        {
            _context.Banks.Update(bank);
        }

        await _context.SaveChangesAsync();
        return await GetByIdAsync(bank.Id) ?? bank;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var bank = await _context.Banks.FindAsync(id);
        if (bank == null)
            return false;

        _context.Banks.Remove(bank);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Banks.AnyAsync(b => b.Id == id);
    }
}

