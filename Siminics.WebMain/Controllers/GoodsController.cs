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
                Condition = string.Format(" productid={0}", typeId),
                OrderBy = "Order by sort asc",
            };

            List<ProductModel> models = new ProductBL().GetItems(paramters);

            ViewBag.Slide = models.Find(o => o.ProductId > 0);

            paramters=new DbQueryParamtersNoPage()
            {
                PageSize = 20,
                Condition = string.Format(" productid={0}",typeId),
                OrderBy = "Order by sort asc"
            };

            List<ProductModelEntity> productModels = new ProductBL().GetModelItems(paramters);

            if (!productModels.Any(o => o.ModelId > 0))
            {
                ViewBag.ProductModels = new List<ProductModelEntity>();
                return View();
            }

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