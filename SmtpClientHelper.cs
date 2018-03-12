using System.Net;
using System.Net.Mail;

namespace Onboarding 
{
    public class SmtpClientHelper
    {
        private int Port { get; set; }
        private string SmtpHost { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }

        public SmtpClientHelper(string port, string smtpHost, string username, string password)
        {
            Port = int.Parse(port);
            SmtpHost = smtpHost;
            Username = username;
            Password = password;
        }

        public void Send(MailAddress from, MailAddress to, string body, string subject)
        {
            using(SmtpClient smtpClient = new SmtpClient(SmtpHost, Port))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(Username, Password);
                
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = from;
                mailMessage.To.Add(to);
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;

                smtpClient.Send(mailMessage);
            }
        }
    }
}