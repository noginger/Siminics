using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Siminics.WebMain.Global;

namespace Siminics.WebMain.Areas.Manage.Controllers
{
    public class BaseController:Controller
    {
        public SysUserModel _sysUserModel = new SysUserModel();
        private RequestContext _requestContext;
        private string _cookieUrlRequestKey = "_cookieUrlRequestKey";

        /// <summary>
        /// 当前地址
        /// </summary>
        public string CurrentUrl { get; set; }
        /// <summary>
        /// 上一页地址
        /// </summary>
        public string PreviousUrl { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            _requestContext = requestContext;
            //验证系统用户登录状态
            _sysUserModel = LoginProccess.GetSession();
            
            if (_sysUserModel == null || string.IsNullOrEmpty(_sysUserModel.EncryptLoginId))
            {
                _requestContext.HttpContext.Response.Redirect("/Manage/Login");
                return;
            }

            ViewBag.SysUser = _sysUserModel;
            //ViewBag.LoginId = _sysUserModel.EncryptLoginId;
            //_Main_Frame_Nav();
            //_Crumbs(requestContext);
            //_Main_Frame_Footer();

            base.Initialize(requestContext);
        }

        ///// <summary>
        ///// Action处理前
        ///// </summary>
        ///// <param name="filterContext"></param>
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);
        //}

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                //记录应用程序异常
            }
            base.OnException(filterContext);
        }

        #region JsAlert

        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="strTitle">提示文字</param>
        /// <param name="url">返回地址:-1(返回上页),0(当前页面),自定义返回URL</param>
        /// <param name="cssType">CSS样式[0：错误，1：成功，2：系统提示]</param>
        protected void JsAlert(string strTitle, int cssType)
        {
            var msbox = "showTip(\"" + strTitle + "\", \"" + cssType + "\",'');";
            TempData["JsPrint"] = msbox;
        }


        #endregion
    }
}