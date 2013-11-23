using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels
{
    public class CategoryMenuModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public string Identifier { get; set; }
    }
}