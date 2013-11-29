using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Mainichi.Web.Store.ViewModels;

namespace Mainichi.Web.Store.Controllers
{
    public class HomeController : BaseController
    {
        //[HandleRavenSessionFilter]
        public ActionResult Index()
        {
            var categories = RavenSession.Load<Categories>("config/categories");
            return View(categories);
        }
    }
}