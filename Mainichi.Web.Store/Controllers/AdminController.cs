using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using Mainichi.Web.Store.Extensions;
using Mainichi.Web.Store.ViewModels;
using Mainichi.Web.Store.ViewModels.Input;

namespace Mainichi.Web.Store.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/
        private const string nameAndLocation = "~/Content/Snapshots/Products/";

        public ActionResult Index()
        {
            return View();
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
                if (t.ImageFile.FileName != existingThing.Image)
                {
                    t.ImageFile.SaveAs(Server.MapPath(string.Concat(nameAndLocation, t.ImageFile.FileName)));
                    var existingFilePath = string.Concat(nameAndLocation, existingThing.Image);
                    if (System.IO.File.Exists(Server.MapPath(existingFilePath)))
                    {
                        System.IO.File.Delete(Server.MapPath(existingFilePath));
                    }
                    existingThing.Image = t.ImageFile.FileName;
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
