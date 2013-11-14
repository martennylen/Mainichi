using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Mainichi.Web.Store.Filters;

namespace Mainichi.Web.Store.Controllers
{
    public class HomeController : BaseController
    {
        [HandleRavenSessionFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
