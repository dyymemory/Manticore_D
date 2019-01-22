using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Api.Controllers;
using WebApi.BLL;
using WebApi.Entity.SDTM;
using WebApi.Entity.SDTMComm;

namespace WebApi.Controllers
{
    public class LoginController : BaseApiController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<U_User> Login([FromBody] JObject model,[FromUri] Encrypt encrypt)
        {
            ResultModel<U_User> msg = new ResultModel<U_User>();
            var user = JsonConvert.DeserializeObject<U_User>(JsonConvert.SerializeObject(model));
            if (user == null)
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (string.IsNullOrEmpty(user.UserName)|| string.IsNullOrEmpty(user.PassWord))
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            UserInfoForCookie userInfoForCookie = null;
            msg = new U_UserBLL().UserLogin(user, ref userInfoForCookie);
            if (msg.Code == 2000)
            {
                HttpContext.Current.Response.Cookies.Set(G_Comm.EncryptCookie<UserInfoForCookie>(userInfoForCookie));
            }
            return msg;
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<object> ResetUserPassword([FromBody] JObject model, [FromUri] Encrypt encrypt)
        {
            ResultModel<object> msg = new ResultModel<object>();
            var user = JsonConvert.DeserializeObject<U_User>(JsonConvert.SerializeObject(model));
            if (user == null)
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (string.IsNullOrEmpty(user.E_Mail))
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (string.IsNullOrEmpty(user.PassWord))
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (string.IsNullOrEmpty(user.AuthCode))
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            return msg = new U_UserBLL().ResetUserPassword(user);
        }
    }
}
