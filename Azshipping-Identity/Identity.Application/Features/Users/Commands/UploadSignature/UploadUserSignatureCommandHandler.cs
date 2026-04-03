using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;
using MrStyx.Application.Exceptions;
using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Application.Features.Users.Commands.UploadSignature;

public sealed class UploadUserSignatureCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IUserSignatureStorageService signatureStorage) : IRequestHandler<UploadUserSignatureCommand, string>
{
    public async Task<string> Handle(UploadUserSignatureCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken, trackingMode: QueryTrackingMode.Tracking)
            ?? throw new NotFoundException($"Can't find user by id \"{request.UserId}\"");

        var publicPath = await signatureStorage.SaveAsync(request.UserId, request.FileStream, request.FileName, cancellationToken);
        user.SetSignatureRelativePath(publicPath);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return publicPath;
    }
}
