using Identity.Application.Features.Users.Commands.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Behaviors;

/// <summary>
/// Outermost pipeline wrapper: logs full exception (including inner exceptions) when validation, transaction, or handler fails.
/// </summary>
public sealed class MediatorExceptionLoggingBehavior<TRequest, TResponse>(
    ILoggerFactory loggerFactory)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger _logger = loggerFactory.CreateLogger($"MediatR.{typeof(TRequest).Name}");

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            switch (request)
            {
                case CreateUserCommand cmd:
                    _logger.LogError(
                        ex,
                        "MediatR request failed: {RequestType}. Username={Username}, Email={Email}, RoleCount={RoleCount}, ActivateImmediately={Activate}, CompanyId={CompanyId}, DepartmentId={DepartmentId}",
                        typeof(TRequest).Name,
                        cmd.CreateUserDto.Username,
                        cmd.CreateUserDto.Email,
                        cmd.CreateUserDto.RoleIds?.Count ?? 0,
                        cmd.CreateUserDto.ActivateImmediately,
                        cmd.CreateUserDto.CompanyId,
                        cmd.CreateUserDto.DepartmentId);
                    break;
                default:
                    _logger.LogError(ex, "MediatR request failed: {RequestType}", typeof(TRequest).Name);
                    break;
            }

            throw;
        }
    }
}
