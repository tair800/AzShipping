using Identity.Application.DTOs.User;
using Identity.Application.Interfaces.Services;
using Identity.Application.Rules.UserRules;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;
using Mapster;
using MediatR;
using MrStyx.Application.Exceptions;
using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Application.Features.Users.Commands.Update;

public sealed class UpdateUserCommandHandler
(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IUserRules userRules,
    IUserDtoEnrichmentService userDtoEnrichmentService

) : IRequestHandler<UpdateUserCommand, UserDto?>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRules _userRules = userRules;
    private readonly IUserDtoEnrichmentService _userDtoEnrichmentService = userDtoEnrichmentService;

    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.UpdateUserDto;

        var user = await _userRepository.GetByIdAsync(dto.Id, cancellationToken, trackingMode: QueryTrackingMode.Tracking) ??
            throw new NotFoundException($"Can't find user by id \"{dto.Id}\"");

        await _userRules.UsernameUniquenessCheck(dto.Username, dto.Id, cancellationToken);

        await _userRules.FindMissingRoles(dto.RoleIds, cancellationToken);

        await _userRules.EmailCollectionUniquenessCheck(user.Email.Value, dto.AdditionalEmails ?? [], dto.Id, cancellationToken);

        var UsernameVO = Username.Create(dto.Username);
        var FullNameVO = string.IsNullOrWhiteSpace(dto.Name) && string.IsNullOrWhiteSpace(dto.Surname)
                         ? null
                         : FullName.Create(dto.Name, dto.Surname);

        var PhoneNumberVO = string.IsNullOrWhiteSpace(dto.Phone)
                         ? null
                         : PhoneNumber.Create(dto.Phone);

        user.UpdateProfile(UsernameVO, FullNameVO, PhoneNumberVO, dto.RoleIds);

        user.ApplyExtendedProfile(
            dto.CompanyId,
            dto.DepartmentId,
            dto.WorkerPostId,
            dto.EmployeeGroupIds ?? [],
            dto.EmployeePrefix,
            dto.UnlimitedAccess,
            dto.IsEmployee,
            dto.AccessSince,
            dto.AdditionalEmails ?? [],
            dto.AdditionalPhones ?? [],
            dto.Fax,
            dto.Skype,
            dto.SipNumber);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var updatedUser = await _userRepository.GetByIdAsync(user.Id, cancellationToken);

        var result = updatedUser!.Adapt<UserDto>();
        return await _userDtoEnrichmentService.EnrichAsync(result, cancellationToken);
    }
}
