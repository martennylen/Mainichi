using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int SortIndex { get; set; }
        public List<Category> SubCategories { get; set; } 
    }
}