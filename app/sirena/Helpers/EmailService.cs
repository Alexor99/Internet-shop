using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace sirena.Helpers
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MailMessage();

            emailMessage.From = new MailAddress("planetrackerteam@gmail.com");
            emailMessage.To.Add(email);
            emailMessage.Subject = subject;
            emailMessage.Body = message; 

            //emailMessage.From.Add(new MailboxAddress("Sirena test email", "planetrackerteam@gmail.com"));
            //emailMessage.To.Add(new MailboxAddress(email));
            //emailMessage.Subject = subject;
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            //{
            //    Text = message
            //};

            try
            {
                using (var client = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
                {
                    //client.Credentials = new NetworkCredential("mykytchenko77@gmail.com", "C0nN3cTMai1");
                    client.Credentials = new NetworkCredential("planetrackerteam@gmail.com", "4lyW1thm3");
                    client.Port = 587;
                    client.EnableSsl = true;
                    await client.SendMailAsync(emailMessage);

                    //await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                    //await client.AuthenticateAsync("planetrackerteam@gmail.com", "4lyW1thm3");
                    //await client.SendAsync(emailMessage);

                    //await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
