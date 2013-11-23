using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mainichi.Web.Store.App_Start;
using Mainichi.Web.Store.Filters;
using Mainichi.Web.Store.ViewModels;

namespace Mainichi.Web.Store.Controllers
{
    public class HomeController : BaseController
    {
        //[HandleRavenSessionFilter]
        public ActionResult Index()
        {
            
            //var apa = new CategoryList
            //{
            //    Categories = new List<Category>
            //    {
            //        new Category
            //        {
            //            Name = "Modellbyggsatser", SortIndex = 0, SubCategories = new List<Category>
            //            {
            //                new Category
            //                {
            //                    Name = "Gundam", SortIndex = 0, SubCategories = new List<Category>()
            //                },
            //                new Category
            //                {
            //                    Name = "Keroro", SortIndex = 1, SubCategories = new List<Category>()
            //                },
            //                new Category
            //                {
            //                    Name = "Zoids", SortIndex = 2, SubCategories = new List<Category>()
            //                }
            //            }
            //        },
            //        new Category {Name = "Bentolådor", SortIndex = 1, SubCategories = new List<Category>()},
            //        new Category {Name = "Snacks", SortIndex = 2, SubCategories = new List<Category>()}
            //    }
            //};

            //RavenSession.Store(apa);
            var categories = RavenSession.Load<CategoryList>("config/categories");

            return View(categories);
            //return View(new CategoryList{ Categories = new List<Category>()});
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
