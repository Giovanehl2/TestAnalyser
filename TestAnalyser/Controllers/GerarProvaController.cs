using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
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
        public static List<string> assuntos = new List<string>();
        // GET: GerarProva
        public ActionResult GerarProva()
        {
            List<string> assuntos = new List<string>();
            ViewBag.Lista = assuntos;

            ViewBag.AssuntosSelecionados = new List<string>();
            ViewBag.Disciplina = null;
            return View(new Prova());
        }

        public JsonResult BuscarAssunto(int id)
        {
             assuntos.Clear();
             assuntos = QuestaoDAO.BuscarAssuntoPorDisciplina(Convert.ToInt32(ViewBag.Disciplina));
            return Json(assuntos);
        }

        public ActionResult AdicionarQuestoesNaProva()
        {
            return View();
        }

        public ActionResult SalvarProva(Prova prova)
        {
            return View();
        }

        public JsonResult ListaAssuntos()
        { 
            return Json(assuntos, JsonRequestBehavior.AllowGet);
        }

    }
}