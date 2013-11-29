using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mainichi.Web.Store.ViewModels;
using Microsoft.Ajax.Utilities;
using Raven.Client.Indexes;

namespace Mainichi.Web.Store.Models.Indexes
{
    public abstract class Things_FrontPageFeatured : AbstractIndexCreationTask<FeaturedProducts>
    {
        //public Things_FrontPageFeatured()
        //{
        //    Map = featureds => from featured in featureds
        //        select featured;

        //    Reduce = results => from result in results
        //        select new Thing
        //        {

        //        };
        //}
    }
}