using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Mainichi.Web.Store.Api.Filter;
using Mainichi.Web.Store.App_Start;
using Mainichi.Web.Store.Filters;

namespace Mainichi.Web.Store
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new RavenSessionManagementAttribute());
        }
    }
}
