using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace RF.Data.Repository
{
    public class RepositoryMail
    {
        public void SendMail(Mail mail)
        {
            string gmailUser = ConfigurationManager.AppSettings["GmailUser"];
            string gmailPassword = ConfigurationManager.AppSettings["GmailPassword"];

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587)) // 587 = SMTP with TLS port
            {
                client.EnableSsl = true; // Este nombre es confuso, esta linea activa la encriptacion TLS
                client.Credentials = new NetworkCredential(gmailUser, gmailPassword);

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(gmailUser, "Restaurant Finder");
                mailMessage.To.Add(mail.Recipient);
                mailMessage.Subject = mail.Subject;
                mailMessage.Body = mail.Body;

                client.Send(mailMessage);
            }
        }
    }
}