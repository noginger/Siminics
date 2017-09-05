using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseLibrary.Common;
using Cee.Tools.Web;
using Siminics.BL;
using Siminics.Model;

namespace Siminics.WebMain.Areas.Manage.Controllers
{
    public class SurportTypeController : Controller
    {
        // GET: Manage/SurportType
        public ActionResult Index()
        {
            int p = WebUtils.GetQueryInt("p", 1);
            DbQueryParamters paramters = new DbQueryParamters()
            {
                PageIndex = p,
                PageSize = 25,
                OrderBy = "Order By parentid asc,sort asc",
                IsCount = true
            };

            var resource = new SurportTypeBL().GetItems(paramters);
            ViewBag.Info = resource.DataSource;
            ViewBag.Count = resource.RecordCount;

            return View();
        }

        public ActionResult Add()
        {
            ViewBag.SurportTypes = BindSurportType();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(SurportTypeModel entity)
        {
            ViewBag.Images = entity.ImageHtml;
            ViewBag.SurportTypes = BindSurportType();
            OperationResult result = new SurportTypeBL().Add(entity);
            if (result.ResultType == OperationResultType.Success)
            {
                return RedirectToAction("Index");
            }
            
            return View(entity);
        }

        public ActionResult Edit(int id)
        {
            var model = new SurportTypeBL().GetItem(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.SurportTypes = BindSurportType(model.ParentId);
            ViewBag.Images = string.IsNullOrEmpty(model.ImageUrl) ? "" : string.Format("<img src='{0}' width='230' height='230' id='{1}'>",model.ImageUrl,model.Id);

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SurportTypeModel entity)
        {
            ViewBag.SurportTypes = BindSurportType(entity.ParentId);
            ViewBag.Images = entity.ImageHtml;

            OperationResult result = new SurportTypeBL().Edit(entity);

            if (result.ResultType == OperationResultType.Success)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        public ActionResult Delete(int id)
        {
            new SurportTypeBL().Delete(id);
            return RedirectToAction("Index");
        }

        #region 绑定类型
        /// <summary>
        /// 绑定类型
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> BindSurportType(int selectId = 0,bool child = false)
        {
            DbQueryParamtersNoPage paramters = new DbQueryParamtersNoPage()
            {
                Condition = child?" parentid<>0":" parentid=0",
                OrderBy = "Order By sort asc"
            };

            var models = new SurportTypeBL().GetItems(paramters);
            List<SelectListItem> typeItems = new List<SelectListItem>();
            typeItems.Add(new SelectListItem() { Selected = false, Text = "==选择类型==", Value = "0" });

            foreach (var item in models)
            {
                SelectListItem sItem = new SelectListItem() { Selected = item.Id == selectId, Text = item.TypeName, Value = item.Id.ToString() };
                typeItems.Add(sItem);
            }
            return typeItems;
        }

        #endregion
    }
}