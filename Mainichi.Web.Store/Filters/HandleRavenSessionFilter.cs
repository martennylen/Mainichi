using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mainichi.Web.Store.App_Start;
using Mainichi.Web.Store.Controllers;
using Raven.Client;

namespace Mainichi.Web.Store.Filters
{
    public class HandleRavenSessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;
            if (controller == null)
                return;

            // Can be set explicitly in unit testing
            if (controller.RavenSession != null)
                return;

            controller.RavenSession = DataDocumentStore.Initialize().OpenSession();
            controller.RavenSession.Advanced.UseOptimisticConcurrency = true;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;
            if (controller == null)
                return;

            using (var session = controller.RavenSession)
            {
                if (session == null)
                    return;

                if (filterContext.Exception != null)
                {
                    session.SaveChanges();
                }
            }
        }
    }
}