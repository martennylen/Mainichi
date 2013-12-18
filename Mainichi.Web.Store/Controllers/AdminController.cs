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
            var allThings = RavenSession.Query<Thing>("Things/AllForEdit");

            var model = RavenSession.Include<ThingListModel>(x => x.ThingIds)
                .Load("thinglist/new");

            var things = model.ThingIds.Select(d => RavenSession.Load<Thing>(d)).ToList();

            var missingProducts = _productsSlots - things.Count();
            if (missingProducts > 0)
            {
                for (int i = 0; i < missingProducts; i++)
                {
                    things.Add(new Thing());
                }
            }

            var m = new ProductsListsViewModel
            {
                AllThings = allThings,
                ThingList = new ThingListViewModel
                {
                    Descriptor = model.Descriptor,
                    Active = model.Active,
                    Things = things
                }
            };

            return View(m);
        }

        [HttpPost]
        public ActionResult UpdateSelectedProducts(ThingListModel featuredProducts)
        {
            string status = "Produktlistan uppdaterad";
            try
            {
                var model = RavenSession.Load<ThingListModel>("thinglist/" + featuredProducts.Id);
                model.Descriptor = featuredProducts.Descriptor;
                model.Active = featuredProducts.Active;
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
                model.Attributes = existingThing.Attributes;

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
                existingThing.Attributes = t.Attributes;
                
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
            result.Attributes = t.Attributes;

            result.Created = DateTime.UtcNow;
            result.LastChange = result.Created;

            RavenSession.Store(result);
        }
    }
}
