using System.Web.Mvc;
using System.Web.Routing;

namespace Training.API
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );

            routes.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new {controller = "Help", action = "Index", apiId = UrlParameter.Optional});
        }
    }
}