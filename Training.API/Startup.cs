using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;
using Training.API.App_Start;
using Training.Identity;
using Training.API;

// ReSharper disable once RedundantNameQualifier

[assembly: OwinStartup(typeof(Startup))]

namespace Training.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            UnityConfig.RegisterTypes();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebApiConfig.Register(config);
            ConfigureOAuth(app);
            Database.SetInitializer(new CreateDatabaseIfNotExists<IdentityContext>());
            //register unity

            app.UseWebApi(config);
        }
    }
}