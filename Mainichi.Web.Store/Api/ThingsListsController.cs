using System.Collections.Generic;
using System.Linq;
using Mainichi.Web.Store.ViewModels;

namespace Mainichi.Web.Store.Api
{
    public class ThingListsController : AbstractApiController
    {
        public IEnumerable<ThingListViewModel> Get()
        {
            var thingLists = RavenSession.Include<ThingListModel>(x => x.ThingIds)
                .Load("thinglist/new", "thinglist/featured", "thinglist/discounted");

            var thingListsViewModel = thingLists.Where(d => d.Active).Select(listsViewModel => new ThingListViewModel
            {
                Things = listsViewModel.ThingIds.Select(id => RavenSession.Load<Thing>(id)),
                Descriptor = listsViewModel.Descriptor,
                Active = listsViewModel.Active
            });

            return thingListsViewModel;
        }

        public ThingListViewModel Get(string id)
        {
            var thingList = RavenSession.Include<ThingListModel>(x => x.ThingIds)
                .Load("thinglist/" + id);

            var things = thingList.ThingIds.Select(d => RavenSession.Load<Thing>(d)).ToList();

            var missingProducts = 6 - things.Count();
            if (missingProducts > 0)
            {
                for (int i = 0; i < missingProducts; i++)
                {
                    things.Add(new Thing());
                }
            }

            return new ThingListViewModel
            {
                Descriptor = thingList.Descriptor,
                Active = thingList.Active,
                Things = things
            };
        }
    }
}