using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTMComm
{
    public class EncrypteComm
    {
        /// <summary>
        /// MD5加密处理
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string Md5Hex32(string ConvertString)
        {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(ConvertString);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
    }
}
