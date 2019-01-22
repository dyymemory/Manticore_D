using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Api.Controllers;
using WebApi.BLL;
using WebApi.Entity.SDTM;
using WebApi.Entity.SDTMComm;

namespace WebApi.Controllers
{
    public class RegisterController : BaseApiController
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<object> Register([FromBody] JObject model)
        {
            ResultModel<object> msg = new ResultModel<object>();
            var user= JsonConvert.DeserializeObject<U_User>(JsonConvert.SerializeObject(model));
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
            if (string.IsNullOrEmpty(user.AuthCode))
            {
                msg.Code = 2001;
                msg.Message = "参数错误";
                return msg;
            }
            if (string.IsNullOrEmpty(user.UserName))
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
            msg = new U_UserBLL().Register(user);
            return msg;
        }
        /// <summary>
        /// 发送邮件验证码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public ResultModel<object> SendEmail([FromBody] JObject model, [FromUri] Encrypt encrypt)
        {
            var user= JsonConvert.DeserializeObject<RejisterModel>(JsonConvert.SerializeObject(model));
            ResultModel<object> msg = new ResultModel<object>();
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
            bool FlagEmail = new U_UserBLL().GetUserByEmail(user.E_Mail);
            if (user.Type)
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    msg.Code = 2001;
                    msg.Message = "参数错误";
                    return msg;
                }
                bool FlagName = new U_UserBLL().GetUserByName(user.UserName);
                if (FlagName)
                {
                    msg.Code = 2001;
                    msg.Message = "用户名已占用";
                    return msg;
                }
                if (FlagEmail)
                {
                    msg.Code = 2001;
                    msg.Message = "该邮箱已注册";
                    return msg;
                }
            }
            else
            {
                if (!FlagEmail)
                {
                    msg.Code = 2001;
                    msg.Message = "该邮箱未注册";
                    return msg;
                }
            }            
            msg = new U_UserBLL().SendEmail(user);
            //if (encrypt.AccessToken == EncrypteComm.Md5Hex32(encrypt.TimeSpan + WebConfigOperation.CommSecretToken))
            //{
            //    msg = new U_UserBLL().SendEmail(user);
            //}
            //else
            //{
            //    msg.Code = 2001;
            //    msg.Message = "未授权";
            //}
            return msg;
        }
    }
}
