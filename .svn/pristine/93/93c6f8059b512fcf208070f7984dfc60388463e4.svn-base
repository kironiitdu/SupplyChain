using System.Net;
using System.Net.Mail;

namespace DMSApi.Models
{
    public static class CustomMail
    {
        public static bool SendMailForExecption(string to, string from, string subject, string body)
        {
            //MailMessage mail = new MailMessage(form, to);
            //SmtpClient client = new SmtpClient();
            //client.Port = 587;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.EnableSsl = true;
            //client.Credentials = new NetworkCredential("tariqul@systechunimax.com", "programmerrony01");
            //client.Host = "smtp-mail.outlook.com";
            //mail.Subject = subject;
            //mail.Body = body;
            //client.Send(mail);
            //try
            //{
            //    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
            //    service.Credentials = new WebCredentials(RPAC_Config.exchange_server_user_name, RPAC_Config.exchange_server_password);
            //    service.Url = new Uri(RPAC_Config.exchange_server_api_url);

            //    //service.AutodiscoverUrl("r-ats@r-pac.local");
            //    EmailMessage message = new EmailMessage(service);
            //    message.Subject = subject;
            //    message.Body = body;
            //    message.ToRecipients.Add(to);
            //    message.Send();
            //}
            //catch
            //{ }
            //return;

            //MailMessage mail = new MailMessage(form, to);
            //SmtpClient client = new SmtpClient();
            //client.Port = long.Parse(RPAC_Config.smtp_port);
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.EnableSsl = true;
            //client.Credentials = new NetworkCredential(RPAC_Config.smtp_user_name, RPAC_Config.smtp_password);
            //client.Host = RPAC_Config.smtp_host;
            //mail.Subject = subject;
            //mail.Body = body;
            //client.Send(mail);
            // Below is main code
            try
            {
                MailMessage mail = new MailMessage(from, to);

                SmtpClient client = new SmtpClient();
                client.Port = 587;                      //long.Parse(RPAC_Config.smtp_port);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("tariqul@systechunimax.com", "programmerrony01");   // new NetworkCredential(RPAC_Config.smtp_user_name, RPAC_Config.smtp_password);
                client.Host = "smtp.systechunimax.com";   //RPAC_Config.smtp_host;   //"smtp-mail.outlook.com"
                mail.Subject = subject;
                mail.Body = body;
                client.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
     
        }
    }
}