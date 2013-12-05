using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mainichi.Web.Store.ViewModels;
using WebGrease.Css.Extensions;

namespace Mainichi.Web.Store.Api
{
    public class ThingsController : AbstractApiController
    {
        // GET api/things
        //public IEnumerable<Thing> Get()
        public IEnumerable<ThingListsViewModel> Get()
        {
            //var thingsLists =
            //    RavenSession.Query<ThingLists>()
            //        .Include(x => x.FeaturedThings.ThingIds)
            //        .Include(d => d.NewArrivalThings.ThingIds).Select(d => d).Single();

            var thingLists = RavenSession.Include<FeaturedThings>(x => x.ThingIds)
                .Load("thinglist/new", "thinglist/featured", "thinglist/discounted");

            var thingListsViewModel = thingLists.Select(listsViewModel => new ThingListsViewModel
            {
                Things = listsViewModel.ThingIds.Select(id => RavenSession.Load<Thing>(id)), 
                Descriptor = listsViewModel.Descriptor
            });

            return thingListsViewModel;
        }

        // GET api/things/5
        public Thing Get(int id)
        {
            if (id == 0)
            {
                return new Thing();
            }
            return RavenSession.Load<Thing>("things/" + id);
        }
    }
}