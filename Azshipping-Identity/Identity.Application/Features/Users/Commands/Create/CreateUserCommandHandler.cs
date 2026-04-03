using System.Security.Cryptography;
using System.Text;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Identity.Application.DTOs.User;
using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;
using Identity.Application.Rules.UserRules;
using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Application.Features.Users.Commands.Create;

public sealed class CreateUserCommandHandler
(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordService passwordService,
    IUserRules userRules,
    ILicensingService licensingService,
    IUserDtoEnrichmentService userDtoEnrichmentService,
    IGeneralEmployeeProvisioningService generalEmployeeProvisioning,
    ILogger<CreateUserCommandHandler> logger

) : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IUserRules _userRules = userRules;
    private readonly ILicensingService _licensingService = licensingService;
    private readonly IUserDtoEnrichmentService _userDtoEnrichmentService = userDtoEnrichmentService;
    private readonly IGeneralEmployeeProvisioningService _generalEmployeeProvisioning = generalEmployeeProvisioning;
    private readonly ILogger<CreateUserCommandHandler> _logger = logger;

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.CreateUserDto;
        _logger.LogInformation(
            "CreateUser handler: start. Username={Username}, Email={Email}",
            dto.Username,
            dto.Email);

        _logger.LogInformation("CreateUser step: validating roles exist");
        await _userRules.FindMissingRoles(dto.RoleIds, cancellationToken);

        _logger.LogInformation("CreateUser step: username / email uniqueness");
        await _userRules.UsernameUniquenessCheck(dto.Username, cancellationToken);
        await _userRules.EmailCollectionUniquenessCheck(dto.Email, dto.AdditionalEmails ?? [], null, cancellationToken);

        var passwordPlain = string.IsNullOrWhiteSpace(dto.Password)
            ? GenerateRandomPassword()
            : dto.Password;

        _logger.LogInformation("CreateUser step: hashing password");
        string passwordHash = _passwordService.HashPassword(passwordPlain);

        _logger.LogInformation("CreateUser step: building value objects and aggregate");
        var UsernameVO = Username.Create(dto.Username);
        var PasswordHashVO = PasswordHash.Create(passwordHash);
        var EmailVO = Email.Create(dto.Email);
        var FullNameVO = string.IsNullOrWhiteSpace(dto.Name) && string.IsNullOrWhiteSpace(dto.Surname)
                         ? null
                         : FullName.Create(dto.Name, dto.Surname);

        var PhoneNumberVO = string.IsNullOrWhiteSpace(dto.Phone)
                         ? null
                         : PhoneNumber.Create(dto.Phone);

        var user = User.Create(UsernameVO, PasswordHashVO, FullNameVO, EmailVO, PhoneNumberVO, dto.RoleIds);

        user.ApplyExtendedProfile(
            dto.CompanyId,
            dto.DepartmentId,
            dto.WorkerPostId,
            dto.EmployeeGroupIds ?? [],
            dto.EmployeePrefix,
            dto.UnlimitedAccess,
            dto.IsEmployee,
            dto.AccessSince ?? DateTime.UtcNow,
            dto.AdditionalEmails ?? [],
            dto.AdditionalPhones ?? [],
            dto.Fax,
            dto.Skype,
            dto.SipNumber);

        if (dto.ActivateImmediately)
        {
            _logger.LogInformation("CreateUser step: licensing + activate");
            await _licensingService.EnsureCanActivateAnotherUserAsync(cancellationToken);
            user.Activate();
        }

        _logger.LogInformation("CreateUser step: persist to repository");
        var createdUser = await _userRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("CreateUser handler: saved UserId={UserId}", createdUser.Id);

        if (dto.IsEmployee)
        {
            var fullNameParts = new List<string>(2);
            if (!string.IsNullOrWhiteSpace(dto.Name)) fullNameParts.Add(dto.Name.Trim());
            if (!string.IsNullOrWhiteSpace(dto.Surname)) fullNameParts.Add(dto.Surname.Trim());
            var fullName = fullNameParts.Count > 0 ? string.Join(' ', fullNameParts) : null;
            var phone = string.IsNullOrWhiteSpace(dto.Phone) ? null : dto.Phone.Trim();

            _logger.LogInformation("CreateUser step: General.API employee row for UserId={UserId}", createdUser.Id);
            await _generalEmployeeProvisioning.TryProvisionEmployeeAsync(
                createdUser.Id,
                dto.Username,
                fullName,
                dto.Email,
                phone,
                dto.DepartmentId,
                dto.WorkerPostId,
                cancellationToken);
        }

        var result = createdUser.Adapt<UserDto>();
        _logger.LogInformation("CreateUser step: enrichment");
        return await _userDtoEnrichmentService.EnrichAsync(result, cancellationToken);
    }

    private static string GenerateRandomPassword(int length = 16)
    {
        const string alphabet = "ABCDEFGHJKLMNPQRSTUVWabcdefghijkmnopqrstuvwxyz23456789!@#$%*";
        var bytes = RandomNumberGenerator.GetBytes(length);
        var sb = new StringBuilder(length);
        foreach (var b in bytes)
            sb.Append(alphabet[b % alphabet.Length]);
        return sb.ToString();
    }
}
