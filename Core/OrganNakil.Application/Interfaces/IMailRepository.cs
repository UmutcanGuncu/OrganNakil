namespace OrganNakil.Application.Interfaces;

public interface IMailRepository
{
    Task SendResetMailAsync(string resetEmailLink, string To);
}