using System.Net.Mail;

namespace SinaMerchant.Web.Services
{
    public static class EmailSender
    {
        public static bool SendEmail(string to, string subject, string body)
        {
            try
            {
                string email = "snejadbakhtiari@gmail.com";
                string password = "tmqnunefvpkatmtt";

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(email, subject);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;


                SmtpServer.Credentials = new System.Net.NetworkCredential(email, password);
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}
