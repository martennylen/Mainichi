using System.Web;
using System.Web.Mvc;
using Mainichi.Web.Store.Filters;

namespace Mainichi.Web.Store
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleRavenSessionFilter());
        }
    }
}