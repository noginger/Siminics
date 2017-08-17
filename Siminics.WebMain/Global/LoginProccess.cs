using System;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using BaseLibrary.Common;
using Cee.Tools;
using Siminics.Model;

namespace Siminics.WebMain.Global
{
    public class LoginProccess
    {
        public static readonly string _sysUserSessionKey = "SYSUSER_KEY";

        public static readonly string _sysUserCookieKey = "SYSUSER_COOKIE_KEY";

        #region 写入session

        /// <summary>
        /// 写入session
        /// </summary>
        public static SysUserModel InsertSession(SysUserInfo userInfo)
        {
            if (userInfo == null || userInfo.UserId <= 0)
                return new SysUserModel();

            SysUserModel sysUserModel = new SysUserModel();
            sysUserModel.SysUserInfo = userInfo;

            //写入Session
            Encryption.LoginInfo loginInfo = new Encryption.LoginInfo(userInfo.UserId, 0, userInfo.UserName, 0, 0,
                                                                      userInfo.PassWord,
                                                                      userInfo.LoginDate,
                                                                      userInfo.LoginIp, 1);
            sysUserModel.EncryptLoginId = new Encryption().EncodeLogin(loginInfo);

            HttpSessionState httpSession = HttpContext.Current.Session;
            if (httpSession[_sysUserSessionKey] != null)
                httpSession.Remove(_sysUserSessionKey);
            httpSession.Add(_sysUserSessionKey, sysUserModel);

            //写入Cookie
            HttpCookie cookie = new HttpCookie(_sysUserCookieKey);
            cookie.Value = sysUserModel.EncryptLoginId;
            cookie.Domain =
                HttpContext.Current.Request.Url.Host.Substring(HttpContext.Current.Request.Url.Host.IndexOf(".") + 1);
            cookie.Path = "/";
            if (HttpContext.Current.Response.Cookies[_sysUserCookieKey] != null)
                HttpContext.Current.Response.Cookies.Set(cookie);
            else
                HttpContext.Current.Response.Cookies.Add(cookie);

            return sysUserModel;
        }

        #endregion

        #region 清除Session

        public static void ClearSession()
        {
            HttpSessionState httpSession = HttpContext.Current.Session;
            if (httpSession[_sysUserSessionKey] != null)
                httpSession.Remove(_sysUserSessionKey);

            //清除cookie
            if (HttpContext.Current.Response.Cookies[_sysUserCookieKey] != null)
                HttpContext.Current.Response.Cookies.Remove(_sysUserCookieKey);
        }

        #endregion

        #region 获取用户Session

        /// <summary>
        ///     获取用户Session
        /// </summary>
        /// <returns></returns>
        public static SysUserModel GetSession()
        {
            SysUserModel sysUserModel = null;

            HttpSessionState httpSession = HttpContext.Current.Session;
            if (httpSession[_sysUserSessionKey] != null)
                sysUserModel = (SysUserModel) httpSession[_sysUserSessionKey];

            return sysUserModel;
        }

        #endregion

    }
}