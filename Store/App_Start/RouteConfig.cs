using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Store
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Products",
               url: "Products/{productURL}",
               defaults: new { controller = "Home", action = "ShowProduct", productURL = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Groups",
               url: "Groups/{groupId}/{groupName}",
               defaults: new { controller = "Home", action = "ShowGroups", groupName = UrlParameter.Optional, groupId = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}