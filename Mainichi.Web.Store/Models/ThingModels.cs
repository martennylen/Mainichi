using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.Models
{
    public class ThingModels
    {
        public class Thing
        {
            public Thing(int id)
            {
                Id = id;
            }

            public int Id { get; set; }
        }
    }
}