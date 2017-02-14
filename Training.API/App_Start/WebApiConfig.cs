using System.Web.Http;
using Microsoft.Practices.Unity.WebApi;
using Training.API.App_Start;

namespace Training.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );

            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.RegisterTypes());
        }
    }
}