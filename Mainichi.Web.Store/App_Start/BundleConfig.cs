﻿using System.Web;
using System.Web.Optimization;
using Mainichi.Web.Store.Infrastructure.Bundling;
using dotless.Core;

namespace Mainichi.Web.Store
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                    "~/Scripts/Lib/jquery-{version}.js",
                    "~/Scripts/Lib/knockout-{version}.js",
                    "~/Scripts/Lib/underscore-{version}.js",
                    "~/Scripts/Lib/sammy-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/mainichi").Include(
                        "~/Scripts/Index/app.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Lib/modernizr-{version}.js"));

            bundles.Add(new LessBundle("~/Content/css").Include("~/Content/Styles/Mainichi.less", "~/Content/Styles/Grid.less"));
        }
    }
}