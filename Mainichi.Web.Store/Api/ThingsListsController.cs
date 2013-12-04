using System.Collections.Generic;
using System.Linq;
using Mainichi.Web.Store.ViewModels;

namespace Mainichi.Web.Store.Api
{
    public class ThingsListsController : AbstractApiController
    {
        //
        // GET: /ThingsLists/

        public IEnumerable<IEnumerable<Thing>> Get()
        {
            //var featuredProducts = RavenSession.Include<FeaturedProducts>(x => x.FeaturedThingIds.Select(id => id)).Load("config/featuredproducts");
            //return featuredProducts.FeaturedThingIds.Select(id => RavenSession.Load<Thing>(id));
            return null;
        }
    }
}
