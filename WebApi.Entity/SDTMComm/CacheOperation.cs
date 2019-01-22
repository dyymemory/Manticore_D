using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace WebApi.Entity.SDTMComm
{
    /// <summary>
    /// 缓存功能设置
    /// </summary>
    public class CacheOperation<T> where T : class, new()
    {
        /// <summary>
        /// 设置缓存信息
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="t">缓存文本对象</param>
        /// <param name="dependency">文件依赖缓存</param>
        /// <param name="minutes">缓存时间默认0分钟</param>
        /// <param name="priority">优先级</param>
        /// <param name="cacheRemoveCallBack">移除缓存回调函数</param>
        public static void SetCache(string key, string t, CacheDependency dependency = null, double minutes = 0, CacheItemPriority priority = CacheItemPriority.Normal, CacheItemRemovedCallback cacheRemoveCallBack = null)
        {
            minutes = minutes == 0 ? Convert.ToDouble(WebConfigOperation.CacheTime) : minutes;
            Cache cache = HttpRuntime.Cache;
            cache.Insert(key, t, dependency, DateTime.Now.AddMinutes(minutes), TimeSpan.Zero, priority, cacheRemoveCallBack);
        }
        /// <summary>
        /// 获取缓存信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCache(string key)
        {
            Cache cache = HttpRuntime.Cache;
            return cache[key] as string;
        }
        /// <summary>
        /// 创建文件依赖对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static CacheDependency CreateCacheDependency(string fileName)
        {
            return new CacheDependency(fileName);
        }
        /// <summary>
        /// 移除缓存信息
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }
    }
}
