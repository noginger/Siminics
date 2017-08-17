using System.Configuration;
using FluentData;

namespace BaseLibrary.SqlConfig
{
    public class MySqlHelper
    {
        public static IDbContext Context
        {
            get { return new DbContext().ConnectionString(GetConnectionString(), new MySqlProvider(), "MySql.Data.MySqlClient").IgnoreIfAutoMapFails(true); }
        }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["connStr"].ToString();
            //return "server=qdm156731623.my3w.com;port=3306;user id=qdm156731623;password=xmgd1201;database=qdm156731623_db;Charset=utf8"; //本地服务器
        }

        public static IDbContext WeChatContext
        {
            get { return new DbContext().ConnectionString(GetWeChatConnection(), new MySqlProvider()).IgnoreIfAutoMapFails(true); }
        }

        private static string GetWeChatConnection()
        {
            return "server=qdm156731623.my3w.com;port=3306;user id=qdm156731623;password=wemeit201314;database=cee_wechat;Charset=utf8"; //本地服务器
        }

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <returns></returns>
        public static string FilterSpecialChar(string text)
        {
            return string.IsNullOrEmpty((text)) ? "" : text.Replace("'", "\'");
        }
    }
}
