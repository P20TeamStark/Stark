using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace rentMyJunk
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "ItemsByCat",
               url: "Items/ByCategory/{cat}",
               defaults: new { controller = "Items", action = "ByCategory", cat = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "RequestByItem",
               url: "Request/Index/{itemId}",
               defaults: new { controller = "Request", action = "Index", itemId = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "CreateRequest",
               url: "Request/Create/{request}",
               defaults: new { controller = "Request", action = "Create", request = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "ItemsByCat2",
               url: "Items/{action}/{category}/{userId}",
               defaults: new { controller = "Items", action = "Index", category = UrlParameter.Optional, userId = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Items",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Items", action = "Index", id = UrlParameter.Optional }
           );

            
        }
    }
}
