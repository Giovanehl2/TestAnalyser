using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestAnalyser
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                "Login",
                "Login",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            "GerarProva",
            "GerarProva",
            defaults: new { controller = "GerarProva", action = "GerarProva" }
            );

           routes.MapRoute(
            "ConfirmarLogin",
            url: "{controller}/{action}",
            defaults: new { controller = "Login", action = "ConfirmarLogin" }
            );

            //routes.MapRoute(
            //    name: "Login",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            //);
        }
    }
}

