using Jijia.Infrastructure.BasicOperations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApi.Entity.SDTMComm
{
    public class G_Comm
    {
        #region 请求相关信息
        /// <summary>
        ///  获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIpaddress()
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_CDN_SRC_IP"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;
            return result;
        }
        /// <summary>
        /// 获取请求参数信息(Webapi)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string RequestParam(HttpRequestMessage request)
        {
            string param = string.Empty;
            if (request != null)
            {
                switch (request.Method.Method.ToLower())
                {
                    case "get":
                        param = HttpContext.Current.Request.QueryString != null ? HttpContext.Current.Request.QueryString.ToString() : "";
                        break;
                    case "post":
                        param = HttpContext.Current.Request.Form != null ? HttpContext.Current.Request.Form.ToString() : "";
                        break;
                    default:
                        param = HttpContext.Current.Request.Params.ToString();
                        break;
                }
            }
            return param;
        }
        /// <summary>
        /// 获取请求参数信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string RequestParam(HttpRequestBase request)
        {
            string param = string.Empty;
            if (request != null)
            {
                switch (request.RequestType.ToLower())
                {
                    case "get":
                        param = request.QueryString == null ? "" : request.QueryString.ToString();
                        break;
                    case "post":
                        param = request.Form == null ? "" : request.Form.ToString();
                        break;
                    default:
                        param = request.Params.ToString();
                        break;
                }
            }
            return param;
        }
        #endregion
        #region 获取请求平台信息
        /// <summary>
        /// 获取请求报头信息
        /// </summary>
        /// <param name="platform">获取请求平台</param>
        /// <returns></returns>
        public static string GetRequestHeaders(string platform)
        {
            return HttpContext.Current.Request.Headers[platform] ?? "0";
        }
        #endregion

        #region Cookie相关设置
        /// <summary>
        /// 设置Cookie加密信息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="cookieName">设置cookie名称 默认WebConfigOperation.LoginCookieName值</param>
        /// <returns>返回当前Cookie对象</returns>
        public static HttpCookie EncryptCookie<T>(T t, string cookieName = "")
        {
            string encryptedTicket = BasicOperationEncrypt.EncryptData(JsonConvert.SerializeObject(t), WebConfigOperation.Config.AuthorityGlobal.SecretKey);
            HttpCookie authCookie = new HttpCookie(string.IsNullOrEmpty(cookieName) ? WebConfigOperation.Config.AuthorityGlobal.CookieName : cookieName, encryptedTicket);
            return authCookie;
        }
        /// <summary>
        /// 获取Cookie信息解密
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="request">请求对象</param>
        /// <returns>返回票据值</returns>
        public static string DecodeCookie(string cookieName, HttpRequest request = null)
        {
            HttpCookie cookie = (request == null) ? System.Web.HttpContext.Current.Request.Cookies[cookieName] : request.Cookies[cookieName];
            if (cookie != null)
            {
                string cookieValue = cookie.Value;
                if (!string.IsNullOrEmpty(cookieValue))
                {
                    try
                    {
                        return BasicOperationEncrypt.DecryptData(cookieValue, WebConfigOperation.Config.AuthorityGlobal.SecretKey);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 将Cookie信息转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="decodeUserInfo"></param>
        /// <returns></returns>
        public static T DecodeCookieToObject<T>(string decodeUserInfo)
        {
            try
            {
                if (!string.IsNullOrEmpty(decodeUserInfo))
                {
                    return JsonConvert.DeserializeObject<T>(decodeUserInfo);
                }
            }
            catch (Exception)
            {
                return default(T);
            }
            return default(T);
        }
        #endregion
    }
}
