using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Raven.Client.Document;
using Raven.Imports.Newtonsoft.Json;
using Raven.Imports.Newtonsoft.Json.Serialization;

namespace Mainichi.Web.Store.Extensions
{
    public static class ThingExtensions
    {
        public static string ToSlug(this string thingName)
        {
            var slug = thingName.Trim();
            slug = string.Concat(slug.First().ToString().ToUpper(), slug.Substring(1, slug.Length - 1));
            slug = String.Join("", slug.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
            slug = slug.Replace(" ", "-");

            return slug;
        }

        public static IHtmlString RenderAsJson(this HtmlHelper helper, object value)
        {
            return helper.Raw(JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new DefaultRavenContractResolver(true)
            }));
        }
    }
}