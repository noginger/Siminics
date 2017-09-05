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
    public class BaseMain: Controller
    {
        public BaseMain()
        {
            DbQueryParamtersNoPage paramters = new DbQueryParamtersNoPage()
            {
                OrderBy = "order by sort asc"
            };

            ViewBag.ProductTypes = new ProductBL().GetItems(paramters);

            paramters = new DbQueryParamtersNoPage()
            {
                Condition = " parentid=0",
                OrderBy = "order by sort asc"
            };

            ViewBag.SurportTypes = new SurportTypeBL().GetItems(paramters);
        }
    }
}