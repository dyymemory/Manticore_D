using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTMComm
{
    public class UserInfoForCookie
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 管理员标识
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string E_Mail { get; set; }
    }
}
