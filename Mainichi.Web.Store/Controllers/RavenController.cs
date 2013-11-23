using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mainichi.Web.Store.Controllers
{
    public class RavenController : ApiController
    {
        // GET api/raven
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/raven/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/raven
        public void Post([FromBody]string value)
        {
        }

        // PUT api/raven/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/raven/5
        public void Delete(int id)
        {
        }
    }
}
