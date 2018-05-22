using System.Net.Mail;

namespace onboarding
{
    public class EmailHelper
    {
        public void SendEmail(string messageBody, string subject, string from, string to, string userName, string password, bool isLocal)
        {
            string smtpHost = "smtp.sendgrid.net";
            string smtpPort = "587";

            SmtpClientHelper smtpClientHelper = new SmtpClientHelper(smtpPort, smtpHost, userName, password);

            if (!isLocal)
            {
                smtpClientHelper.Send(new MailAddress(from), new MailAddress(to), messageBody, subject);
            }
        }
    }
}
