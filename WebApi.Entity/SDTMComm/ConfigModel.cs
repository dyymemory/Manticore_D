using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Entity.SDTMComm
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public class ConfigModel
    {
        public AuthorityGlobalConfig AuthorityGlobal { get; set; }
    }
    #region  全局配置
    /// <summary>
    ///  全局配置
    /// </summary>
    public class AuthorityGlobalConfig
    {
        /// <summary>
        /// 缓存名称
        /// </summary>
        public string CookieName { get; set; }
        /// <summary>
        /// 白名单
        /// </summary>
        public string[] WhiteList { get; set; }
        /// <summary>
        /// 是否跳过签名 
        /// <para>0否 1是</para>
        /// </summary>
        public int SkipSign { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }
    }
    #endregion
}
