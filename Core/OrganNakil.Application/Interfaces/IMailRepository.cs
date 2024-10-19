namespace OrganNakil.Application.Interfaces;

public interface IMailRepository
{
    Task SendResetMailAsync(string resetPasswordEmailLink, string ToEmail);
}