using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Cee.Tools.Web
{
    /// <summary>
    /// Web上下文
    /// </summary>
    public class WebContext
    {
        /// <summary>
        /// 获取指定当前请求项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetItem(string key)
        {
            return IsWeb ? HttpContext.Current.Items[key] : Thread.GetData(Thread.GetNamedDataSlot(key));
        }

        /// <summary>
        /// 设置当前项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void SetItem(string key, object obj)
        {
            if (IsWeb)
                HttpContext.Current.Items[key] = obj;
            else
                Thread.SetData(Thread.GetNamedDataSlot(key), obj);
        }

        public static Boolean IsWeb { get { return HttpContext.Current != null; } }
        public static Boolean IsWindows
        {
            get { return Environment.OSVersion.VersionString.ToLower().IndexOf("windows") >= 0; }
        }
    }
}
