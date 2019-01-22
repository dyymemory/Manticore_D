using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTMComm
{
    public class EncryptOperation
    {
        /// <summary>
        /// 密码MD5加密 
        /// </summary>
        /// <param name="pwd">加密前密码</param>
        /// <returns></returns>
        public static string MD5HashHex(string pwd)
        {
            StringBuilder encryptPwd = new StringBuilder();
            if (!string.IsNullOrEmpty(pwd))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(pwd), 0, pwd.Length);
                if (res != null)
                {
                    foreach (char item in res)
                    {
                        encryptPwd.Append(System.Uri.HexEscape(item));
                    }
                }
            }
            return encryptPwd.ToString().Replace("%", "").ToLower();
        }
    }
}
