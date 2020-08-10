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
            url: "{controller}/{action}",
            defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            "Confirmar Login",
            url: "{controller}/{action}",
            defaults: new { controller = "Login", action = "ConfirmarLogin" }
            );

            routes.MapRoute(
            "Gerar Prova",
            url: "{controller}/{action}",
            defaults: new { controller = "GerarProva", action = "GerarProva" }
            );
          

            routes.MapRoute(
            "Cadastrar Questões",
            url: "{controller}/{action}",
            defaults: new { controller = "CadastrarQuestoes", action = "CadastrarQuestoes" }
            );

            routes.MapRoute(
            "Consultar Questões",
            url: "{controller}/{action}",
            defaults: new { controller = "ConsultarQuestoes", action = "ConsultarQuestoes" }
            );
            
            routes.MapRoute(
            "Configurações",
            url: "{controller}/{action}",
            defaults: new { controller = "Configuracoes", action = "Configuracoes" }
            );



            //routes.MapRoute(
            //    name: "Login",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            //);
        }
    }
}

