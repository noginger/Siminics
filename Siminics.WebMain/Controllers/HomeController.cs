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
    public class HomeController : BaseMain
    {
        // GET: Home
        public ActionResult Index()
        {

            DbQueryParamtersNoPage paramters = new DbQueryParamtersNoPage()
            {
                Condition = string.Format(" position={0}",(int)EnumConst.BannerPosition.Index),
                OrderBy = "order by sort asc"
            };
            List<SlideModel> models = new SlideBL().GetItems(paramters);

            ViewBag.Slides = models;

            return View();
        }
    }
}