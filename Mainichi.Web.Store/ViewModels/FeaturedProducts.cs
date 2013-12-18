using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels
{
    public class ThingListViewModel
    {
        public string Descriptor { get; set; }
        public bool Active { get; set; }
        public IEnumerable<Thing> Things { get; set; }
    }

    public class ThingListModel
    {
        public string Id { get; set; }
        public bool Active { get; set; }
        public string Descriptor { get; set; }
        public IEnumerable<string> ThingIds { get; set; }
    }
}