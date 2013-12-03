using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Mainichi.Web.Store.App_Start;
using Mainichi.Web.Store.Models.Indexes;
using Mainichi.Web.Store.ViewModels;
using Raven.Client;
using Raven.Client.Indexes;

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