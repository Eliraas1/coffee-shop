﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoffeeShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Reg",
               url: "Reg",
               defaults: new { controller = "LogReg", action = "Reg", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Log",
               url: "Log",
               defaults: new { controller = "LogReg", action = "Log", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "LogOut",
               url: "LogOut",
               defaults: new { controller = "LogReg", action = "Logout", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "SearchUser",
               url: "SearchUser",
               defaults: new { controller = "Admin", action = "SearchUser", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "edit",
              url: "EditCoffee",
              defaults: new { controller = "Admin", action = "EditCoffee", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}