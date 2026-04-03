using Identity.Application.DTOs.Auth;
using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Domain.AggregatesModel.UserAggregate.Enumerations;
using MediatR;
using MrStyx.Application.Exceptions;

namespace Identity.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandHandler
(
    IUserRepository userRepository,
    IPasswordService passwordService,
    ITokenService tokenService,
    IPermissionReadService permissionReadService,
    IEmployeeGroupPermissionClaimsService employeeGroupPermissionClaimsService

) : IRequestHandler<LoginCommand, AccessTokenDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IPermissionReadService _permissionReadService = permissionReadService;
    private readonly IEmployeeGroupPermissionClaimsService _employeeGroupPermissionClaimsService = employeeGroupPermissionClaimsService;

    public async Task<AccessTokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var dto = request.LoginDto;

        var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Username.Value == dto.Username, cancellationToken);

        if (user is null || !_passwordService.VerifyPassword(dto.Password, user.PasswordHash.Value))
            throw new UnauthorizedException("Invalid credentials");

        if (user.Status == UserStatus.Pending)
            throw new ForbiddenException("You must activate your account via email to log in");

        if (user.Status == UserStatus.Deactivated)
            throw new ForbiddenException("This account is deactivated");

        if (user.Status == UserStatus.Deleted)
            throw new UnauthorizedException("Invalid credentials");

        if (user.Status == UserStatus.Blocked)
            throw new ForbiddenException("This account is blocked");

        var roles = await _permissionReadService.GetUserRolesAsync(user.Id);

        var permissions = await _permissionReadService.GetUserPermissionsAsync(user.Id);

        var erp = await _employeeGroupPermissionClaimsService.ResolveAsync(user.EmployeeGroupIds, user.UnlimitedAccess, cancellationToken);

        var token = _tokenService.GenerateAccessToken(user.Id, user.Username.Value, user.Email.Value, roles, permissions, erp);

        user.MarkLoggedIn(DateTime.UtcNow);

        return token;
    }
}