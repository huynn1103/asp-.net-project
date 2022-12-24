using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SV19T1081011.UserInterface
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Account",
                url: "Account/{action}",
                defaults: new { controller = "Account", action = "Login", }
            );

            routes.MapRoute(
                name: "Post",
                url: "{categoryUrlName}/{urlTitle}",
                defaults: new { controller = "Home", action = "Post" }
            );

            routes.MapRoute(
                name: "Category",
                url: "{categoryUrlName}",
                defaults: new { controller = "Home", action = "Category" }
            );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
