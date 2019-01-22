using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTM
{
    public class U_User
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        ///用户编号
        /// </summary>
        public string UserCode { get; set; }
        public bool IsAdmin { get; set; }
        public byte IsDel { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string E_Mail { get; set; }
        /// <summary>
        /// 邮箱验证码
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string ImageUrl { get; set; }
        public string Memo { get; set; }
    }
}
