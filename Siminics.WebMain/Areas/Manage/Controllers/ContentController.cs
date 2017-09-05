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
using Siminics.WebMain.Models;

namespace Siminics.WebMain.Areas.Manage.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Manage/Content
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Company()
        {
            int p = WebUtils.GetQueryInt("p", 1);
            string text = WebUtils.GetQuery("s_text");
            int status = WebUtils.GetQueryInt("s_filter", 0);

            string condition = " 1=1";

            if (!string.IsNullOrEmpty(text))
            {
                condition += string.Format(" And title like '%{0}%'", text);
            }

            if (status > 0)
            {
                condition += string.Format(" And typeid = {0}", status);
            }

            DbQueryParamters paramters = new DbQueryParamters()
            {
                PageIndex = p,
                PageSize = 25,
                Condition = condition,
                OrderBy = "Order By contentId Desc",
                IsCount = true
            };

            var resource = new CompanyContentBL().GetItems(paramters);

            ViewBag.Types = BindCompanyContentType();

            ViewBag.Info = resource.DataSource;
            ViewBag.Count = resource.RecordCount;

            return View();
        }

        public ActionResult AddCompany()
        {
            ViewBag.ContentsTypes = BindCompanyContentType();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCompany(ContentViewModel entity)
        {
            ViewBag.Images = entity.ImageHtml;
            ViewBag.Content = entity.Content;
            ViewBag.ContentsTypes = BindCompanyContentType(entity.TypeId);

            CompanyContentModel model=new CompanyContentModel()
            {
                imageurl = entity.ImageUrl,
                content = entity.Content,
                sort = entity.Sort,
                title = entity.Title,
                typeid = entity.TypeId
            };

            var result = new CompanyContentBL().Add(model);

            if (result.ResultType == OperationResultType.Success)
                return RedirectToAction("Company");
            else
                JsAlert(result.Message, (int)OperationResultType.Failed);

            return View();
        }

        public ActionResult EditCompany(int id)
        {
            CompanyContentModel model = new CompanyContentBL().GetItem(id);

            if (model == null)
            {
                JsAlert("无该数据", (int)OperationResultType.Failed);
                return Redirect(HttpContext.Request.UrlReferrer.ToString());
            }

            ViewBag.ContentsTypes = BindCompanyContentType(model.typeid);

            ContentViewModel entity=new ContentViewModel()
            {
                Content = model.content,
                ContentId = model.contentid,
                ImageUrl = model.imageurl,
                Sort = model.sort,
                Title = model.title,
                TypeId = model.typeid
            };

            ViewBag.Content = entity.Content;
            entity.ImageHtml= string.IsNullOrEmpty(entity.ImageUrl) ? "" : "<img src=" + entity.ImageUrl + " width='230' height='230' id='upimg'>";
            ViewBag.Images = entity.ImageHtml;

            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCompany(ContentViewModel model)
        {
            ViewBag.ContentsTypes = BindCompanyContentType(model.TypeId);
            ViewBag.Content = model.Content;

            CompanyContentModel entity = new CompanyContentModel()
            {
                contentid = model.ContentId,
                content = model.Content,
                imageurl = model.ImageUrl,
                sort = model.Sort,
                title = model.Title,
                typeid = model.TypeId
            };

            var result = new CompanyContentBL().Edit(entity);

            if (result.ResultType == OperationResultType.Success)
                return RedirectToAction("Company");
            else
                JsAlert(result.Message, (int)OperationResultType.Failed);

            return View(model);
        }

        public ActionResult News()
        {
            int p = WebUtils.GetQueryInt("p", 1);
            string text = WebUtils.GetQuery("s_text");
            int status = WebUtils.GetQueryInt("s_filter", 0);

            string condition = " 1=1";

            if (!string.IsNullOrEmpty(text))
            {
                condition += string.Format(" And title like '%{0}%'", text);
            }

            if (status > 0)
            {
                condition += string.Format(" And typeid = {0}", status);
            }

            DbQueryParamters paramters = new DbQueryParamters()
            {
                PageIndex = p,
                PageSize = 25,
                Condition = condition,
                OrderBy = "Order By newsId Desc",
                IsCount = true
            };

            var resource = new NewsContentBL().GetItems(paramters);

            ViewBag.Types = BindNewsContentType();

            ViewBag.Info = resource.DataSource;
            ViewBag.Count = resource.RecordCount;

            return View();
        }

        public ActionResult AddNews()
        {
            ViewBag.ContentsTypes = BindNewsContentType();
            ViewBag.ProductTypes = new SurportTypeController().BindSurportType(0,true);

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNews(ContentViewModel entity)
        {
            ViewBag.ContentsTypes = BindNewsContentType();
            ViewBag.Content = entity.Content;
            ViewBag.Images = entity.ImageHtml;
            ViewBag.ProductTypes = new SurportTypeController().BindSurportType(0,true);

            NewsContentModel model=new NewsContentModel()
            {
                ProductId = entity.ProductId,
                content = entity.Content,
                createtime = DateTime.Now,
                imageurl = entity.ImageUrl,
                shortdesc = entity.ShortDesc,
                Sort = entity.Sort,
                title = entity.Title,
                typeid = entity.TypeId
            };

            var result = new NewsContentBL().Add(model);

            if (result.ResultType == OperationResultType.Success)
                return RedirectToAction("News");
            else
                JsAlert(result.Message, (int)OperationResultType.Failed);

            return View();
        }

        public ActionResult EditNews(int id)
        {
            NewsContentModel model = new NewsContentBL().GetItem(id);
            if (model == null)
            {
                JsAlert("无该数据", (int)OperationResultType.Failed);
                return Redirect(HttpContext.Request.UrlReferrer.ToString());
            }

            ViewBag.ProductTypes = new SurportTypeController().BindSurportType(model.ProductId,true);
            ViewBag.ContentsTypes = BindNewsContentType(model.typeid);

            ContentViewModel entity=new ContentViewModel()
            {
                ContentId = model.NewsId,
                Content = model.content,
                ImageUrl = model.imageurl,
                ProductId = model.ProductId,
                ShortDesc = model.shortdesc,
                Sort = model.Sort,
                Title = model.title,
                TypeId = model.typeid
            };
            ViewBag.Content = entity.Content;
            entity.ImageHtml = string.IsNullOrEmpty(entity.ImageUrl) ? "" : "<img src=" + entity.ImageUrl + " width='230' height='230' id='upimg'>";
            ViewBag.Images = entity.ImageHtml;

            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditNews(ContentViewModel entity)
        {
            ViewBag.Images = entity.ImageHtml;
            ViewBag.Content = entity.Content;
            ViewBag.ContentsTypes = BindNewsContentType(entity.TypeId);
            ViewBag.ProductTypes = new SurportTypeController().BindSurportType(entity.ProductId,true);

            NewsContentModel model=new NewsContentModel()
            {
                NewsId = entity.ContentId,
                content = entity.Content,
                imageurl = entity.ImageUrl,
                ProductId = entity.ProductId,
                shortdesc = entity.ShortDesc,
                Sort = entity.Sort,
                title = entity.Title,
                typeid = entity.TypeId
            };

            var result = new NewsContentBL().Edit(model);

            if (result.ResultType == OperationResultType.Success)
                return RedirectToAction("News");
            else
                JsAlert(result.Message, (int)OperationResultType.Failed);

            return View(entity);
        }

        #region 绑定内容类型
        /// <summary>
        /// 绑定内容类型
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> BindCompanyContentType(int selectId = 0)
        {
            EnumDescription[] types = EnumDescription.GetFieldTexts(typeof(EnumConst.CompanyContentType));

            List<SelectListItem> typeItems = new List<SelectListItem>();
            typeItems.Add(new SelectListItem() { Selected = false, Text = "==选择类型==", Value = "0" });

            foreach (var item in types)
            {
                SelectListItem sItem = new SelectListItem() { Selected = item.EnumValue == selectId, Text = item.EnumDisplayText, Value = item.EnumValue.ToString() };
                typeItems.Add(sItem);
            }
            return typeItems;
        }


        /// <summary>
        /// 绑定内容类型
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> BindNewsContentType(int selectId = 0)
        {
            EnumDescription[] types = EnumDescription.GetFieldTexts(typeof(EnumConst.NewsContentType));

            List<SelectListItem> typeItems = new List<SelectListItem>();
            typeItems.Add(new SelectListItem() { Selected = false, Text = "==选择类型==", Value = "0" });

            foreach (var item in types)
            {
                SelectListItem sItem = new SelectListItem() { Selected = item.EnumValue == selectId, Text = item.EnumDisplayText, Value = item.EnumValue.ToString() };
                typeItems.Add(sItem);
            }
            return typeItems;
        }

        #endregion

        public ActionResult DeleteNews(int id)
        {
            new NewsContentBL().Delete(id);
            return RedirectToAction("Company");
        }


        public ActionResult DeleteCompany(int id)
        {
            new CompanyContentBL().Delete(id);
            return RedirectToAction("Company");
        }
    }
}