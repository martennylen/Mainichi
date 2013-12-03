using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mainichi.Web.Store.ViewModels;

namespace Mainichi.Web.Store.Api
{
    public class ThingsController : AbstractApiController
    {
        // GET api/things
        //public IEnumerable<Thing> Get()
        public IEnumerable<Thing> Get()
        {
            var featuredProducts = RavenSession.Include<FeaturedProducts>(x => x.FeaturedThingIds.Select(id => id)).Load("config/featuredproducts");
            return featuredProducts.FeaturedThingIds.Select(id => RavenSession.Load<Thing>(id));
            //return RavenSession.Load<FeaturedProducts>("config/featuredproducts");
            //return RavenSession.Query<Thing>().ToList();
        }

        // GET api/things/5
        public Thing Get(int id)
        {
            if (id == 0)
            {
                return new Thing
                {
                    Id = string.Empty,
                    Name = "Tom slot",
                    Image = "placeholder.png"
                };
            }
            return RavenSession.Load<Thing>("things/" + id);
        }
    }
}