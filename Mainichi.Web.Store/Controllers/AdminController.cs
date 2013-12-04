using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Mainichi.Web.Store.Api;
using Mainichi.Web.Store.Extensions;
using Mainichi.Web.Store.ViewModels;
using Mainichi.Web.Store.ViewModels.Input;
using WebGrease.Css.Extensions;

namespace Mainichi.Web.Store.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/
        private const string _nameAndLocation = "~/Content/Snapshots/Products/";
        private const int _featuredProductsSlots = 6;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductLists()
        {
            //var model = RavenSession.Include<FeaturedThings>(x => x.FeaturedThingIds.Select(id => id)).Load("config/featuredproducts")
            //.FeaturedThingIds.Select(id => RavenSession.Load<Thing>(id)).ToList();

            //var model = RavenSession.Load<ThingLists>("config/thingslists").FeaturedThings;
            //var model = RavenSession.Include<FeaturedThings>(x => x.ThingIds.Select(id => id))
            //    .Load("config/thingslists")
            //    .ThingIds.Select(id => RavenSession.Load<Thing>(id))
            //    .ToList();

            //var modell = RavenSession.Load<ThingLists>("config/thingslists").FeaturedThings;
            //var model = modell.

            var model = RavenSession.Include<FeaturedThings>(x => x.ThingIds)
                .Load("thinglist/featured").ThingIds.Select(d => RavenSession.Load<Thing>(d)).ToList();
            var allThings = RavenSession.Query<Thing>("Things/AllForEdit");

            var missingProducts = _featuredProductsSlots - model.Count();
            if (missingProducts > 0)
            {
                for (int i = 0; i < missingProducts; i++)
                {
                    model.Add(new Thing
                    {
                        Id = string.Empty,
                        Name = "Tom slot",
                        Image = "placeholder.png"
                    });
                }
            }

            var m = new FeaturedProductsViewModel
            {
                AllThings = allThings,
                FeaturedThings = model
            };

            return View(m);

            return null;
        }

        [HttpPost]
        public ActionResult UpdateSelectedProducts(FeaturedThings featuredProducts)
        {
            string status = "Produktlistan uppdaterad";
            try
            {
                var model = RavenSession.Load<FeaturedThings>("thinglist/featured");
                model.ThingIds = featuredProducts.ThingIds.Where(d => !string.IsNullOrWhiteSpace(d));
            }
            catch (Exception e)
            {
                status = e.Message;
            }

            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Thing(string id)
        {
            var model = new ThingInputViewModel();

            if (!string.IsNullOrWhiteSpace(id))
            {
                var existingThing = RavenSession.Load<Thing>("things/" + id);
                model.Id = existingThing.Id;
                model.Price = existingThing.Price;
                model.Name = existingThing.Name;
                model.Description = existingThing.Description;
                model.Image = existingThing.Image;

                model.IsEditing = true;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditThing(ThingInputViewModel t)
        {
            if (t.Id == null)
            {
                SaveThing(t);
            }
            else
            {
                UpdateThing(t);
            }

            return RedirectToAction("Thing");
        }

        private void UpdateThing(ThingInputViewModel t)
        {
            var existingThing = RavenSession.Load<Thing>("things/" + t.Id);

            if (existingThing != null)
            {
                if (t.ImageFile != null) //Image was updated
                {
                    if (t.ImageFile.FileName != existingThing.Image)
                    {
                        t.ImageFile.SaveAs(Server.MapPath(string.Concat(_nameAndLocation, t.ImageFile.FileName)));
                        var existingFilePath = string.Concat(_nameAndLocation, existingThing.Image);
                        if (System.IO.File.Exists(Server.MapPath(existingFilePath)))
                        {
                            System.IO.File.Delete(Server.MapPath(existingFilePath));
                        }
                        existingThing.Image = t.ImageFile.FileName;
                    }
                }
                existingThing.Name = t.Name;
                existingThing.Slug = t.Name.ToSlug();
                existingThing.Description = t.Description;
                existingThing.Price = t.Price;
                //existingThing.Id = t.Id;
                
                existingThing.LastChange = DateTime.UtcNow;
            }
        }

        private void SaveThing(ThingInputViewModel t)
        {
            var result = new Thing();
            if (t.ImageFile != null)
            {
                string nameAndLocation = "~/Content/Snapshots/Products/" + t.ImageFile.FileName;
                //Path.GetTempPath();
                t.ImageFile.SaveAs(Server.MapPath(nameAndLocation));
                result.Image = t.ImageFile.FileName;
            }

            result.Name = t.Name;
            result.Slug = t.Name.ToSlug();
            result.Description = t.Description;
            result.Price = t.Price;

            result.Created = DateTime.UtcNow;
            result.LastChange = result.Created;

            RavenSession.Store(result);
        }
    }
}
