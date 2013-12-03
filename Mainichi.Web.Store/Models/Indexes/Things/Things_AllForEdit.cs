using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mainichi.Web.Store.ViewModels;
using Microsoft.Ajax.Utilities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Mainichi.Web.Store.Models.Indexes
{
    public class Things_AllForEdit : AbstractIndexCreationTask<Thing>
    {
        public Things_AllForEdit()
        {
            Map = things => from thing in things
                select new { thing.Id, thing.Name };

            Index(x => x.Id, FieldIndexing.Analyzed);
            Index(x => x.Name, FieldIndexing.Analyzed);
        }
    }
}