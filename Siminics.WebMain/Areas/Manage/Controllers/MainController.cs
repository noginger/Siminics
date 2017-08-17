using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Siminics.WebMain.Areas.Manage.Controllers
{
    public class MainController : BaseController
    {
        // GET: Manage/Main
        public ActionResult Index()
        {
            return View();
        }
    }
}