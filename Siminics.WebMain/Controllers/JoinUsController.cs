using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseLibrary.Common;
using Siminics.BL;
using Siminics.Model;

namespace Siminics.WebMain.Controllers
{
    public class JoinUsController : BaseMain
    {
        // GET: JoinUs
        public ActionResult Index()
        {
            DbQueryParamtersNoPage paramters = new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" position={0}", (int)EnumConst.BannerPosition.JoinUs),
                OrderBy = "Order by sort asc",
            };

            List<SlideModel> models = new SlideBL().GetItems(paramters);
            ViewBag.Slide = models.Find(o => o.id > 0);

            paramters = new DbQueryParamtersNoPage()
            {
                Condition = string.Format(" typeid={0}", (int)EnumConst.CompanyContentType.JoinUs),
                OrderBy = "Order by sort asc"
            };

            ViewBag.List = new CompanyContentBL().GetItems(paramters);

            return View();
        }
    }
}