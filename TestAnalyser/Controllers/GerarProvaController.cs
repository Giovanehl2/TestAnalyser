using Microsoft.Ajax.Utilities;
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
    public class GerarProvaController : Controller
    {
        // GET: GerarProva
        public ActionResult GerarProva()
        {
            List<string> assuntos = new List<string>();
            assuntos.Add("");
            ViewBag.ListaAssuntos = new List<string>();

            ViewBag.AssuntosSelecionados = new List<string>();
            ViewBag.Disciplina = null;
            return View();
        }
        [HttpPost]
        public void BuscarAssunto(int id)
        {
            ViewBag.ListaAssuntos = QuestaoDAO.BuscarAssuntoPorDisciplina(Convert.ToInt32(ViewBag.Disciplina));
            ViewBag.AssuntosSelecionados = new List<string>();
        }

        public ActionResult AdicionarQuestoesNaProva()
        {
            return View();
        }
    }
}