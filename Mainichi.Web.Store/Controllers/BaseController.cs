using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mainichi.Web.Store.App_Start;
using Raven.Client;

namespace Mainichi.Web.Store.Controllers
{
    public class BaseController : Controller
    {
        public IDocumentSession RavenSession { get; set; }
    }
}