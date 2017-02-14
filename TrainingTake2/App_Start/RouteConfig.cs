﻿using System.Web.Http;
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

            //routes.MapHttpRoute(
            //    "API Default",
            //    "api/{controller}/{id}",
            //    new {id = RouteParameter.Optional}
            //);
        }
    }
}