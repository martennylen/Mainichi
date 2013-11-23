using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mainichi.Web.Store.ViewModels;

namespace Mainichi.Web.Store.Api
{
    public class ThingsController : AbstractApiController
    {
        // GET api/things
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/things/5
        public Thing Get(int id)
        {
            return RavenSession.Load<Thing>(id);
        }
    }
}