using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels
{
    public class ProductsListsViewModel
    {
        public IEnumerable<Thing> AllThings { get; set; }
        public IEnumerable<Thing> ListThings { get; set; } 
    }
}