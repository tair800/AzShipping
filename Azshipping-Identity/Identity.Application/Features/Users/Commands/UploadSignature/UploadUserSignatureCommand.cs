using MediatR;

namespace Identity.Application.Features.Users.Commands.UploadSignature;

public sealed record UploadUserSignatureCommand(long UserId, Stream FileStream, string FileName) : IRequest<string>;
