﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels
{
    public class Thing
    {
        public Thing()
        {
            Name = "Välj en produkt";
            Image = "placeholder.png";
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Article { get; set; }

        public List<ImageItem> Slides { get; set; } 
        public List<Attribute> Attributes { get; set; } 

        public decimal? Price { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastChange { get; set; }
    }

    public class ImageItem
    {
        public string FileName { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
    }

    public class Attribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}