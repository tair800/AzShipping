using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class BankService
{
    private readonly IBankRepository _repository;

    public BankService(IBankRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<BankDto>> GetAllAsync()
    {
        var banks = await _repository.GetAllAsync();
        return banks.Select(MapToDto);
    }

    public async Task<BankDto?> GetByIdAsync(Guid id)
    {
        var bank = await _repository.GetByIdAsync(id);
        return bank == null ? null : MapToDto(bank);
    }

    public async Task<BankDto> CreateAsync(CreateBankDto createDto)
    {
        var bank = new Bank
        {
            Id = Guid.NewGuid(),
            Name = createDto.Name,
            UnofficialName = createDto.UnofficialName,
            Branch = createDto.Branch,
            Code = createDto.Code,
            Swift = createDto.Swift,
            Country = createDto.Country,
            City = createDto.City,
            Address = createDto.Address,
            PostCode = createDto.PostCode,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repository.CreateAsync(bank);
        return MapToDto(created);
    }

    public async Task<BankDto?> UpdateAsync(Guid id, UpdateBankDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        existing.Name = updateDto.Name;
        existing.UnofficialName = updateDto.UnofficialName;
        existing.Branch = updateDto.Branch;
        existing.Code = updateDto.Code;
        existing.Swift = updateDto.Swift;
        existing.Country = updateDto.Country;
        existing.City = updateDto.City;
        existing.Address = updateDto.Address;
        existing.PostCode = updateDto.PostCode;
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static BankDto MapToDto(Bank bank)
    {
        return new BankDto
        {
            Id = bank.Id,
            Name = bank.Name,
            UnofficialName = bank.UnofficialName,
            Branch = bank.Branch,
            Code = bank.Code,
            Swift = bank.Swift,
            Country = bank.Country,
            City = bank.City,
            Address = bank.Address,
            PostCode = bank.PostCode,
            CreatedAt = bank.CreatedAt,
            UpdatedAt = bank.UpdatedAt
        };
    }
}

