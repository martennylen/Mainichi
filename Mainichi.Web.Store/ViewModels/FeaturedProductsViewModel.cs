using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels
{
    public class FeaturedProductsViewModel
    {
        public IEnumerable<Thing> AllThings { get; set; }
        public IEnumerable<Thing> FeaturedThings { get; set; } 
    }
}