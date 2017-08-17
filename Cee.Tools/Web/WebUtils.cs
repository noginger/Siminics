using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Cee.Tools.Web
{
    /// <summary>
    /// WEB常用辅助类
    /// </summary>
    public class WebUtils
    {
        #region 获取指定Url参数的值
        /// <summary>
        /// 获取指定Url参数的值
        /// </summary>
        /// <param name="paramName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQuery(string paramName)
        {
            return string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[paramName])
                       ? ""
                       : HttpContext.Current.Request.QueryString[paramName].ToString();
        }

        #endregion

        #region 获取指定Url参数的int类型值
        /// <summary>
        /// 获取指定Url参数的int类型值
        /// </summary>
        /// <param name="paramName">Url参数</param>
        /// <param name="defalutValue">默认值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string paramName, int defalutValue)
        {
            return ConvertHelper.StrToInt(HttpContext.Current.Request.QueryString[paramName], defalutValue);
        }
        #endregion

        #region 获得指定Url参数的float类型值
        /// <summary>
        /// 获得指定Url参数的float类型值
        /// </summary>
        /// <param name="paramName">Url参数</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static float GetQueryFloat(string paramName, float defaultValue)
        {
            return ConvertHelper.StrToFloat(HttpContext.Current.Request.QueryString[paramName], defaultValue);
        }
        #endregion

        #region 获得指定Url参数的decimal类型值
        /// <summary>
        /// 获得指定Url参数的decimal类型值
        /// </summary>
        /// <param name="paramName">Url参数</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>Url参数的decimal类型值</returns>
        public static decimal GetQueryDecimal(string paramName, decimal defaultValue)
        {
            return ConvertHelper.StrToDecimal(HttpContext.Current.Request.QueryString[paramName], defaultValue);
        }
        #endregion

        #region 获取指定Form参数值

        /// <summary>
        /// 获取指定Form参数值
        /// </summary>
        /// <param name="paramName">Form参数</param>
        /// <returns>Form参数的值</returns>
        public static string GetFormString(string paramName)
        {
            return string.IsNullOrEmpty((HttpContext.Current.Request.Form[paramName]))
                       ? ""
                       : HttpContext.Current.Request.Form[paramName].ToString();
        }

        #endregion

        #region 获得指定Form参数的int类型值
        /// <summary>
        /// 获得指定Form参数的int类型值
        /// </summary>
        /// <param name="paramName">表单参数</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>表单参数的int类型值</returns>
        public static int GetFormInt(string paramName, int defaultValue)
        {
            return ConvertHelper.StrToInt(HttpContext.Current.Request.Form[paramName], defaultValue);
        }
        #endregion

        #region 获得指定Form参数的Long类型值
        /// <summary>
        /// 获得指定Form参数的Long类型值
        /// </summary>
        /// <param name="paramName">表单参数</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>表单参数的decimal类型值</returns>
        public static long GetFormLong(string paramName, long defaultValue)
        {
            return ConvertHelper.StrToLong(HttpContext.Current.Request.Form[paramName], defaultValue);
        }
        #endregion

        #region 获得指定Form参数的decimal类型值
        /// <summary>
        /// 获得指定Form参数的decimal类型值
        /// </summary>
        /// <param name="paramName">表单参数</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>表单参数的decimal类型值</returns>
        public static decimal GetFormDecimal(string paramName, int defaultValue)
        {
            return ConvertHelper.StrToDecimal(HttpContext.Current.Request.Form[paramName], defaultValue);
        }
        #endregion

        #region 获得指定Form参数的float类型值
        /// <summary>
        /// 获得指定Form参数的float类型值
        /// </summary>
        /// <param name="paramName">表单参数</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>表单参数的float类型值</returns>
        public static float GetFormFloat(string paramName, float defaultValue)
        {
            return ConvertHelper.StrToFloat(HttpContext.Current.Request.Form[paramName], defaultValue);
        }

        #endregion

        #region 获取并转换
        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            if (string.IsNullOrEmpty(GetQuery(strName)))
            {
                return GetFormString(strName);
            }
            else
            {
                return GetQuery(strName);
            }
        }

        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static int GetInt(string strName, int defValue)
        {
            if (GetQueryInt(strName, defValue) == defValue)
            {
                return GetFormInt(strName, defValue);
            }
            else
            {
                return GetQueryInt(strName, defValue);
            }
        }

        /// <summary>
        /// 获得指定Url或表单参数的decimal类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static decimal GetDecimal(string strName, int defValue)
        {
            if (GetQueryDecimal(strName, defValue) == defValue)
            {
                return GetFormDecimal(strName, defValue);
            }
            else
            {
                return GetQueryDecimal(strName, defValue);
            }
        }
        #endregion

        #region 获得当前绝对路径
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                //if (strPath.StartsWith("\\"))
                //{
                //    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                //}
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion

        #region 获得当前页面客户端的IP
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(result) || !Validation.IsIP(result))
            {
                return "0.0.0.0";
            }

            return result;

        }
        #endregion

        #region 验证ID字符串是否正确 ID之间用逗号分开
        /// <summary>
        /// 字符串是否正确 
        /// ID之间用逗号分开
        /// </summary>
        /// <param name="ids">id 集合</param>
        /// <returns></returns>
        public static bool IsIds(string ids)
        {
            if (string.IsNullOrEmpty(ids))
                return false;

            string[] arrys = ids.Split(',');
            bool reslut = arrys.All(Validation.IsNumber);

            return reslut;
        }

        #endregion

        #region 排除非数字的字符
        /// <summary>
        /// 排除非数字的字符
        /// </summary>
        /// <param name="ids">id 集合</param>
        /// <param name="replaceStr">被替换字符</param>
        /// <returns></returns>
        public static string GetIds(string ids, char split, string replaceStr)
        {
            if (string.IsNullOrEmpty(ids))
                return string.Empty;

            string[] arrys = ids.Split(split);
            string temp = string.Empty;

            for (int i = 0; i < arrys.Length; i++)
            {
                if (Validation.IsNumber(arrys[i]) == false)
                {
                    temp = i == 0 ? arrys[i] : (split + arrys[i]);
                    ids = ids.Replace(temp, replaceStr);
                }
            }
            return ids;
        }

        #endregion

        #region 获取文件索引名
        /// <summary>
        /// 获取文件索引名
        /// </summary> 
        /// <param name="fileName">文件名称</param>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public static string GetFileNameIndex(string fileName, int index)
        {
            if (string.IsNullOrEmpty(fileName))
                return "";
            if (index < 0)
                index = 0;
            string result = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(fileName), index,
                                          Path.GetExtension(fileName));

            return result;
        }
        #endregion

        #region 注册Css
        /// <summary>
        /// 注册Css
        /// </summary>
        /// <param name="page">当前Page</param>
        /// <param name="url">Css地址</param>
        public static void RegisterHtmlLink(Page page, int index, string url)
        {
            HtmlLink autoCompleteCss = new HtmlLink();
            autoCompleteCss.Attributes.Add("type", "text/css");
            autoCompleteCss.Attributes.Add("rel", "stylesheet");
            autoCompleteCss.Href = url;
            page.Header.Controls.AddAt(index, autoCompleteCss);
        }
        #endregion

        #region 注册Javascript
        /// <summary>
        /// 注册Javascript
        /// </summary>
        /// <param name="page">当前Page</param>
        /// <param name="url">Javascript地址</param>
        public static void RegisterHtmlScript(Page page, int index, string url)
        {
            HtmlGenericControl autoCompleteScript = new HtmlGenericControl("script");
            autoCompleteScript.Attributes.Add("type", "text/javascript");
            autoCompleteScript.Attributes.Add("src", url);
            page.Header.Controls.AddAt(index, autoCompleteScript);
        }
        #endregion

        #region Session操作
        /// <summary>
        /// 读Session值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>Session值</returns>
        public static object GetSession(string strName)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[strName] != null)
            {
                var value = HttpContext.Current.Session[strName];
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 写入Session值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="value"> </param>
        /// <returns>Session值</returns>
        public static void WriteSession(string strName, string value)
        {
            HttpContext.Current.Session[strName] = value;
        }

        /// <summary>
        /// 写入Session值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="value">Session值</param>
        public static void WriteSession(string strName, object value)
        {
            HttpContext.Current.Session.Add(strName, value);
        }
        /// <summary>
        /// 移除Session
        /// </summary>
        /// <param name="strName"></param>
        public static void RemoveSession(string strName)
        {
            HttpContext.Current.Session.Remove(strName);
        }

        #endregion
    }
}
