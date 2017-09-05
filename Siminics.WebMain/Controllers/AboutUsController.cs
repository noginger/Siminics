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
    public class AboutUsController : BaseMain
    {
        // GET: AboutUs
        public ActionResult Index()
        {
            DbQueryParamtersNoPage paramters=new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" typeid={0}",(int)EnumConst.CompanyContentType.CompanyIntro),
                OrderBy = "Order By sort asc"
            };

            var contents = new CompanyContentBL().GetItems(paramters);
            if (contents.Any(o => o.contentid > 0))
            {
                ViewBag.CompanyInfo = contents.Find(o => o.contentid > 0).content;
            }
            
            paramters = new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" position={0}", (int)EnumConst.BannerPosition.Company),
                OrderBy = "Order by sort asc",
            };

            List<SlideModel> models = new SlideBL().GetItems(paramters);

            ViewBag.Slide = models.Find(o => o.id > 0);

            return View();
        }
    }
}