using Jijia.Infrastructure.BasicOperations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity.GlobalVariable;

namespace WebApi.Entity.SDTMComm
{
    public class WebConfigOperation
    {
        /// <summary>
        /// 配置发件人邮箱
        /// </summary>
        public static string E_MailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["E_MailAddress"].ToString();
            }
        }
        /// <summary>
        /// 邮箱授权码
        /// </summary>
        public static string SecretKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SecretKey"].ToString();
            }
        }
        /// <summary>
        /// 加密秘钥
        /// </summary>
        public static string CommSecretToken
        {
            get
            {
                return ConfigurationManager.AppSettings["CommSecretToken"].ToString();
            }
        }
        public static string CacheTime
        {
            get
            {
                return ConfigurationManager.AppSettings["CacheTime"];
            }
        }
        /// <summary>
        /// 获取配置信息
        /// </summary>
        public static ConfigModel Config
        {
            get
            {
                return GetUplusConfig();
            }
        }
        /// <summary>
        /// 权限配置信息
        /// </summary>
        private static ConfigModel GetUplusConfig(double minutes = 1440)
        {
            return BasicOperationJsonObject.GetJsonObject<ConfigModel>(ConstCacheKey.ERPAUTHORITYCACHEKEY, ConfigPath, minutes);
        }
        /// <summary>
        /// 配置项路经
        /// </summary>
        private static string ConfigPath
        {
            get
            {
                return Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, ConfigurationManager.AppSettings["ConfigPath"].ToString());
            }
        }
    }
}
