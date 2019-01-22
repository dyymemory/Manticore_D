using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.SDTM;

namespace WebApi.Entity.SDTMComm
{
    public class E_MailSendMethod
    {
        public ResultModel<object> SendE_Mail(RejisterModel user)
        {
            string Sender = WebConfigOperation.E_MailAddress;
            string SecretKey = WebConfigOperation.SecretKey;
            var msg = new ResultModel<object>();
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(Sender);
                mailMessage.To.Add(new MailAddress(user.E_Mail));
                mailMessage.Subject = "这是你的验证码";
                string verificationcode = CreateRandomCode(6);
                mailMessage.Body = verificationcode;
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.qq.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Sender, SecretKey);
                client.Send(mailMessage);
                //设置缓存
                CacheOperation<List<dynamic>>.SetCache(user.E_Mail, verificationcode);
            }
            catch (Exception ex)
            {
                msg.Code = 2001;
                msg.Message = ex.ToString();
            }
            return msg;
        }
        private string CreateRandomCode(int codelengh)
        {
            int rep = 0;
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codelengh; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
    }
}
