using System.Collections.Generic;
using System.Linq;
using Mainichi.Web.Store.ViewModels;

namespace Mainichi.Web.Store.Api
{
    public class ThingsListsController : AbstractApiController
    {
        //
        // GET: /ThingsLists/

        public IEnumerable<Thing> Get(string id)
        {
            var model = RavenSession.Include<FeaturedThings>(x => x.ThingIds)
                .Load("thinglist/" + id).ThingIds.Select(d => RavenSession.Load<Thing>(d)).ToList();

            var missingProducts = 6 - model.Count();
            if (missingProducts > 0)
            {
                for (int i = 0; i < missingProducts; i++)
                {
                    model.Add(new Thing());
                }
            }
            return model;
        }
    }
}