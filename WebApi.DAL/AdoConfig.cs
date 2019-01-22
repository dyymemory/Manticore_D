using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DAL
{
    public class AdoConfig
    {
        public static SqlConnection GetDBConnection(string dbLink = null)
        {
            if (dbLink == null)
                return GetOpenConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
            else
                return GetOpenConnection(dbLink);
        }
        /// <summary>
        /// 获取一个打开的数据库连接对象 
        /// </summary>
        /// <param name="key">web.config中连接字符串的key</param>
        /// <returns></returns>
        private static SqlConnection GetOpenConnection(string key)
        {
            var connection = new SqlConnection(key);
            connection.Open();
            return connection;
        }
    }
}
