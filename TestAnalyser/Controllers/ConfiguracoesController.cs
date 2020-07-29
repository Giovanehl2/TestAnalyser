using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAnalyser.DAL;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    public class ConfiguracoesController : Controller
    {
        // GET: Configuracoes
        public ActionResult Configuracoes()
        {
            //var configs = AdminDAO.BuscarConfiguracoes();
            return View();
        }

        public ActionResult SalvarConfiguracoes(Configuracao configuracoes)
        {

            AdminDAO.SalvarConfiguracoes(configuracoes);

            return RedirectToAction("Configuracoes", "Configuracoes");
        }
    }
}