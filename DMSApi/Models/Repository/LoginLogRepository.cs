using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using DMSApi.Models.IRepository;
using DMSApi.Models.StronglyType;
using DMSApi.Models.crystal_models;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text.RegularExpressions;

namespace DMSApi.Models.Repository
{
    public class LoginLogRepository:ILoginLogRepository
    {
        private readonly DMSEntities _entities;

        public LoginLogRepository()
        {
            this._entities=new DMSEntities();
        }

        public long LoginInfoEntry(long userId, string LoginInfoEntry, string loginName)
        {
            try
            {
                //string strHostName = System.Net.Dns.GetHostName();//get the pc name


                //string externalIP = "";
                //externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                //externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(externalIP)[0].ToString();

                //string strHostName = "";
                //strHostName = System.Net.Dns.GetHostName();
                //IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
                //IPAddress[] addr = ipEntry.AddressList;
                //string IP = addr[addr.Length - 1].ToString();

                //if (request.Properties.ContainsKey["MS_HttpContext"])
                //{
                //    var ctx = request.Properties["MS_HttpContext"] as HttpContextWrapper;
                //    if (ctx != null)
                //    {
                //        var ip = ctx.Request.UserHostAddress;
                //        //do stuff with IP
                //    }
                //}

                user_log userLog = new user_log
                {
                    user_id = userId,
                    login_date = DateTime.Now,
                    ip_address = LoginInfoEntry,
                    login_name = loginName
                    
                };
                _entities.user_log.Add(userLog);
                _entities.SaveChanges();
                return userLog.ulogo_id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long LogOutInfoEntry(long userId)
        {
            try
            {
                
                user_log userLog = new user_log
                {
                    
                    user_id = userId,
                    logout_date = DateTime.Now,
                    ip_address = "ip"
                };
                _entities.user_log.Add(userLog);
                _entities.SaveChanges();
                return userLog.ulogo_id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}