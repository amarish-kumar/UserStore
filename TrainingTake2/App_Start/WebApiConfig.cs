using System.Web.Http;
using Microsoft.Practices.Unity.WebApi;
using TrainingTake2.App_Start;

namespace TrainingTake2
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