using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.DAL;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    [Authorize]
    public class ConfiguracoesController : Controller
    {
        // GET: Configuracoes
        public ActionResult Configuracoes()
        {
            var configs = AdminDAO.MostrarConfigsTela();
            return View(configs);
        }

        public ActionResult SalvarConfiguracoes(Configuracao configuracoes)
        {

            AdminDAO.SalvarConfiguracoes(configuracoes);

            return RedirectToAction("Configuracoes", "Configuracoes");
        }

        public string ImportarDadosApi()
        {
            
           if (ApiIntegracaoController.Importar(AdminDAO.BuscarConfiguracoes()))
            return "Carga realizada com sucesso";

            return "Erro inesperado durante a carga, favor entrar em contato com o suporte!";
        }
    }
}