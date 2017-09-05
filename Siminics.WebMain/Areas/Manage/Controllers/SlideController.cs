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

namespace Siminics.WebMain.Areas.Manage.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Manage/Slide
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
                condition += string.Format(" And `name` like '%{0}%'", text);
            }

            if (status > 0)
            {
                condition += string.Format(" And position = {0}", status);
            }

            DbQueryParamters paramters = new DbQueryParamters()
            {
                PageIndex = p,
                PageSize = 25,
                Condition = condition,
                OrderBy = "Order By id Desc",
                IsCount = true
            };

            var resource = new SlideBL().GetItems(paramters);

            ViewBag.Info = resource.DataSource;
            ViewBag.Count = resource.RecordCount;

            return View();
        }

        public ActionResult Add()
        {
            ViewBag.PositionTypes = BindPosition();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(SlideModel model)
        {
            OperationResult result = new SlideBL().Add(model);
            model.title = model.name;

            if (result.ResultType == OperationResultType.Success)
                return RedirectToAction("Index");
            else
                JsAlert(result.Message, (int)OperationResultType.Failed);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (id<=0)
            {
                JsAlert("参数错误", (int)OperationResultType.Failed);
                return Redirect(HttpContext.Request.UrlReferrer.ToString());
            }

            SlideModel model = new SlideBL().GetItem(id);

            if (model == null)
            {
                JsAlert("无该数据", (int)OperationResultType.Failed);
                return Redirect(HttpContext.Request.UrlReferrer.ToString());
            }

            ViewBag.PositionTypes = BindPosition(model.position);
            model.ImageHtml= string.IsNullOrEmpty(model.imageurl) ? "" : "<img src=" + model.imageurl + " width='230' height='230' id='upimg'>";
            ViewBag.Images = model.ImageHtml;

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SlideModel model)
        {
            ViewBag.PositionTypes = BindPosition(model.position);
            ViewBag.Images = model.ImageHtml;
            model.title = model.name;

            OperationResult result = new SlideBL().Edit(model);

            if (result.ResultType == OperationResultType.Success)
                return RedirectToAction("Index");
            else
                JsAlert(result.Message, (int)OperationResultType.Failed);

            return View();
        }

        public ActionResult Delete(int id)
        {
            new SlideBL().Delete(id);
            return RedirectToAction("Index");
        }


        /// <summary>
        /// 绑定类型
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> BindPosition(int selectId = 0)
        {
            EnumDescription[] types = EnumDescription.GetFieldTexts(typeof(EnumConst.BannerPosition));

            List<SelectListItem> typeItems = new List<SelectListItem>();
            typeItems.Add(new SelectListItem() { Selected = false, Text = "==选择类型==", Value = "0" });

            foreach (var item in types)
            {
                SelectListItem sItem = new SelectListItem() { Selected = item.EnumValue == selectId, Text = item.EnumDisplayText, Value = item.EnumValue.ToString() };
                typeItems.Add(sItem);
            }
            return typeItems;
        }
    }
}