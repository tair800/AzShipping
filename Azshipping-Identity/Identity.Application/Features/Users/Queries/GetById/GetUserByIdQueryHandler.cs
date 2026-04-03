using Mapster;
using MediatR;
using Identity.Application.DTOs.User;
using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using MrStyx.Application.Exceptions;

namespace Identity.Application.Features.Users.Queries.GetById;

public sealed class GetUserByIdQueryHandler(IUserRepository userRepository, IUserDtoEnrichmentService enrichmentService)
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken) ??
            throw new NotFoundException($"Can't find user by id \"{request.Id}\"");

        return await enrichmentService.EnrichAsync(user.Adapt<UserDto>(), cancellationToken);
    }
}