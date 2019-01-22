using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.SDTM;

namespace WebApi.DAL
{
    public class U_UserDAL
    {
        public U_User UserLogin(U_User user)
        {
            string sql = @"
SELECT  U.* ,
        I.ImageSrc AS ImageUrl
FROM    dbo.[User] U
        LEFT JOIN dbo.Image I ON I.UserCode = U.UserCode
WHERE   ( UserName = @UserName
          OR E_Mail = @E_Mail
        )
        AND PassWord = @PassWord
        AND U.IsDel = 0
        AND I.IsDel = 0;";
            using (var conn = AdoConfig.GetDBConnection())
            {
                return conn.Query<U_User>(sql, new { user.UserName, user.PassWord, user.E_Mail }).FirstOrDefault();
            }
        }
        public int Register(U_User user)
        {
            string sql = @"INSERT dbo.[User]
        ( UserName ,
          PassWord ,
          UserCode ,
          E_Mail          
        )
VALUES  ( @UserName ,
          @PassWord , 
          @UserCode  , 
          @E_Mail
        )";
            using (var conn = AdoConfig.GetDBConnection())
            {
                return conn.Execute(sql, user);
            }
        }
        /// <summary>
        /// 判断该邮箱是否已经注册
        /// </summary>
        /// <param name="e_mail"></param>
        /// <returns></returns>
        public bool GetUserByEmail(string e_mail)
        {
            string sql = @"SELECT  COUNT(*)
FROM    dbo.[User]
WHERE   E_Mail = @E_Mail
        AND IsDel = 0;";
            using (var conn = AdoConfig.GetDBConnection())
            {
                return conn.Query(sql, new { E_Mail = e_mail }).FirstOrDefault() > 0 ? true : false;
            }
        }
        /// <summary>
        /// 判断该用户名是否被占用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetUserByName(string name)
        {
            string sql = @"SELECT  COUNT(*)
FROM    dbo.[User]
WHERE   UserName = @UserName
        AND IsDel = 0;";
            using (var conn = AdoConfig.GetDBConnection())
            {
                return conn.Query(sql, new { UserName = name }).FirstOrDefault() > 0 ? true : false;
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int ResetUserPassword(U_User user)
        {
            string sql = @"UPDATE  dbo.[User]
SET     PassWord = @PassWord ,
        ModDate = GETDATE()
WHERE   E_Mail = @E_Mail
        AND IsDel = 0";
            using (var conn = AdoConfig.GetDBConnection())
            {
                return conn.Execute(sql, user);
            }
        }
        /// <summary>
        /// 修改用户名
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int ModifyUserName(U_User user)
        {
            string sql = @"UPDATE  dbo.[User]
SET     UserName = @UserName
WHERE   UserCode = @UserCode
        AND IsDel = 0";
            using (var conn = AdoConfig.GetDBConnection())
            {
                return conn.Execute(sql, user);
            }
        }
    }
}
