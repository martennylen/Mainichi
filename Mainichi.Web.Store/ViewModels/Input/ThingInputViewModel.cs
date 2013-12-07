using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels.Input
{
    public class ThingInputViewModel
    {
        public string Id { get; set; }
        public bool IsEditing { get; set; }

        [Required(ErrorMessage = "Fyll i produktnamn!")]
        [DisplayName("Namn:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fyll i pris!")]
        [DisplayName("Pris:")]
        public decimal? Price { get; set; }

        //[DisplayName("Produktbild:")]
        //public HttpPostedFileBase ImageFile { get; set; }

        public string Image { get; set; }
        public List<ImageItemViewModel> Slides { get; set; }

        [Required(ErrorMessage = "Fyll i produktbeskrivning!")]
        [DisplayName("Produktbeskrivning:")]
        public string Description { get; set; }
    }

    public class ImageItemViewModel
    {
        public HttpPostedFileBase File { get; set; }
        public string FileName { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }

        public bool IsNew { get; set; }
        public bool DeleteMe { get; set; }
    }
}