using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels
{
    public class Categories //REFAKTORISERA, BRYT UT KATEGORIER TILL EGNA DOKUMENT I RAVEN
    {
        public IEnumerable<Category> AllCategories { get; set; } 
    }
}