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

            //LoginController
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

            //TelaInicialController
            routes.MapRoute(
            "Tela Inicial",
            url: "{controller}/{action}",
            defaults: new { controller = "TelaInicial", action = "TelaInicial", id = UrlParameter.Optional }
            );

            //ConsultarProvaAlController
            routes.MapRoute(
            "Consultar Prova Aluno",
            url: "{controller}/{action}",
            defaults: new { controller = "ConsultarProvaAl", action = "ConsultarProvaAl" }
            );

            routes.MapRoute(
            "Realizar Prova",
            url: "{controller}/{action}",
            defaults: new { controller = "ConsultarProvaAl", action = "RealizarProva" }
            );

            routes.MapRoute(
            "Visualizar Prova Aluno",
            url: "{controller}/{action}",
            defaults: new { controller = "ConsultarProvaAl", action = "VisualizarProva" }
            );

            //ConsultarProvaPrController
            routes.MapRoute(
            "Consultar Prova Professor",
            url: "{controller}/{action}",
            defaults: new { controller = "ConsultarProvaPr", action = "ConsultarProvaPr" }
            );

            routes.MapRoute(
            "Corrigir Prova do Aluno Especifico",
            url: "{controller}/{action}",
            defaults: new { controller = "ConsultarProvaPr", action = "CorrigirProvaAlunoEspecifico" }
            );

            routes.MapRoute(
            "Corrigir Prova",
            url: "{controller}/{action}",
            defaults: new { controller = "ConsultarProvaPr", action = "CorrigirProva" }
            );

            routes.MapRoute(
            "Opções de Correção",
            url: "{controller}/{action}",
            defaults: new { controller = "ConsultarProvaPr", action = "OpcoesCorrecao" }
            );

            routes.MapRoute(
            "Visualizar Prova Professor",
            url: "{controller}/{action}",
            defaults: new { controller = "ConsultarProvaPr", action = "VisualizarProva" }
            );

            routes.MapRoute(
            "Visualizar Todas as Provas",
            url: "{controller}/{action}",
            defaults: new { controller = "ConsultarProvaPr", action = "VisualizarTodasAsProvas" }
            );

            //GerarProvaController
            routes.MapRoute(
            "Gerar Prova",
            url: "{controller}/{action}",
            defaults: new { controller = "GerarProva", action = "GerarProva" }
            );

            routes.MapRoute(
            "Verificar Questões da Prova",
            url: "{controller}/{action}",
            defaults: new { controller = "GerarProva", action = "AdicionarQuestoesNaProva" }
            );

            //CadastrarQuestoesController
            routes.MapRoute(
            "Cadastrar Questões",
            url: "{controller}/{action}",
            defaults: new { controller = "CadastrarQuestoes", action = "CadastrarQuestoes" }
            );

            //EditarQuestoesController
            routes.MapRoute(
            "Editar Questão Dissertativa",
            url: "{controller}/{action}",
            defaults: new { controller = "EditarQuestoes", action = "Dissertativa" }
            );

            routes.MapRoute(
            "Editar Questão Multipla-Escolha",
            url: "{controller}/{action}",
            defaults: new { controller = "EditarQuestoes", action = "MultiplaEscolha" }
            );

            routes.MapRoute(
            "Editar Questão Simples-Escolha",
            url: "{controller}/{action}",
            defaults: new { controller = "EditarQuestoes", action = "SimplesEscolha" }
            );

            routes.MapRoute(
            "Editar Questão Verdadeiro ou Falso",
            url: "{controller}/{action}",
            defaults: new { controller = "EditarQuestoes", action = "VerdadeiroFalso" }
            );

            //ConsultarQuestoesController
            routes.MapRoute(
            "Consultar Questões",
            url: "{controller}/{action}",
            defaults: new { controller = "ConsultarQuestoes", action = "ConsultarQuestoes" }
            );

            //ConfiguracoesController
            routes.MapRoute(
            "Configurações",
            url: "{controller}/{action}",
            defaults: new { controller = "Configuracoes", action = "Configuracoes" }
            );

        }
    }
}

