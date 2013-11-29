using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels
{
    public class Thing
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
	    public decimal Price { get; set; }
		public string Image { get; set; }
        public string Article { get; set; }
    }
}