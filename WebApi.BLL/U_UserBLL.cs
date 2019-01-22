using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebApi.DAL;
using WebApi.Entity.SDTM;
using WebApi.Entity.SDTMComm;

namespace WebApi.BLL
{
    public class U_UserBLL
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultModel<U_User> UserLogin(U_User user,ref UserInfoForCookie userInfoForCookie)
        {
            ResultModel<U_User> msg = new ResultModel<U_User>();
            user.PassWord = EncryptOperation.MD5HashHex(user.PassWord);
            msg.Data = new U_UserDAL().UserLogin(user);
            if (msg.Data == null)
            {
                msg.Code = 2001;
                msg.Message = "帐号或密码错误";
            }
            else
            {
                userInfoForCookie = new UserInfoForCookie()
                {
                    UserName = msg.Data.UserName,
                    UserCode = msg.Data.UserCode,
                    IsAdmin = msg.Data.IsAdmin,
                    E_Mail = msg.Data.E_Mail,
                };
            }
            return msg;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultModel<object> Register(U_User user)
        {
            ResultModel<object> msg = new ResultModel<object>();
            var list = CacheOperation<List<dynamic>>.GetCache(user.E_Mail);
            if (list == null)
            {
                msg.Code = 2001;
                msg.Message = "验证码已过期";
                return msg;
            }
            if (list != user.AuthCode)
            {
                msg.Code = 2001;
                msg.Message = "验证码错误";
                return msg;
            }
            user.UserCode = new CreateEmpCode().GetRandomEmpCode(2,4);  
            var result = new U_UserDAL().Register(user);
            if (result > 0)
            {
                msg.Message = "注册成功";
                return msg;
            }
            else
            {
                msg.Code = 2001;
                msg.Message = "注册失败";
                return msg;
            }
        }
        /// <summary>
        /// 邮箱验证码验证
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultModel<object> SendEmail(RejisterModel user)
        {
            var msg = new ResultModel<object>();
            msg = new E_MailSendMethod().SendE_Mail(user);
            return msg;
        }
        public bool GetUserByEmail(string e_mail)
        {
            return new U_UserDAL().GetUserByEmail(e_mail);
        }
        public bool GetUserByName(string name)
        {
            return new U_UserDAL().GetUserByName(name);
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultModel<object> ResetUserPassword(U_User user)
        {
            ResultModel<object> msg = new ResultModel<object>();
            var list = CacheOperation<List<dynamic>>.GetCache(user.E_Mail);
            if (list == null)
            {
                msg.Code = 2001;
                msg.Message = "验证码已过期";
                return msg;
            }
            if (list != user.AuthCode)
            {
                msg.Code = 2001;
                msg.Message = "验证码错误";
                return msg;
            }
            user.PassWord = EncryptOperation.MD5HashHex(user.PassWord);
            var result = new U_UserDAL().ResetUserPassword(user);
            if (result > 0)
            {
                msg.Message = "修改成功";
                return msg;
            }
            else
            {
                msg.Code = 2001;
                msg.Message = "修改失败";
                return msg;
            }
        }
        public ResultModel<object> ModifyUserName(U_User user)
        {
            bool flag = false;
            ResultModel<object> msg = new ResultModel<object>();
            flag = GetUserByName(user.UserName);
            if (flag)
            {
                msg.Code = 2001;
                msg.Message = "用户名已占用";
                return msg;
            }
            var result = new U_UserDAL().ModifyUserName(user);
            if (result > 0)
            {
                msg.Message = "修改成功";
                return msg;
            }
            else
            {
                msg.Code = 2001;
                msg.Message = "修改失败";
                return msg;
            }
        }
    }
}
