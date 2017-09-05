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
    public class GoodsController : BaseMain
    {
        // GET: Goods
        public ActionResult Index()
        {
            int typeId = WebUtils.GetQueryInt("id", 0);
            if (typeId <= 0)
            {
                return RedirectToAction("Index");
            }

            DbQueryParamtersNoPage paramters=new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" position={0}", (int)EnumConst.BannerPosition.Product),
                OrderBy = "Order by sort asc",
            };

            List<SlideModel> models = new SlideBL().GetItems(paramters);

            ViewBag.Slide = models.Find(o => o.id > 0);

            paramters=new DbQueryParamtersNoPage()
            {
                PageSize = 20,
                Condition = string.Format(" productid={0}",typeId),
                OrderBy = "Order by sort asc"
            };

            List<ProductModelEntity> productModels = new ProductBL().GetModelItems(paramters);

            var images = new ProductBL().GetModelImages(productModels.Select(o => o.ModelId).ToList());

            foreach (var item in productModels)
            {
                item.Images = images.Where(o => o.modelid == item.ModelId).ToList();
            }
            
            ViewBag.ProductModels = productModels;

            return View();
        }
    }
}