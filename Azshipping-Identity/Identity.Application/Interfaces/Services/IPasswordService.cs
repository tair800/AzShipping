namespace Identity.Application.Interfaces.Services;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    bool NeedsRehash(string hashedPassword);
}