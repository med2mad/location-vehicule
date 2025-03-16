using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace RPtest;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string Email, string Subject, string Message)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential("mohamed.leghdaich@gmail.com", "agrf ducd hnvr rcpv") //password got by "Manage your Google Account" -> Security -> Search "App passwords"
        };

        return client.SendMailAsync(new MailMessage(from: "mohamed.leghdaich@gmail.com", to: Email,
                                                        subject: Subject, body: Message));
    }
}
