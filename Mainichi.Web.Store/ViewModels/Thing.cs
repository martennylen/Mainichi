using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels
{
    public class Thing
    {
        [Required(ErrorMessage = "Fyll i produktnamn!")]
        [DisplayName("Namn:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fyll i pris!")]
        [DisplayName("Pris:")]
        public decimal? Price { get; set; }

        [DisplayName("Produktbild:")]
        public HttpPostedFileBase Shimage { get; set; }

        [Required(ErrorMessage = "Fyll i produktbeskrivning!")]
        [DisplayName("Produktbeskrivning:")]
        public string Description { get; set; }

        public string Image { get; set; }
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Article { get; set; }
    }
}