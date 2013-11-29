using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mainichi.Web.Store.ViewModels;

namespace Mainichi.Web.Store.Controllers
{
    public class ThingController : BaseController
    {
        //
        // GET: /Thing/

        public ActionResult Details(string id)
        {
            var thing = RavenSession.Load<Thing>("things/" + id.Split(new char[] {'-'}).First());
            return View(thing);
        }
    }
}
