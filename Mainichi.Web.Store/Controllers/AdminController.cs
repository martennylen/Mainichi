using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Mainichi.Web.Store.Api;
using Mainichi.Web.Store.Extensions;
using Mainichi.Web.Store.ViewModels;
using Mainichi.Web.Store.ViewModels.Input;
using Raven.Client.Linq;
using WebGrease.Css.Extensions;

namespace Mainichi.Web.Store.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/
        private const string _nameAndLocation = "~/Content/Snapshots/Products/";
        private const int _productsSlots = 6;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditProductLists()
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
                .Load("thinglist/new").ThingIds.Select(d => RavenSession.Load<Thing>(d)).ToList();
            //var allThings = new List<Thing>
            //{
            //    new Thing()
            //};

            var allThings = RavenSession.Query<Thing>("Things/AllForEdit");


            var missingProducts = _productsSlots - model.Count();
            if (missingProducts > 0)
            {
                for (int i = 0; i < missingProducts; i++)
                {
                    model.Add(new Thing());
                }
            }

            var m = new ProductsListsViewModel
            {
                AllThings = allThings,
                ListThings = model
            };

            return View(m);
        }

        [HttpPost]
        public ActionResult UpdateSelectedProducts(FeaturedThings featuredProducts)
        {
            string status = "Produktlistan uppdaterad";
            try
            {
                var model = RavenSession.Load<FeaturedThings>("thinglist/" + featuredProducts.Id);
                model.ThingIds = featuredProducts.ThingIds.Where(d => !string.IsNullOrWhiteSpace(d) && d != "things/0");
            }
            catch (Exception e)
            {
                status = e.Message;
            }

            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditThing(string id)
        {
            var model = new ThingInputViewModel();

            if (!string.IsNullOrWhiteSpace(id))
            {
                var existingThing = RavenSession.Load<Thing>("things/" + id);
                model.Id = existingThing.Id;
                model.Price = existingThing.Price;
                model.Name = existingThing.Name;
                model.Description = existingThing.Description;
                model.Slides = new List<ImageItemViewModel>();

                foreach (var item in existingThing.Slides.OrderBy(d => d.Index).ToList())
                {
                    model.Slides.Add(new ImageItemViewModel()
                    {
                        FileName = item.FileName,
                        Text = item.Text,
                        Index = item.Index
                    });
                }

                model.IsEditing = true;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddOrUpdateThing(ThingInputViewModel t)
        {
            if (t.Id == null)
            {
                SaveThing(t);
            }
            else
            {
                UpdateThing(t);
            }

            return RedirectToAction("EditThing");
        }

        private void UpdateThing(ThingInputViewModel t)
        {
            var existingThing = RavenSession.Load<Thing>("things/" + t.Id);

            if (existingThing != null)
            {
                if (t.Slides.Any())
                {
                    var itemsToDelete = new List<ImageItem>();

                    for(int i=0;i<=t.Slides.Count-1;i++)
                    {
                        var current = t.Slides[i];
                        if (!current.IsNew)
                        {
                            if (current.DeleteMe)
                            {
                                itemsToDelete.Add(existingThing.Slides[i]);
                            }
                            else
                            {
                                var existingFilePath = string.Concat(_nameAndLocation, existingThing.Image);
                                if (current.File != null && (current.File.FileName != existingThing.Slides[i].FileName))
                                    //No new image file on current slide
                                {
                                    if (System.IO.File.Exists(Server.MapPath(existingFilePath)))
                                    {
                                        System.IO.File.Delete(Server.MapPath(existingFilePath));
                                    }
                                    current.File.SaveAs(Server.MapPath(string.Concat(_nameAndLocation, current.FileName)));
                                }

                                existingThing.Slides[i].FileName = current.FileName;
                                existingThing.Slides[i].Index = current.Index;
                                existingThing.Slides[i].Text = current.Text;
                            }
                        }
                        else
                        {
                            current.File.SaveAs(Server.MapPath(string.Concat(_nameAndLocation, current.FileName)));
                            existingThing.Slides.Add(new ImageItem
                            {
                                FileName = current.FileName,
                                Index = current.Index,
                                Text = current.Text
                            });
                        }
                    }

                    existingThing.Slides.RemoveAll(itemsToDelete.Contains);
                    existingThing.Image = t.Slides.First().FileName;
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
            var result = new Thing
            {
                Slides = new List<ImageItem>()
            };

            if (t.Slides.Any())
            {
                foreach (var image in t.Slides)
                {
                    string nameAndLocation = "~/Content/Snapshots/Products/" + image.FileName;
                    image.File.SaveAs(Server.MapPath(nameAndLocation));
                    result.Slides.Add(new ImageItem
                    {
                        FileName = image.FileName,
                        Text = image.Text,
                        Index = image.Index
                    });
                }

                result.Image = result.Slides.First().FileName;
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
