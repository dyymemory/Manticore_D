using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.SDTM;

namespace WebApi.Entity.SDTMComm
{
    public class CreateEmpCode
    {
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="x">字母个数</param>
        /// <param name="y">数字个数</param>
        /// <returns></returns>
        public string GetRandomEmpCode(int x,int y)
        {
            string result = "";
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] NumPattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int n = Pattern.Length;
            int m = NumPattern.Length;
            Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i <= x; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            for (int i = 0; i <= y; i++)
            {
                int rnd = random.Next(0, m);
                result += NumPattern[rnd];
            }
            return result;
        }
    }
}
