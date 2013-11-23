using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mainichi.Web.Store.ViewModels;
using Raven.Client;
using Raven.Client.Document;

namespace Mainichi.Web.Store.App_Start
{
    public class DataDocumentStore
    {
        private static IDocumentStore _instance;

        public static IDocumentStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("IDocumentStore has not been initialized.");
                }
                return _instance;
            }
        }

        public static IDocumentStore Initialize()
        {
            _instance = new DocumentStore { ConnectionStringName = "RavenDB" };
            _instance.Conventions.RegisterIdConvention<CategoryList>((dbname, commands, categoryList) => "config/categories");
            _instance.Conventions.RegisterIdConvention<Category>((dbname, commands, category) => "category/" + category.Name);
            //_instance.Conventions.DefaultQueryingConsistency = ConsistencyOptions.QueryYourWrites;
            _instance.Initialize();
            return _instance;
        }
    }
}