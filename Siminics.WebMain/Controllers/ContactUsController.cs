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
    public class ContactUsController : BaseMain
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            DbQueryParamtersNoPage paramters = new DbQueryParamtersNoPage()
            {
                PageSize = 1,
                Condition = string.Format(" position={0}", (int)EnumConst.BannerPosition.ContactUs),
                OrderBy = "Order by sort asc",
            };

            List<SlideModel> models = new SlideBL().GetItems(paramters);
            ViewBag.Slide = models.Find(o => o.id > 0);

            paramters=new DbQueryParamtersNoPage()
            {
                Condition = string.Format(" typeid in ({0})",string.Join(",",new List<int>() {(int)EnumConst.CompanyContentType.Contact,(int)EnumConst.CompanyContentType.Traffic})),
                OrderBy = "Order By sort asc",
                
            };

            var lists = new CompanyContentBL().GetItems(paramters);

            ViewBag.Traffic = lists.Find(o => o.typeid == (int) EnumConst.CompanyContentType.Traffic);
            ViewBag.Contact = lists.Find(o => o.typeid == (int) EnumConst.CompanyContentType.Contact);

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(LeaveMessageModel entity)
        {
            if (string.IsNullOrEmpty(entity.Company) || string.IsNullOrEmpty(entity.Email) ||
                string.IsNullOrEmpty(entity.Message) || string.IsNullOrEmpty(entity.Phone) ||
                string.IsNullOrEmpty(entity.Company))
            {
                ViewBag.Error = "请填写完整内容";
                return View(entity);
            }

            int row=new LeaveMessageBL().Add(entity);
            if (row > 0)
            {
                ViewBag.Error = "提交成功，我们将尽快回复";
                return View();
            }
            else
            {
                ViewBag.Error = "提交失败，请重试";
                return View();
            }
        }
    }
}