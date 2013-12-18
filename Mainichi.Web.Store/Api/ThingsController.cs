using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mainichi.Web.Store.ViewModels;
using WebGrease.Css.Extensions;

namespace Mainichi.Web.Store.Api
{
    public class ThingsController : AbstractApiController
    {
        // GET api/things/5
        public Thing Get(int id)
        {
            if (id == 0)
            {
                return new Thing();
            }
            return RavenSession.Load<Thing>("things/" + id);
        }
    }
}