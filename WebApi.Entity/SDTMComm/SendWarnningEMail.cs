using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTMComm
{
    public class SendWarnningEMail
    {
        public void SendEMail(string ExceptionMsg)
        {
            string Sender = WebConfigOperation.E_MailAddress;
            string SecretKey = WebConfigOperation.SecretKey;
            var msg = new ResultModel<object>();
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(Sender);
                mailMessage.To.Add(new MailAddress("1570403769@qq.com"));
                mailMessage.Subject = "接口报错";
                mailMessage.Body = "错误信息：" + ExceptionMsg;
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.qq.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Sender, SecretKey);
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
