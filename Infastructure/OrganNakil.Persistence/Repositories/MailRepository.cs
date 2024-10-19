using OrganNakil.Application.Interfaces;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using OrganNakil.Application.Dtos.MailDtos;

namespace OrganNakil.Persistence.Repositories;

public class MailRepository : IMailRepository
{
    
/*
    public Task SendResetMailAsync(string resetEmailLink, string To)
    {
        MailAddress to = new MailAddress("ToAddress");
        MailAddress from = new MailAddress("FromAddress");

        MailMessage emaill = new MailMessage(from, to);
        emaill.Subject = "Testing out email sending";
        emaill.Body = "Hello all the way from the land of C#";

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.server.address";
        smtp.Port = 25;
        smtp.Credentials = new NetworkCredential("smtp_username", "smtp_password");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true;

        try
        {
            
            
            smtp.Send(emaill);
        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex.ToString());
        }
        
    }
    */
public Task SendResetMailAsync(string resetEmailLink, string To)
{
    throw new NotImplementedException();
}
}