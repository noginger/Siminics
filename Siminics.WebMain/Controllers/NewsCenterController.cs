using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseLibrary.Common;
using Cee.Tools.Types;
using Cee.Tools.Web;
using Siminics.BL;
using Siminics.Model;

namespace Siminics.WebMain.Controllers
{
    public class NewsCenterController : BaseMain
    {
        // GET: NewCenter
        public ActionResult Index()
        {
            DbQueryParamtersNoPage paramters = new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" position={0}", (int)EnumConst.BannerPosition.News),
                OrderBy = "Order by sort asc",
            };

            List<SlideModel> models = new SlideBL().GetItems(paramters);

            ViewBag.Slide = models.Find(o => o.id > 0);

            paramters=new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" typeid={0}",(int)EnumConst.NewsContentType.News),
                OrderBy = "Order by sort asc",
            };

            var news = new NewsContentBL().GetItems(paramters);

            paramters = new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" typeid={0}", (int)EnumConst.NewsContentType.Active),
                OrderBy = "Order by sort asc",
            };

            var active = new NewsContentBL().GetItems(paramters);

            paramters = new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" typeid={0}", (int)EnumConst.NewsContentType.Media),
                OrderBy = "Order by sort asc",
            };

            var media = new NewsContentBL().GetItems(paramters);

            ViewBag.News = news.Find(o => o.NewsId > 0);
            ViewBag.Active = active.Find(o => o.NewsId > 0);
            ViewBag.Media = media.Find(o => o.NewsId > 0);

            return View();
        }

        public ActionResult List()
        {
            int tId = WebUtils.GetQueryInt("tid", (int)EnumConst.NewsContentType.News);

            if (tId < (int) EnumConst.NewsContentType.News || tId > (int) EnumConst.NewsContentType.Media)
            {
                tId = (int) EnumConst.NewsContentType.News;
            }

            DbQueryParamtersNoPage paramters=new DbQueryParamtersNoPage()
            {
                Condition = string.Format(" typeid={0}",tId),
                OrderBy = "Order By sort asc"
            };

            var list = new NewsContentBL().GetItems(paramters);

            ViewBag.List = list;
            ViewBag.TypeId = tId;

            ViewBag.Name = ((EnumConst.NewsContentType) tId).ToDesc();

            paramters = new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" position={0}", (int)EnumConst.BannerPosition.News),
                OrderBy = "Order by sort asc",
            };

            List<SlideModel> models = new SlideBL().GetItems(paramters);

            ViewBag.Slide = models.Find(o => o.id > 0);

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
    }
}