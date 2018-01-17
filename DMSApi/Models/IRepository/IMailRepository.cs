using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSApi.Models.IRepository
{
   public interface IMailRepository
   {
       object SendMail(string fromEmail, string toEmail, string mailSubject, string mailBody, string senderName,
           string senderPass, string attacmmentLocationPath);
   }
}
