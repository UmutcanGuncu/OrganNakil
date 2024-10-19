using OrganNakil.Application.Interfaces;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using OrganNakil.Application.OptionsModel;


namespace OrganNakil.Persistence.Repositories;

public class MailRepository : IMailRepository
{
    private readonly EmailSettings _emailSettings;

    public MailRepository(IOptions<EmailSettings> options)
    {
        _emailSettings = options.Value;
    }

    public async Task SendResetMailAsync(string resetPasswordEmailLink, string ToEmail)
    {
        var smtpClient = new SmtpClient();
        smtpClient.Host = _emailSettings.Host;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Port = 587;
        smtpClient.Credentials =
            new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
        smtpClient.EnableSsl = true;
        smtpClient.Timeout = 5000;
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(_emailSettings.Email);
        mailMessage.To.Add(ToEmail);
        mailMessage.Subject = "Organ Nakil Uygulaması Şifre Sıfırlama Linki";
        mailMessage.Body = @$"<h4>Şifre yenilemek için aşağıdaki linke tıklayınız</h4><p><a href='{resetPasswordEmailLink}'>
                           Şifre Yenileme Linki</a></p>";
        mailMessage.IsBodyHtml = true;
        await smtpClient.SendMailAsync(mailMessage);

    }
}