using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Cee.Tools.Web
{
    /// <summary>
    /// URL操作类
    /// </summary>
    public class UrlHelper
    {
        #region 获取网站主域
        
        /// <summary>
        /// 获取网站主域
        /// </summary>
        /// <returns></returns>
        public static string GetDomain()
        {
            string url = HttpContext.Current.Request.Url.ToString();

            string host;
            Uri uri;
            try
            {
                uri = new Uri(url);
                host = uri.Host + "";
            }
            catch
            {
                return "";
            }

            //var beReplacedStrs = new string[] { ".com", ".cn"};

            //foreach (string oneBeReplacedStr in beReplacedStrs)
            //{
            //    string beReplacedStr = oneBeReplacedStr + "";
            //    if (host.IndexOf(beReplacedStr, System.StringComparison.Ordinal) != -1)
            //    {
            //        host = host.Replace(beReplacedStr, string.Empty);
            //        break;
            //    }
            //}

            int dotIndex = host.IndexOf(".", System.StringComparison.Ordinal);
            var doMain = host.Substring(dotIndex, host.Length - dotIndex);

            return doMain.TrimStart('.');
        }

        #endregion

        #region 获取网站根路径

        /// <summary>
        /// 获取网站根路径
        /// </summary>
        /// <returns>Root of the website.</returns>
        public static string GetSiteRoot()
        {
            string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (protocol == null || protocol == "0")
                protocol = "http://";
            else
                protocol = "https://";

            string port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (port == null || port == "80" || port == "443")
                port = "";
            else
                port = ":" + port;

            string siteRoot =
                protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] +
                port + HttpContext.Current.Request.ApplicationPath;
            return siteRoot;
        }
        #endregion

        #region 获取网站根路径

        /// <summary>
        /// 获取网站根路径
        /// </summary>
        /// <returns>Root of the website.</returns>
        public static string GetAppRoot()
        {
            string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (protocol == null || protocol == "0")
                protocol = "http://";
            else
                protocol = "https://";

            string port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (port == null || port == "80" || port == "443")
                port = "";
            else
                port = ":" + port;

            string siteRoot =
                protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] +
                port + HttpContext.Current.Request.ApplicationPath;
            return siteRoot;
        }
        #endregion

        #region 获取RawURL文件名
        /// <summary>
        /// 获取RawURL文件名
        /// </summary>
        /// <param name="rawUrl">Raw URL.</param>
        /// <param name="includeExtension">Flag indicating if extension of the file should also be 
        /// included.</param>
        /// <returns>Requested file name.</returns>
        public static string GetRequestedFileName(string rawUrl, bool includeExtension)
        {
            string file = rawUrl.Substring(rawUrl.LastIndexOf("/") + 1);

            if (includeExtension)
                return file;

            return file.Substring(0, file.IndexOf("."));
        }

        #endregion

        #region 根据给出的相对地址获取网站绝对地址
        /// <summary>
        /// 根据给出的相对地址获取网站绝对地址
        /// </summary>
        /// <param name="localPath">相对地址</param>
        /// <returns>绝对地址</returns>
        public static string GetWebPath(string localPath)
        {
            string path = HttpContext.Current.Request.ApplicationPath;
            string thisPath;
            string thisLocalPath;
            //如果不是根目录就加上"/" 根目录自己会加"/"
            if (path != "/")
            {
                thisPath = path + "/";
            }
            else
            {
                thisPath = path;
            }
            if (localPath.StartsWith("~/"))
            {
                thisLocalPath = localPath.Substring(2);
            }
            else
            {
                return localPath;
            }
            return thisPath + thisLocalPath;
        }

        #endregion

        #region 获取网站绝对地址
        /// <summary>
        ///  获取网站绝对地址
        /// </summary>
        /// <returns></returns>
        public static string GetWebPath()
        {
            string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            string thisPath;
            //如果不是根目录就加上"/" 根目录自己会加"/"
            if (path != "/")
            {
                thisPath = path + "/";
            }
            else
            {
                thisPath = path;
            }
            return thisPath;
        }
        #endregion

        #region 根据相对路径或绝对路径获取绝对路径
        /// <summary>
        /// 根据相对路径或绝对路径获取绝对路径
        /// </summary>
        /// <param name="localPath">相对路径或绝对路径</param>
        /// <returns>绝对路径</returns>
        public static string GetFilePath(string localPath)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(localPath, @"([A-Za-z]):\\([\S]*)"))
            {
                return localPath;
            }
            else
            {
                return System.Web.HttpContext.Current.Server.MapPath(localPath);
            }
        }
        #endregion

        #region 添加URL参数

        /// <summary>
        /// 添加URL参数
        /// </summary>
        public static string AddParam(string url, string paramName, string value)
        {
            Uri uri = new Uri(url);
            if (string.IsNullOrEmpty(uri.Query))
            {
                string eval = HttpContext.Current.Server.UrlEncode(value);
                return String.Concat(url, "?" + paramName + "=" + eval);
            }
            else
            {
                string eval = HttpContext.Current.Server.UrlEncode(value);
                return String.Concat(url, "&" + paramName + "=" + eval);
            }
        }

        #endregion

        #region 更新URL参数
        /// <summary>
        /// 更新URL参数
        /// </summary>
        public static string UpdateParam(string url, string paramName, string value)
        {
            string keyWord = paramName + "=";
            int index = url.IndexOf(keyWord) + keyWord.Length;
            int index1 = url.IndexOf("&", index);
            if (index1 == -1)
            {
                url = url.Remove(index, url.Length - index);
                url = string.Concat(url, value);
                return url;
            }
            url = url.Remove(index, index1 - index);
            url = url.Insert(index, value);
            return url;
        }
        #endregion

        #region 分析 url 字符串中的参数信息

        /// <summary>
        /// 分析 url 字符串中的参数信息
        /// </summary>
        /// <param name="url">输入的 URL</param>
        /// <param name="baseUrl">输出 URL 的基础部分</param>
        /// <param name="dictionary">输出分析后得到的 (参数名,参数值) 的集合 </param>
        public static void ParseUrl(string url, out string baseUrl, out Dictionary<string, string> dictionary)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            dictionary = new Dictionary<string, string>();
            baseUrl = "";

            if (url == "")
                return;

            int questionMarkIndex = url.IndexOf('?');

            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return;
            }
            baseUrl = url.Substring(0, questionMarkIndex);
            if (questionMarkIndex == url.Length - 1)
                return;
            string ps = url.Substring(questionMarkIndex + 1);

            // 开始分析参数对    
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);

            foreach (Match m in mc)
            {
                dictionary.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }
        }
        #endregion
    }
}
