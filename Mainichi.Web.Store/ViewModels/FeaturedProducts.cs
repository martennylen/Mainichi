using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels
{
    public class ThingListsViewModel
    {
        public string Descriptor { get; set; }
        public IEnumerable<Thing> Things { get; set; }
    }

    public class ThingLists
    {
        public FeaturedThings FeaturedThings { get; set; }
        public FeaturedThings NewArrivalThings { get; set; }
        public FeaturedThings DiscountedThings { get; set; }
    }

    public class FeaturedThings
    {
        public string Id { get; set; }
        public string Descriptor { get; set; }
        public IEnumerable<string> ThingIds { get; set; }
    }
}