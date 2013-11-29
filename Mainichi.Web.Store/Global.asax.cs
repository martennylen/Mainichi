using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Mainichi.Web.Store.App_Start;
using Mainichi.Web.Store.Models.Indexes;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Mainichi.Web.Store
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            DataDocumentStore.Initialize();

            //var catalog = new CompositionContainer(new AssemblyCatalog(typeof(Things_FrontPageFeatured).Assembly));
            //IndexCreation.CreateIndexes(catalog, DataDocumentStore.Instance.DatabaseCommands.ForDatabase("Mainichi"), DataDocumentStore.Instance.Conventions);
        }
    }
}