using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using BaseLibrary.Common;
using Cee.Tools.Types;
using Cee.Tools.Web;
using Siminics.BL;
using Siminics.Model;
using Siminics.WebMain.Areas.Manage.Controllers;
using Siminics.WebMain.Models;

namespace Siminics.WebMain.Areas.Manage.Controllers
{
    public class ProductController : BaseController
    {
        public ActionResult Index()
        {
            int p = WebUtils.GetQueryInt("p", 1);
            string text = WebUtils.GetQuery("s_text");
            int status = WebUtils.GetQueryInt("s_filter", 0);

            ViewBag.Status = status;
            ViewBag.Text = text;

            string condition = " 1=1";

            if (!string.IsNullOrEmpty(text))
            {
                condition += string.Format(" And ProductName like '%{0}%'", text);
            }

            if (status > 0)
            {
                condition += string.Format(" And ProductType = {0}", status);
            }

            DbQueryParamters paramters = new DbQueryParamters()
            {
                ColumnFields = new[] { "a.ProductId","modelname","productname","modelid" },
                PageIndex = p,
                PageSize = 25,
                Condition = condition,
                OrderBy = "Order By ProductId Desc",
                Join = "a inner join product b on a.productId=b.productId",
                IsCount = true
            };

            var resource = new ProductBL().GetItems(paramters);

            ViewBag.Info = resource.DataSource;
            ViewBag.Count = resource.RecordCount;

            return View();
        }

        public ActionResult Add()
        {
            ViewBag.ProductTypes = BindProductType();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ProductViewModel entity, string customerMode, string price)
        {
            ViewBag.ProductTypes = BindProductType();
            ViewBag.Infomation = entity.Infomation;

            //if (entity.ProductType == (int)EnumConst.ProductType.Template && string.IsNullOrEmpty(entity.ImageSize))
            //{
            //    ViewBag.Error = "alert('模板类型的产品需填写模板分辨率！');";
            //    return View(entity);
            //}
            
            if (ModelState.IsValid)
            {
                ProductModelEntity model = new ProductModelEntity();
                model.modelname = entity.ProductName;
                model.productid = entity.ProductType;
                model.downurl = entity.DownAddress;
                model.apply = entity.Infomation;
                model.desc = entity.ShortDesc;
                model.sort = entity.Sort;

                OperationResult result = new ProductBL().AddModel(model);

                if (result.ResultType == OperationResultType.Success)
                    return RedirectToAction("Index");
                else
                    JsAlert(result.Message, (int)OperationResultType.Error);
                return View(entity);
            }
            return View(entity);
        }

        public ActionResult Edit()
        {
            return View();
        }


        #region 绑定产品类型
        /// <summary>
        /// 绑定产品类型
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> BindProductType(int selectId=0)
        {
            DbQueryParamtersNoPage paramters = new DbQueryParamtersNoPage()
            {
                ColumnFields = new[] { "ProductId", "ProductName" },
                OrderBy = "Order By ProductId Desc"
            };

            List<ProductModel> models = new ProductBL().GetItems(paramters);
            List<SelectListItem> typeItems = new List<SelectListItem>();
            typeItems.Add(new SelectListItem() { Selected = false, Text = "==选择类型==", Value = "0" });

            foreach (var item in models)
            {
                SelectListItem sItem = new SelectListItem() { Selected = item.ProductId == selectId, Text = item.productname, Value = item.ProductId.ToString() };
                typeItems.Add(sItem);
            }
            return typeItems;
        }

        #endregion
    }
}
