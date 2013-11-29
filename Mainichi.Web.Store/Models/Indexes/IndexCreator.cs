using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mainichi.Web.Store.ViewModels;
using Raven.Client.Indexes;

namespace Mainichi.Web.Store.Models.Indexes
{
    public abstract class Things_Selected : AbstractIndexCreationTask<Thing>
    {
    }
}