using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Entity.SDTMComm;

namespace WebApi.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 获取会话中的用户信息
        /// </summary>
        private UserInfoForCookie user;
        protected new UserInfoForCookie User
        {
            get
            {
                if (user == null)
                {
                    user = G_Comm.DecodeCookieToObject<UserInfoForCookie>(G_Comm.DecodeCookie(WebConfigOperation.Config.AuthorityGlobal.CookieName));
                }
                return user;
            }
            set { user = value; }
        }
    }
}
