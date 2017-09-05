using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseLibrary.Common;
using Cee.Tools.Web;
using Siminics.BL;
using Siminics.Model;

namespace Siminics.WebMain.Controllers
{
    public class SurportController : BaseMain
    {
        // GET: Surport
        public ActionResult Index()
        {
            DbQueryParamtersNoPage paramters = new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" position={0}", (int)EnumConst.BannerPosition.Surport),
                OrderBy = "Order by sort asc",
            };

            List<SlideModel> models = new SlideBL().GetItems(paramters);

            ViewBag.Slide = models.Find(o => o.id > 0);


            int id = WebUtils.GetQueryInt("id", 0);

            if (id <= 0)
            {
                return RedirectToAction("Index", "Home");
            }

            paramters = new DbQueryParamtersNoPage()
            {
                Condition = string.Format(" parentid={0}",id),
                OrderBy = "Order by sort asc"
            };

            ViewBag.List = new SurportTypeBL().GetItems(paramters);

            return View();
        }

        public ActionResult List()
        {
            int id = WebUtils.GetQueryInt("id", 0);
            if (id <= 0)
            {
                return RedirectToAction("Index");
            }

            DbQueryParamtersNoPage paramters = new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" position={0}", (int)EnumConst.BannerPosition.Surport),
                OrderBy = "Order by sort asc",
            };

            List<SlideModel> models = new SlideBL().GetItems(paramters);
            ViewBag.Slide = models.Find(o => o.id > 0);

            paramters =new DbQueryParamtersNoPage()
            {
                Condition = string.Format(" productid={0}",id),
                OrderBy = "Order by sort asc",
            };

            ViewBag.List = new NewsContentBL().GetItems(paramters);

            return View();
        }


        public ActionResult Detail()
        {
            int id = WebUtils.GetQueryInt("id", 0);
            if (id <= 0)
            {
                return RedirectToAction("Index");
            }

            NewsContentModel model = new NewsContentBL().GetItem(id);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.NewInfo = model;

            return View();
        }

        public ActionResult CommonQuestion()
        {
            DbQueryParamtersNoPage paramters = new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" position={0}", (int)EnumConst.BannerPosition.Surport),
                OrderBy = "Order by sort asc",
            };

            List<SlideModel> models = new SlideBL().GetItems(paramters);
            ViewBag.Slide = models.Find(o => o.id > 0);

            paramters = new DbQueryParamtersNoPage()
            {
                Condition = string.Format(" typeid={0}", (int)EnumConst.NewsContentType.CommconQuestion),
                OrderBy = "Order by sort asc"
            };

            ViewBag.List = new NewsContentBL().GetItems(paramters);

            return View();
        }
    }
}