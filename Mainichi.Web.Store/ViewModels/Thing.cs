using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mainichi.Web.Store.ViewModels
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