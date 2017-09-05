using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseLibrary.Common;
using Cee.Tools.Web;
using Siminics.BL;

namespace Siminics.WebMain.Areas.Manage.Controllers
{
    public class CustomerMessageController : BaseController
    {
        // GET: Manage/CustomerMessage
        public ActionResult Index()
        {
            int p = WebUtils.GetQueryInt("p", 1);
            string text = WebUtils.GetQuery("s_text");
            int status = WebUtils.GetQueryInt("s_filter", 0);

            ViewBag.Status = status;
            ViewBag.Text = text;
            
            DbQueryParamters paramters = new DbQueryParamters()
            {
                PageIndex = p,
                PageSize = 25,
                OrderBy = "Order By id Desc",
                IsCount = true
            };

            var resource = new LeaveMessageBL().GetItems(paramters);

            ViewBag.Info = resource.DataSource;
            ViewBag.Count = resource.RecordCount;

            return View();


            return View();
        }
    }
}