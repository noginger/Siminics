using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using BaseLibrary.Common;
using Cee.Tools;
using Cee.Tools.DEncrypt;
using Cee.Tools.Web;
using Siminics.BL;
using Siminics.Model;
using Siminics.WebMain.Global;
using Siminics.WebMain.Models;

namespace Siminics.WebMain.Areas.Manage.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(LoginModel model)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.PassWord))
            {
                JsciptAlert.Alert("请输入用户名/密码");
                return View();
            }
            try
            {
                model.LoginDate = DateTime.Now;
                model.PassWord = DEncrypt.MD5By16(model.PassWord);
                model.LoginIp = WebUtils.GetIP();
                OperationResult result = new SysUser().Login(LoginMaper.ModelMapInfo(model));

                string msg = result.Message ?? Cee.Tools.Types.EnumDescription.GetFieldText(result.ResultType);
                if (result.ResultType == OperationResultType.Success)
                {
                    var sysUserModel = LoginProccess.InsertSession((SysUserInfo)result.AppendData);
                    
                    SysUserInfo sysUserInfo = (SysUserInfo)result.AppendData;
                    TimeSpan timeSpan = new TimeSpan(0, 1, 0);
                    DateTime expiration = model.IsRememberLogin ? DateTime.Now.AddDays(1) : DateTime.Now.Add(timeSpan);
                    HttpCookie cookie = new HttpCookie(LoginProccess._sysUserCookieKey);
                    if (model.IsRememberLogin)
                    {
                        cookie.Expires = DateTime.Now.AddDays(1);
                    }
                    Response.Cookies.Set(cookie);

                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    JsciptAlert.Alert(msg);
                }

                return View(model);
            }
            catch (Exception e)
            {
                JsciptAlert.Alert(StringHelper.ReplaceHtml(e.Message));
                return View(model);
            }
            return View();
        }

        public ActionResult LoginOut()
        {
            HttpSessionState httpSession = System.Web.HttpContext.Current.Session;
            LoginProccess.ClearSession();

            return Redirect("/login");
        }
    }
}