using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Raven.Client;

namespace Mainichi.Web.Store.Api
{
    public abstract class AbstractApiController : ApiController
    {
        public IDocumentSession RavenSession { get; set; }
    }
}