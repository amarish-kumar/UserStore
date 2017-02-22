using System.Web.Http;
using Microsoft.Practices.Unity.WebApi;
using Newtonsoft.Json.Serialization;
using Training.API.App_Start;

namespace Training.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.RegisterTypes());

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver 
                = new CamelCasePropertyNamesContractResolver();
        }
    }
}