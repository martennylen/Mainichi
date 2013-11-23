using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mainichi.Web.Store.App_Start;
using Raven.Client;

namespace Mainichi.Web.Store.Filters
{
    public class HandleRavenSessionFilter : ActionFilterAttribute
    {
        public IDocumentSession RavenSession { get; protected set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = DataDocumentStore.Initialize().OpenSession();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            using (RavenSession)
            {
                if (filterContext.Exception != null)
                    return;

                if (RavenSession != null)
                    RavenSession.SaveChanges();
            }
        }
    }
}