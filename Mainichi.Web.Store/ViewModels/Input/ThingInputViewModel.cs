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

        [DisplayName("Produktbild:")]
        public HttpPostedFileBase ImageFile { get; set; }

        [DisplayName("Välj bild:")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Fyll i produktbeskrivning!")]
        [DisplayName("Produktbeskrivning:")]
        public string Description { get; set; }
    }
}