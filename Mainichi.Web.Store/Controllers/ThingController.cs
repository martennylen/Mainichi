using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mainichi.Web.Store.Models;

namespace Mainichi.Web.Store.Controllers
{
    public class ThingController : Controller
    {
        //
        // GET: /Thing/

        public ActionResult Details(int id)
        {
            var m = new ThingModels.Thing(id);
            return View(m);
        }
    }
}
