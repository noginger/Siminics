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
            ViewBag.Products = BindProductType(status);

            ViewBag.Status = status;
            ViewBag.Text = text;

            string condition = " 1=1";

            if (!string.IsNullOrEmpty(text))
            {
                condition += string.Format(" And modelname like '%{0}%'", text);
            }

            if (status > 0)
            {
                condition += string.Format(" And a.productid = {0}", status);
            }

            DbQueryParamters paramters = new DbQueryParamters()
            {
                ColumnFields = new[] { "a.ProductId","modelname","productname","modelid","a.sort" },
                PageIndex = p,
                PageSize = 25,
                Condition = condition,
                OrderBy = "Order By a.ProductId Desc",
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
        public ActionResult Add(ProductViewModel entity)
        {
            ViewBag.ProductTypes = BindProductType();
            ViewBag.Infomation = entity.Infomation;
            ViewBag.Images = entity.ImagesHtml;
            ViewBag.AnotherImages = entity.AnotherImageHtml;

            if (string.IsNullOrEmpty(entity.ProductName))
            {
                ViewBag.Error = "alert('请填写产品型号！');";
                return View(entity);
            }

            if (ModelState.IsValid)
            {
                ProductModelEntity model = new ProductModelEntity();
                model.modelname = entity.ProductName;
                model.productid = entity.ProductType;
                model.downurl = entity.DownAddress;
                model.apply = entity.Infomation;
                model.desc = entity.ShortDesc;
                model.sort = entity.Sort;

                model.Images = new List<ModelSourceModel>();
                if (!string.IsNullOrEmpty(entity.ShowImage))
                {
                    foreach (var image in entity.ShowImage.Split(','))
                    {
                        model.Images.Add(new ModelSourceModel() { imageurl = image, TypeId = 1 });
                    }
                }

                if (!string.IsNullOrEmpty(entity.AnotherImages))
                {
                    foreach (var image in entity.AnotherImages.Split(','))
                    {
                        model.Images.Add(new ModelSourceModel() { imageurl = image, TypeId = 2 });
                    }
                }
                

                OperationResult result = new ProductBL().AddModel(model);

                if (result.ResultType == OperationResultType.Success)
                    return RedirectToAction("Index");
                else
                    JsAlert(result.Message, (int)OperationResultType.Error);
                return View(entity);
            }
            return View(entity);
        }

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                JsAlert("参数错误！", (int)OperationResultType.Failed);
                return Redirect(HttpContext.Request.UrlReferrer.ToString());
            }

            ProductModelEntity model = new ProductBL().GetItem(id);

            if (model == null)
            {
                JsAlert("该产品不存在", (int)OperationResultType.Failed);
                return Redirect(HttpContext.Request.UrlReferrer.ToString());
            }

            ProductViewModel entity = new ProductViewModel();
            entity.ModelId = id;
            entity.ProductName = model.modelname;
            entity.ProductType = model.productid;
            entity.DownAddress = model.downurl;
            entity.Infomation = model.apply;
            entity.ShortDesc = model.desc;
            entity.Sort = model.sort;
            entity.ShowImage = string.Join(",",model.Images.Where(o=>o.TypeId==1).Select(o=>o.imageurl));
            entity.AnotherImages = string.Join(",", model.Images.Where(o => o.TypeId == 2).Select(o => o.imageurl));
            
            entity.ImagesHtml = GetImageHtml(model.Images, 1);
            ViewBag.Images = entity.ImagesHtml;

            entity.AnotherImageHtml = GetImageHtml(model.Images, 2);
            ViewBag.AnotherImages = entity.AnotherImageHtml;

            ViewBag.ProductTypes = BindProductType(model.productid);
            ViewBag.Infomation = entity.Infomation;
            ViewBag.ShortDesc = entity.ShortDesc;

            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductViewModel entity)
        {
            ViewBag.ProductTypes = BindProductType();
            ViewBag.Infomation = entity.Infomation;
            ViewBag.ShortDesc = entity.ShortDesc;
           
            if (ModelState.IsValid)
            {
                ProductModelEntity model = new ProductModelEntity();
                model.productid = entity.ProductType;
                model.modelname = entity.ProductName;
                model.ModelId = entity.ModelId;
                model.downurl = entity.DownAddress;
                model.apply = entity.Infomation;
                model.sort = entity.Sort;
                model.desc = entity.ShortDesc;

                model.Images = new List<ModelSourceModel>();
                if (!string.IsNullOrEmpty(entity.ShowImage))
                {
                    foreach (var image in entity.ShowImage.Split(','))
                    {
                        model.Images.Add(new ModelSourceModel() { imageurl = image, TypeId = 1 });
                    }
                }

                if (!string.IsNullOrEmpty(entity.AnotherImages))
                {
                    foreach (var image in entity.AnotherImages.Split(','))
                    {
                        model.Images.Add(new ModelSourceModel() { imageurl = image, TypeId = 2 });
                    }
                }

                OperationResult result = new ProductBL().EditModel(model);

                if (result.ResultType == OperationResultType.Success)
                    return RedirectToAction("Index");
                else
                    JsAlert(result.Message, (int)OperationResultType.Failed);
                return View(entity);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            new ProductBL().Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteType(int id)
        {
            new ProductBL().DeleteType(id);
            return RedirectToAction("ProductType");
        }


        public ActionResult ProductType()
        {
            int p = WebUtils.GetQueryInt("p", 1);
            DbQueryParamters paramters = new DbQueryParamters()
            {
                ColumnFields = new[] { "ProductId", "productname","sort","imageurl" },
                PageIndex = p,
                PageSize = 25,
                OrderBy = "Order By ProductId Desc",
                IsCount = true
            };

            var resource = new ProductBL().GetProductItems(paramters);
            ViewBag.Info = resource.DataSource;
            ViewBag.Count = resource.RecordCount;

            return View();
        }

        public ActionResult AddType()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddType(ProductViewModel entity)
        {
            var result = new ProductBL().Add(entity.ProductName,entity.Sort,entity.ShowImage);
            if (result.ResultType == OperationResultType.Success)
                return RedirectToAction("ProductType");
            else
                JsAlert(result.Message, (int)OperationResultType.Error);
            return View(entity);
        }

        public ActionResult EditType(int id)
        {
            if (id <= 0)
            {
                JsAlert("参数错误！", (int)OperationResultType.Failed);
                return Redirect(HttpContext.Request.UrlReferrer.ToString());
            }

            ProductModel model = new ProductBL().GetProductItem(id);

            if (model == null)
            {
                JsAlert("无该产品", (int)OperationResultType.Failed);
                return Redirect(HttpContext.Request.UrlReferrer.ToString());
            }

            ProductViewModel entity=new ProductViewModel()
            {
                ProductName = model.productname,
                ProductId = model.ProductId,
                Sort = model.sort,
                ShowImage = model.ImageUrl
            };

            entity.ImagesHtml = string.IsNullOrEmpty(model.ImageUrl) ? "" : "<img src=" + model.ImageUrl + " width='230' height='230' id='upimg'>";
            ViewBag.Images = entity.ImagesHtml;

            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditType(ProductViewModel model)
        {
            ProductModel entity=new ProductModel()
            {
                ProductId = model.ProductId,
                productname = model.ProductName,
                sort = model.Sort
            };

            OperationResult result = new ProductBL().EditType(entity);
            if (result.ResultType == OperationResultType.Success)
                return RedirectToAction("ProductType");
            else
                JsAlert(result.Message, (int)OperationResultType.Failed);

            return View(model);
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


        private string GetImageHtml(IList<ModelSourceModel> images,int type)
        {
            string imageHtml = "";
            foreach (var image in images)
            {
                if (image.TypeId != type)
                    continue;

                if (imageHtml.Length == 0)
                {
                    imageHtml = string.Format("<img src='{0}' width='230' height='230' id='{1}'>", image.imageurl,image.TypeId==1?"upimg": "upAnotherimg");
                    continue;
                }
                imageHtml += string.Format("<img src='{0}' width='230' height='230'>", image.imageurl);
            }

            return imageHtml;
        }
    }
}
