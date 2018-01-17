using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using DMSApi.Models.IRepository;

namespace DMSApi.Models.Repository
{
    public class MailRepository : IMailRepository
    {
        private DMSEntities _entities;

        public MailRepository()
        {
            _entities = new DMSEntities();
        }

        public object SendMail(string fromEmail, string toEmail, string mailSubject, string mailBody, string senderName, string senderPass, string attacmmentLocationPath)
        {
            try
            {
                MailMessage mail = new MailMessage();
                //Must be change before using 
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = mailSubject;
                mail.Body = mailBody;
                mail.IsBodyHtml = true;
               // mail.Attachments.Add(new Attachment(@attacmmentLocationPath));

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(senderName, senderPass);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {

                return ex;
            }
        }
    }
}